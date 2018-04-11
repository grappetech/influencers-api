using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Action.Extensions;
using Action.Models;
using Action.Models.Core;
using Action.Models.Scrap;
using Action.Models.Watson;
using Action.Models.Watson.NLU;
using Action.Models.Watson.PersonalityInsights;
using Action.Models.Watson.ToneAnalyze;
using Action.Services.Scrap.V2;
using Action.Services.Watson.V2.LanguageTanslator;
using Action.Services.Watson.V2.NaturalLanguageUnderstanding;
using Action.Services.Watson.V2.PersonalityInsights;
using Action.Services.Watson.V2.ToneAnalyzer;
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;
using Microsoft.EntityFrameworkCore;

namespace Action.Services.TaskScheduler
{
    public class ApplicationTaskScheduler
    {
        public static void ProccessDataExtraction(ApplicationDbContext dbContext)
        {
            #region Set Watson Services Credentials

            Debugger.Log(0, "SCP", "Buscando Credenciais." + Environment.NewLine);
            var credentials = dbContext.WatsonCredentials.AsNoTracking().ToList();

            var wltc =
                credentials.FirstOrDefault(x => x.Service == EWatsonServices.WatsonLanguageTranslator);
            var wpic =
                credentials.FirstOrDefault(x => x.Service == EWatsonServices.WatsonPersonalityInsights);
            var wnluc =
                credentials.FirstOrDefault(x => x.Service == EWatsonServices.WatsonNaturalLanguageUnderstanding);
            var wta =
                credentials.FirstOrDefault(x => x.Service == EWatsonServices.WatsonToneAnalyzer);


            Debugger.Log(0, "SCP", "Credenciais definidas." + Environment.NewLine);

            #endregion

            #region Extract Inner Links from Sources

            Debugger.Log(0, "SCP", "Extraindo Links das fontes" + Environment.NewLine);
            var links = new List<LinkItem>();
            var queue = dbContext.ScrapQueue
                .AsNoTracking()
                .ToList()
                .AsParallel();

            Parallel.ForEach(queue,
                scrapQueue =>
                {
                    lock (links)
                    {
                        links.AddRange(new Scrapper().ProccessTask(scrapQueue.Url, 1).GetAwaiter().GetResult());
                    }
                });
            Debugger.Log(0, "SCP", "Links extraídos." + Environment.NewLine);

            #endregion

            Debugger.Log(0, "SCP", "Inicianado Enriquecimento de Dados." + Environment.NewLine);
            foreach (var item in links)
            {
                try
                {
                    #region Enhance Page Content

                    var nluSvc = new NLUService();
                    NLUResult result = null;
                    AnalysisResults nluAnalysis = null;
                    try
                    {
                        nluAnalysis = nluSvc
                            .ProccessUrl(item.Href, wnluc.UserName, wnluc.Password, wnluc.Model, wnluc.Version).Result;
                        result = NLUResult.Parse(nluAnalysis);
                    }
                    catch
                    {
                        continue;
                    }


                    if (result == null) continue;
                    Debugger.Log(0, "SCP", "Enriquecendo o site " + item.Text + Environment.NewLine);

                    var scrappedPage = new ScrapedPage
                    {
                        Date = DateTime.Today,
                        Id = Guid.NewGuid(),
                        Status = EDataExtractionStatus.Finalized,
                        Text = nluAnalysis.AnalyzedText.RemoveIllegalChars(),
                        Url = item.Href
                    };

                    scrappedPage.Translated = new LanguageTranslatorService()
                        .ProccessTranslation(scrappedPage.Text, "pt", "en", wltc.UserName, wltc.Password)
                        .GetAwaiter()
                        .GetResult().RemoveIllegalChars();

                    result.ScrapedPageId = scrappedPage.Id;
                    dbContext.ScrapedPages.Add(scrappedPage);
                    dbContext.NluResults.Add(result);

                    Debugger.Log(0, "SCP", item.Text + "Enriquecido" + Environment.NewLine);

                    #endregion

                    #region ExtractTone

                    foreach (var entity in result.Entity)
                    {
                        var sentences = result.Relations.Select(x => x.sentence);
                        foreach (var sentence in sentences)
                        {
                            var translatedSentence =  new LanguageTranslatorService()
                                .ProccessTranslation(sentence, "pt", "en", wltc.UserName, wltc.Password)
                                .GetAwaiter()
                                .GetResult().RemoveIllegalChars();
                            
                            var toneResult = new ToneAnalyzerService()
                                .ProccessToneAnalisys(translatedSentence, "en", wta.UserName, wta.Password, wta.Version)
                                .GetAwaiter()
                                .GetResult();
                            if(toneResult == null) continue;
                            
                            var tone = ToneResult.Parse(toneResult);
                            tone.NluEntityId = entity.Id;
                            tone.ScrapedPageId = result.ScrapedPageId;
                            dbContext.Tones.Add(tone);
                        }
                    }

                    #endregion

                    #region Extract Personality

                    Debugger.Log(0, "SCP", "Extraindo Personalidade." + Environment.NewLine);
                    var profile = new PersonalityInsightsService()
                        .ProccessText(scrappedPage.Translated, wpic.UserName, wpic.Password, wpic.Version)
                        .GetAwaiter()
                        .GetResult();

                    var piResult = PersonalityResult.Parse(profile);

                    if (piResult != null)
                    {
                        piResult.ScrapedPageId = scrappedPage.Id;
                        dbContext.Personalities.Add(piResult);
                    }

                    Debugger.Log(0, "SCP", "Extração de Personalidade Concluída." + Environment.NewLine);

                    #endregion

                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Debugger.Log(0, "ERR-SCP", ex.Message + Environment.NewLine);
                }
            }

            Debugger.Log(0, "SCP", "Enriquecimento Finalizado." + Environment.NewLine);
        }


        public static void ExtractPersonality(ApplicationDbContext dbContext){

            #region Set Watson Services Credentials
    
            Debugger.Log(0, "SCP", "Buscando Credenciais." + Environment.NewLine);
            var credentials = dbContext.WatsonCredentials.AsNoTracking().ToList();
            var wpic =
                credentials.FirstOrDefault(x => x.Service == EWatsonServices.WatsonPersonalityInsights);


            Debugger.Log(0, "SCP", "Credenciais definidas." + Environment.NewLine);

            #endregion
            #region Extract Personality
            try{
                var entities = new Dictionary<Guid, List<long?>>();

                var result = dbContext.NluResults
                                      .Include(x => x.Entity)
                                      .AsNoTracking()
                                      .ToList();

                result.ForEach(r =>
                {
                    var itm =r.Entity
                              .Select(x => x.EntityId)
                              .ToList();
                    entities.Add(r.ScrapedPageId, itm.Where(x => x.HasValue).ToList());
                });


                foreach (var page in entities)
                {

                    var pg = dbContext.ScrapedPages
                                      .AsNoTracking()
                                      .FirstOrDefault(x => x.Id == page.Key && x.Status == EDataExtractionStatus.InProcces);

                    if (pg == null) continue;
                    page.Value.ForEach(ent =>
                    {
                        Debugger.Log(0, "SCP", "Extraindo Personalidade." + Environment.NewLine);
                        var profile = new PersonalityInsightsService()
                            .ProccessText(pg.Translated, wpic.UserName, wpic.Password, wpic.Version)
                            .GetAwaiter()
                            .GetResult();

                        var piResult = PersonalityResult.Parse(profile);

                        if (piResult != null)
                        {
                            piResult.EntityId = ent.Value;
                            piResult.ScrapedPageId = pg.Id;
                            dbContext.Personalities.Add(piResult);
                            var pagina = dbContext.ScrapedPages.Find(pg.Id);
                            pagina.Status = EDataExtractionStatus.Finalized;
                            dbContext.Entry(pagina).State = EntityState.Modified;
                            dbContext.SaveChanges();
                        }
                    });
                }
                Debugger.Log(0, "SCP", "Extração de Personalidade Concluída." + Environment.NewLine);

#endregion

            }
            catch (Exception ex)
            {
                Debugger.Log(0, "ERR-SCP", ex.Message + Environment.NewLine);
            }
        }
    }
}