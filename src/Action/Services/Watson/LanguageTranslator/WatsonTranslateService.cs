using System.Linq;
using IBM.WatsonDeveloperCloud.LanguageTranslator.v2;

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
            return service.Translate("pt-BR", "en-US", text).Translations.FirstOrDefault()?.Translation;
        }
    }
}