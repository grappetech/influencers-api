using System.Collections.Generic;
using System.Linq;
using IBM.WatsonDeveloperCloud.LanguageTranslator.v2;
using IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Model;

namespace Action.Services.Watson.LanguageTranslator
{
    public class WatsonTranslateService
    {
        private static WatsonTranslateService _instance;


        public static WatsonTranslateService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WatsonTranslateService();
                return _instance;
            }
        }


        public string Translate(string text)
        {
            var service = new LanguageTranslatorService();
            service.Password = "y0bEmI63FMhF";
            service.UserName = "1093b643-b149-4a8a-9da5-b80359fc9519";
            return service.Translate(new TranslateRequest {Source = "pt-BR", Target = "en-US", Text = new List<string>
            {
                text   
            }}).Translations.FirstOrDefault()?.TranslationOutput;
        }
    }
}