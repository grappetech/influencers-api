using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
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
using IBM.WatsonDeveloperCloud.Discovery.v1.Model;
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using WatsonServices.Services.ApiClient.Core.Models;
using WatsonServices.Services.ApiClient.Watson;
using Environment = System.Environment;

namespace Action.Services.TaskScheduler
{
    public class ApplicationTaskScheduler
    {
        public static void ProccessDataExtraction(ApplicationDbContext dbContext)
        {
            SmtpService.SendMessage("luiz@nexo.ai", "[ACTION-API NLU Started]", $"Date Time: {DateTime.Now}");

            

            var tweets = dbContext.Entities.Where(x => !string.IsNullOrWhiteSpace(x.TweeterUser)).Select(x=> new {url = x.TweeterUser, id = x.Id}).ToList();

            foreach (var item in tweets)
            {
                try
                {
                    var social = new Social();
                    var data = Scrapy.GetTwitterMetrics(item.url);
                    social.EntityId = item.id;
                    social.Network = ESocialNetwork.Twitter;
                    social.Interactions = data.Tweets;
                    social.Followers = data.Followers;
                    social.Following = data.Following;
                    dbContext.SocialData.Add(social);
                    dbContext.SaveChanges();
                }
                catch{}
            }
            
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
            PrepareQueue(dbContext);
            var queue = dbContext.ScrapQueue
                .Where(x => !x.Completed)
                .AsNoTracking()
                .ToList();

            queue.ForEach(
                scrapQueue =>
                {
                    try
                    {
                        var links = new Scrapper().ProccessTask(scrapQueue.Url, 1)
                            .GetAwaiter()
                            .GetResult()
                            .Where(x => new Uri(x.Href).DnsSafeHost.Equals(new Uri(scrapQueue.Url).DnsSafeHost));

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
                                Debugger.Log(0, "SCP",
                                    "Enriquecendo o site " + item.Text + Environment.NewLine);

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
                                    k.retrieved_url = scrapQueue.Url;
                                    if (fragment.Length > 100)
                                        k.translatedFragment = new LanguageTranslatorService()
                                            .ProccessTranslation(fragment, "pt", "en", wltc.UserName,
                                                wltc.Password)
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
                                                        .FirstOrDefault(
                                                            x => x.CategoryId.Equals("emotion_tone"))?
                                                        .Tones?
                                                        .FirstOrDefault(x => x.ToneId.ToLower().Equals("anger"))
                                                        ?
                                                        .Score ?? 0,
                                            disgust = emotion?.DocumentTone?
                                                          .ToneCategories?
                                                          .FirstOrDefault(x =>
                                                              x.CategoryId.Equals("emotion_tone"))?
                                                          .Tones
                                                          ?.FirstOrDefault(
                                                              x => x.ToneId.ToLower().Equals("disgust"))
                                                          ?.Score ??
                                                      0,
                                            fear = emotion?.DocumentTone?
                                                       .ToneCategories?
                                                       .FirstOrDefault(x => x.CategoryId.Equals("emotion_tone"))
                                                       ?
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
                                                          .FirstOrDefault(x =>
                                                              x.CategoryId.Equals("emotion_tone"))?
                                                          .Tones
                                                          ?.FirstOrDefault(
                                                              x => x.ToneId.ToLower().Equals("sadness"))
                                                          ?.Score ?? 0
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
                                            .ProccessTranslation(sentence, "pt", "en", wltc.UserName,
                                                wltc.Password)
                                            .GetAwaiter()
                                            .GetResult().RemoveIllegalChars();

                                        var toneResult = new WatsonToneAnalyzerService().PostTone(
                                                translatedSentence,
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
                                        new ApiAuthorization().SetBasicAuthorization(wta.UserName,
                                            wta.Password))
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
                                ExtractPersonality(dbContext);
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
            /*   });*/

            #endregion
        }


        public static void PrepareQueue(ApplicationDbContext dbContext)
        {
            var today = DateTime.Today;
            //Entidades Diárias
            var l1 = dbContext.Entities.Where(x =>
                    x.Tier == 1 && x.ExecutionInterval <= today.Subtract(x.LastExecutionDate).Days)
                .AsNoTracking()
                .Include(x => x.ScrapSources)
                .ThenInclude(x => x.ScrapSource)
                .Select(x => new {x.Id, x.Name, Source = x.ScrapSources.Select(y => y.ScrapSource)})
                .ToList();
            
            l1.AddRange(dbContext.Entities.Where(x => !string.IsNullOrWhiteSpace(x.SiteUrl) &&
                    x.Tier == 1 && x.ExecutionInterval <= today.Subtract(x.LastExecutionDate).Days)
                .AsNoTracking()
                .Select(x => new {x.Id, x.Name, Source = (new ScrapSource[]{ new ScrapSource{Dept = 2,Url = x.SiteUrl}}).AsEnumerable()}) 
                .ToList());

            //Entidades Semanais
            var l2 = dbContext.Entities.Where(x =>
                    x.Tier == 2 && x.ExecutionInterval * 7 <= today.Subtract(x.LastExecutionDate).Days)
                .AsNoTracking()
                .Include(x => x.ScrapSources)
                .ThenInclude(x => x.ScrapSource)
                .Select(x => new {x.Id, x.Name, Source = x.ScrapSources.Select(y => y.ScrapSource)})
                .ToList();
            
            l2.AddRange(dbContext.Entities.Where(x => !string.IsNullOrWhiteSpace(x.SiteUrl) &&
                x.Tier == 2 && x.ExecutionInterval * 7 <= today.Subtract(x.LastExecutionDate).Days)
                .AsNoTracking()
                .Select(x => new {x.Id, x.Name, Source = (new ScrapSource[]{ new ScrapSource{Dept = 2,Url = x.SiteUrl}}).AsEnumerable()}) 
                .ToList());

            //Entidades Quinzenais
            var l3 = dbContext.Entities.Where(x =>
                    x.Tier == 3 && x.ExecutionInterval * 14 <= today.Subtract(x.LastExecutionDate).Days)
                .AsNoTracking()
                .Include(x => x.ScrapSources)
                .ThenInclude(x => x.ScrapSource)
                .Select(x => new {x.Id, x.Name, Source = x.ScrapSources.Select(y => y.ScrapSource)})
                .ToList();
            
            l3.AddRange(dbContext.Entities.Where(x => !string.IsNullOrWhiteSpace(x.SiteUrl) &&
                x.Tier == 3 && x.ExecutionInterval * 14 <= today.Subtract(x.LastExecutionDate).Days)
                .AsNoTracking()
                .Select(x => new {x.Id, x.Name, Source = (new ScrapSource[]{ new ScrapSource{Dept = 2,Url = x.SiteUrl}}).AsEnumerable()}) 
                .ToList());

            //Entidades Mensais
            var l4 = dbContext.Entities.Where(x =>
                x.Tier == 4 && x.ExecutionInterval * 30 <= today.Subtract(x.LastExecutionDate).Days)
                .AsNoTracking()
                .Include(x => x.ScrapSources)
                .ThenInclude(x => x.ScrapSource)
                .Select(x => new {x.Id, x.Name, Source = x.ScrapSources.Select(y => y.ScrapSource)})
                .ToList();

            l4.AddRange(dbContext.Entities.Where(x => !string.IsNullOrWhiteSpace(x.SiteUrl) &&
                x.Tier == 4 && x.ExecutionInterval * 30 <= today.Subtract(x.LastExecutionDate).Days)
                .AsNoTracking()
                .Select(x => new {x.Id, x.Name, Source = (new ScrapSource[]{ new ScrapSource{Dept = 2,Url = x.SiteUrl}}).AsEnumerable()}) 
                .ToList());
            
            //Join
            List<ScrapQueue> queue = new List<ScrapQueue>();

            l1.ForEach(x =>
            {
                foreach (var scrapSource in x.Source)
                {
                    queue.Add(new ScrapQueue
                    {
                        Id = Guid.NewGuid(),
                        EnqueueDateTime = DateTime.UtcNow,
                        Url = scrapSource.Url.Replace("{{entity}}", x.Name),
                    });
                }
            });

            l2.ForEach(x =>
            {
                foreach (var scrapSource in x.Source)
                {
                    queue.Add(new ScrapQueue
                    {
                        Id = Guid.NewGuid(),
                        EnqueueDateTime = DateTime.UtcNow,
                        Url = scrapSource.Url.Replace("{{entity}}", x.Name),
                    });
                }
            });

            l3.ForEach(x =>
            {
                foreach (var scrapSource in x.Source)
                {
                    queue.Add(new ScrapQueue
                    {
                        Id = Guid.NewGuid(),
                        EnqueueDateTime = DateTime.UtcNow,
                        Url = scrapSource.Url.Replace("{{entity}}", x.Name),
                    });
                }
            });

            l4.ForEach(x =>
            {
                foreach (var scrapSource in x.Source)
                {
                    queue.Add(new ScrapQueue
                    {
                        Id = Guid.NewGuid(),
                        EnqueueDateTime = DateTime.UtcNow,
                        Url = scrapSource.Url.Replace("{{entity}}", x.Name),
                        
                    });
                }
            });

            var id = l1.Select(x => x.Id).Union(l2.Select(x => x.Id)).Union(l3.Select(x => x.Id))
                .Union(l4.Select(x => x.Id)).ToList();
            
            foreach (var entity in dbContext.Entities.Where(x=> id.Contains( x.Id)))
            {
                entity.LastExecutionDate = DateTime.Today;
            }

            //Persiste
            dbContext.ScrapQueue.AddRange(queue);
            dbContext.SaveChanges();
        }


        [STAThread]
        public static void UpdateEntities(ApplicationDbContext context)
        {
            foreach (var entity in context.Entities)
            {
                foreach (var obj in context.NluResults.Include(x=>x.Entity)
                    .SelectMany(x=>x.Entity)
                    .Where(x=>x.EntityId == null && x.Text.ToLower().Contains(entity.Name.ToLower())))
                {
                    obj.EntityId = entity.Id;
                }
            }

            context.SaveChanges();
        }

        [STAThread]
        public static void ExtractPersonality(ApplicationDbContext context)
        {
            var dbContext = context;

            #region Set Watson Services Credentials

            Debugger.Log(0, "SCP", "Buscando Credenciais." + Environment.NewLine);
            var credentials = dbContext.WatsonCredentials.ToList();
            var wpic =
                credentials.FirstOrDefault(x => x.Service == EWatsonServices.WatsonPersonalityInsights);


            Debugger.Log(0, "SCP", "Credenciais definidas." + Environment.NewLine);

            #endregion

            #region Extract Personality

            try
            {
                var entities = new Dictionary<Guid, List<long>>();

                var result = dbContext.NluResults
                    .Include(x => x.Entity)
                    .AsNoTracking()
                    .ToList();
                var itm = dbContext.NluResults
                    .Include(x => x.Entity)
                    .SelectMany(x => x.Entity)
                    .Where(x => x.EntityId.HasValue)
                    .Select(x => x.EntityId.Value)
                    .ToList();

                result.ForEach(r => { entities.Add(r.ScrapedPageId, itm.Where(x => x > 0).Distinct().ToList()); });


                SmtpService.SendMessage("luiz@nexo.ai", "[ACTION API SCP]",
                    $"Extraindo Personalidade de {entities.Values.Count} entidades em {entities.Keys.Count} fontes");
                foreach (var page in entities)
                {
                    try
                    {
                        var pg = dbContext.ScrapedPages
                            .AsNoTracking()
                            .FirstOrDefault(x => x.Id == page.Key);

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
                                piResult.EntityId = ent;
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
                SmtpService.SendMessage("luiz@nexo.ai", "[ACTION-API NLU ERROR]",
                    $"Date Time: {DateTime.Now + Environment.NewLine} <br/> {ex.Message + Environment.NewLine}");
            }

            SmtpService.SendMessage("luiz@nexo.ai", "[ACTION-API NLU Finished]", $"Date Time: {DateTime.Now}");
            Task.Run(()=>UpdateEntities(dbContext)).GetAwaiter().OnCompleted(() =>
            {
                SmtpService.SendMessage("luiz@nexo.ai", "[ACTION-API Entity Update Finished]", $"Date Time: {DateTime.Now}");
            });
        }
    }
}