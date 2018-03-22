using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire.Annotations;
using IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Model;
using Microsoft.EntityFrameworkCore.Internal;
using WatsonLanguageTranslatorService = IBM.WatsonDeveloperCloud.LanguageTranslator.v2.LanguageTranslatorService;

namespace Action.Services.Watson.V2.LanguageTanslator
{
    public class LanguageTranslatorService
    {
        public Task<string> ProccessTranslation(string text, string source, string target, string username, string password)
        {
            return Task.Run(() =>
            {
                var svc = new WatsonLanguageTranslatorService(username, password);
                return svc.Translate(new TranslateRequest
                {
                    Source = source,
                    Target = target,
                    Text = new List<string> {text}
                })
                    .Translations
                    .Select(x=>x.TranslationOutput)
                    .Join(Environment.NewLine);
            });
        }
    }
 }