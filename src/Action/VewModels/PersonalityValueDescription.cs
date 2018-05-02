using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using Newtonsoft.Json;

namespace Action.VewModels
{
    public  class PersonalityValueDescription
    {
        public class  STranslatedDescription
        {
            public  string Name { get; set; }
            public  string Description { get; set; }

            public STranslatedDescription(string name, string description)
            {
                Name = name;
                Description = description;
            }
        }

        [JsonIgnore]
        public Dictionary<string, STranslatedDescription> TranslatedData =
            new Dictionary<string, STranslatedDescription>();

        public PersonalityValueDescription()
        {
            #region Agreeableness

            TranslatedData.Add("Self-enhancement",
                new STranslatedDescription("Autocrescimento",
                    "Procuram prazer e gratificação por eles mesmos. Tem na sua cultura a principal fonte de referencia."));

            TranslatedData.Add("Hedonism",
                new STranslatedDescription("Hendonismo",
                    "Procure prazer e gratidão sensual para si. Proudutos feitos para pessoas que atraves deles buscam satifazer suas ambições"));
            TranslatedData.Add("Openness to change",
                new STranslatedDescription("Abertura à mudança",
                    "Enfatize a ação, o pensamento e o sentimento independentes, bem como a prontidão para novas experiências.	Não se limitam a um único mercado, abrindo e fechando diferentes frentes"));
            TranslatedData.Add("Conservation",
                new STranslatedDescription("Conservação",
                    "Enfatize a auto-restrição, a ordem e a resistência à mudança.	Voltados para performace e lucro"));
            TranslatedData.Add("Self-transcendence",
                new STranslatedDescription("Autotranscendência",
                    "Mostrar preocupação pelo bem-estar e interesses de terceiros.	Preocupação com os stakehoders e produzir uma cadeia de consumo sustentável"));
           
            #endregion

        }

        private string _name;

        [JsonProperty("name")]
        public string Name
        {
            get
            {
             return TranslatedData.FirstOrDefault(x=>x.Key.Equals(_name)).Value?.Name ?? _name;
            }
            set { _name = value; }
        }


        [JsonProperty("description")]
        public string Description
        {
            get
            {
                return TranslatedData.FirstOrDefault(x=>x.Key.Equals(_name)).Value?.Description ?? _name;
            }
        }

        [JsonProperty("percentile")] 
        public double? Percentile { get; set; }
    }
    
}