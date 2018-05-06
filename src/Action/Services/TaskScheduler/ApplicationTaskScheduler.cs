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
using Action.Services.SMTP;
using Action.Services.Watson.V2.LanguageTanslator;
using Action.Services.Watson.V2.NaturalLanguageUnderstanding;
using Action.Services.Watson.V2.PersonalityInsights;
using Action.Services.Watson.V2.ToneAnalyzer;
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;
using Microsoft.EntityFrameworkCore;
using WatsonServices.Services.ApiClient.Core.Models;
using WatsonServices.Services.ApiClient.Watson;

namespace Action.Services.TaskScheduler
{
    public class ApplicationTaskScheduler
    {
        public static void ProccessDataExtraction(ApplicationDbContext dbContext)
        {
            
            SmtpService.SendMessage("luiz@nexo.ai", "[ACTION-API NLU Started]", $"Date Time: {DateTime.Now}");
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
            var linkQueue = new List<ScrapQueue>();
            dbContext.ScrapSources.ToList().ForEach(s =>
            {
                dbContext.Entities.Select(e => e.Name).ToList().ForEach(i =>
                {
                    linkQueue.Add(new ScrapQueue
                    {
                        EnqueueDateTime = DateTime.UtcNow,
                        Url = s.Url.Replace("{{entity}}", i.ToLower()),
                        StartDateTime = DateTime.UtcNow
                    });
                });
            });

            dbContext.ScrapQueue.AddRange(linkQueue);

            dbContext.SaveChanges();

            var queue = dbContext.ScrapQueue
                .AsNoTracking()
                .ToList();

            queue.ForEach(
                scrapQueue =>
                {
                    try
                    {
                        var links = new Scrapper().ProccessTask(scrapQueue.Url, 1).GetAwaiter().GetResult();
                        //links.AddRange();
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
                                        .ProccessUrl(item.Href, wnluc.UserName, wnluc.Password, wnluc.Model,
                                            wnluc.Version).Result;
                                    result = NLUResult.Parse(nluAnalysis);
                                }
                                catch (Exception ex)
                                {
                                    Debugger.Log(0, "ERR-SCP", ex.Message + Environment.NewLine);
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
                                    .ProccessTranslation(scrappedPage.Text, "pt", "en", wltc.UserName,
                                        wltc.Password)
                                    .GetAwaiter()
                                    .GetResult().RemoveIllegalChars();

                                result.Keywords.ForEach(k =>
                                {
                                    var fragment = "";
                                    var pos = nluAnalysis.AnalyzedText.IndexOf(k.text);
                                    pos = pos > 101 ? pos - 100 : pos;
                                    var end = pos > 101 ? pos + 100 : 0 + k.text.Length;
                                    end = (end + 100 + pos) < (nluAnalysis.AnalyzedText.Length - 100)
                                        ? end + 100
                                        : end;
                                    fragment = nluAnalysis.AnalyzedText.Substring(pos < 0 ? 0 : pos,
                                        (end + pos) > nluAnalysis.AnalyzedText.Length
                                            ? (nluAnalysis.AnalyzedText.Length - pos - 1)
                                            : end);
                                    k.fragment = fragment;

                                    if (fragment.Length > 100)
                                        k.translatedFragment = new LanguageTranslatorService()
                                            .ProccessTranslation(fragment, "pt", "en", wltc.UserName, wltc.Password)
                                            .GetAwaiter()
                                            .GetResult().RemoveIllegalChars();

                                    if (k.emotions == null)
                                    {
                                        var emotion = new WatsonToneAnalyzerService()
                                            .PostTone(k.translatedFragment,
                                                new ApiAuthorization().SetBasicAuthorization(wta.UserName,
                                                    wta.Password))
                                            .GetAwaiter()
                                            .GetResult();
                                        k.emotions = new EmotionsKeyword
                                        {
                                            anger = emotion?.DocumentTone?
                                                        .ToneCategories?
                                                        .FirstOrDefault(x => x.CategoryId.Equals("emotion_tone"))?
                                                        .Tones?
                                                        .FirstOrDefault(x => x.ToneId.ToLower().Equals("anger"))?
                                                        .Score ?? 0,
                                            disgust = emotion?.DocumentTone?
                                                          .ToneCategories?
                                                          .FirstOrDefault(x => x.CategoryId.Equals("emotion_tone"))?
                                                          .Tones
                                                          ?.FirstOrDefault(
                                                              x => x.ToneId.ToLower().Equals("disgust"))?.Score ??
                                                      0,
                                            fear = emotion?.DocumentTone?
                                                       .ToneCategories?
                                                       .FirstOrDefault(x => x.CategoryId.Equals("emotion_tone"))?
                                                       .Tones
                                                       ?.FirstOrDefault(x => x.ToneId.ToLower().Equals("fear"))
                                                       ?.Score ?? 0,
                                            joy = emotion?.DocumentTone?
                                                      .ToneCategories?
                                                      .FirstOrDefault(x => x.CategoryId.Equals("emotion_tone"))?
                                                      .Tones
                                                      ?.FirstOrDefault(x => x.ToneId.ToLower().Equals("joy"))
                                                      ?.Score ?? 0,
                                            sadness = emotion?.DocumentTone?
                                                          .ToneCategories?
                                                          .FirstOrDefault(x => x.CategoryId.Equals("emotion_tone"))?
                                                          .Tones
                                                          ?.FirstOrDefault(
                                                              x => x.ToneId.ToLower().Equals("sadness"))?.Score ?? 0
                                        };
                                    }

                                    if (k.sentiment == null)
                                        k.sentiment = new SentimentKeyword
                                        {
                                            score = (k.emotions?.joy ?? 0) - (new double[]
                                            {
                                                k.emotions?.anger ?? 0, k.emotions?.disgust ?? 0,
                                                k.emotions?.fear ?? 0, k.emotions?.sadness ?? 0
                                            }).Average()
                                        };
                                });
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
                                        var translatedSentence = new LanguageTranslatorService()
                                            .ProccessTranslation(sentence, "pt", "en", wltc.UserName, wltc.Password)
                                            .GetAwaiter()
                                            .GetResult().RemoveIllegalChars();

                                        var toneResult = new WatsonToneAnalyzerService().PostTone(translatedSentence,
                                                new ApiAuthorization().SetBasicAuthorization(wta.UserName,
                                                    wta.Password))
                                            .GetAwaiter()
                                            .GetResult();
                                        if (toneResult == null) continue;

                                        var tone = new ToneResult
                                        {
                                            DocumentTone = toneResult.DocumentTone,
                                            ScrapedPageId = result.ScrapedPageId,
                                            SetenceTones = toneResult.SetenceTones
                                        };
                                        tone.NluEntityId = entity.Id;
                                        tone.ScrapedPageId = result.ScrapedPageId;
                                        dbContext.Tones.Add(tone);
                                    }
                                }

                                #endregion

                                #region Extract Personality

                                Debugger.Log(0, "SCP", "Extraindo Personalidade." + Environment.NewLine);
                                var profile = new PersonalityInsightsService()
                                    .ProccessText(scrappedPage.Translated, wpic.UserName, wpic.Password,
                                        wpic.Version)
                                    .GetAwaiter()
                                    .GetResult();

                                var piResult = PersonalityResult.Parse(profile);

                                if (piResult != null)
                                {
                                    piResult.ScrapedPageId = scrappedPage.Id;
                                    dbContext.Personalities.Add(piResult);
                                }

                                Debugger.Log(0, "SCP",
                                    "Extração de Personalidade Concluída." + Environment.NewLine);

                                #endregion

                                #region Extract Tone

                                Debugger.Log(0, "SCP", "Extraindo Tom." + Environment.NewLine);
                                var tones = new WatsonToneAnalyzerService()
                                    .PostTone(scrappedPage.Translated,
                                        new ApiAuthorization().SetBasicAuthorization(wta.UserName, wta.Password))
                                    .GetAwaiter()
                                    .GetResult();

                                var taResult = new ToneResult
                                {
                                    DocumentTone = tones.DocumentTone,
                                    ScrapedPageId = result.ScrapedPageId,
                                    SetenceTones = tones.SetenceTones
                                };

                                if (taResult != null)
                                {
                                    taResult.ScrapedPageId = scrappedPage.Id;
                                    dbContext.Tones.Add(taResult);
                                }

                                Debugger.Log(0, "SCP", "Extração de Tom Concluída." + Environment.NewLine);

                                #endregion


                                dbContext.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                Debugger.Log(0, "ERR-SCP", ex.Message + Environment.NewLine);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debugger.Log(0, "ERR", "Falha NLU." + ex.Message);
                    }
                });

            #endregion
        }


        public static void ExtractPersonality(ApplicationDbContext dbContext)
        {
            #region Set Watson Services Credentials

            Debugger.Log(0, "SCP", "Buscando Credenciais." + Environment.NewLine);
            var credentials = dbContext.WatsonCredentials.AsNoTracking().ToList();
            var wpic =
                credentials.FirstOrDefault(x => x.Service == EWatsonServices.WatsonPersonalityInsights);


            Debugger.Log(0, "SCP", "Credenciais definidas." + Environment.NewLine);

            #endregion

            #region Extract Personality

            try
            {
                var entities = new Dictionary<Guid, List<long?>>();

                var result = dbContext.NluResults
                    .Include(x => x.Entity)
                    .AsNoTracking()
                    .ToList();

                result.ForEach(r =>
                {
                    var itm = r.Entity
                        .Select(x => x.EntityId)
                        .ToList();
                    entities.Add(r.ScrapedPageId, itm.Where(x => x.HasValue).ToList());
                });


                SmtpService.SendMessage("luiz@nexo.ai", "[ACTION API SCP]",
                    $"Extraindo Personalidade de {entities.Values.Count} entidades em {entities.Keys.Count} fontes");
                foreach (var page in entities)
                {
                    try
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
                    catch (Exception ex)
                    {
                        Debugger.Log(0, "ERR-SCP", ex.Message + Environment.NewLine);
                    }
                }


                #endregion
            }
            catch (Exception ex)
            {
                Debugger.Log(0, "ERR-SCP", ex.Message + Environment.NewLine);
                SmtpService.SendMessage("luiz@nexo.ai", "[ACTION-API NLU ERROR]", $"Date Time: {DateTime.Now + Environment.NewLine} <br/> {ex.Message + Environment.NewLine}");
            }
            
            SmtpService.SendMessage("luiz@nexo.ai", "[ACTION-API NLU Finished]", $"Date Time: {DateTime.Now}");
           
        }
    }
}