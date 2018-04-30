using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using Newtonsoft.Json;

namespace Action.VewModels
{
    public abstract class APersonalityDescription
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

        public APersonalityDescription()
        {
            #region Agreeableness

            TranslatedData.Add("Agreeableness",
                new STranslatedDescription("Amabilidade",
                    "Tendência da pessoa que a leva a ser compassiva e cooperativa em relação aos outros"));

            TranslatedData.Add("Sympathy",
                new STranslatedDescription("Simpatia",
                    "São carinhosos e compassivos. São proximas, preocupadas e carinhosas com seus clientes "));
            TranslatedData.Add("Altruism",
                new STranslatedDescription("Altruísmo",
                    "Ajudar os outros é realmente gratificante, é uma forma de sua realização, em vez de auto-sacrifício. Tem na ajuda seu proprósito "));
            TranslatedData.Add("Trust",
                new STranslatedDescription("Confiança",
                    " Justo, honesto, e tem boas intenções.  Acredita no ser humano e promove relacoes verdadeiras entre eles"));
            TranslatedData.Add("Modesty",
                new STranslatedDescription("Modéstia",
                    "É modesto, no entanto,  não necessariamente falta confiança ou auto-estima. Low profile, não precisa de exposição desnecessária.Seus produtos falam por sí  "));
            TranslatedData.Add("Uncompromising",
                new STranslatedDescription("Determinação",
                    "Sem necessidade de pretensão ou manipulação ao lidar com outros e, portanto, são sinceros, honesto e verdadeiro Pés no chão, sem grandes inovações, com icones tradicionais de solidez e consistencia "));
            TranslatedData.Add("Cooperation",
                new STranslatedDescription("Cooperação",
                    "São perfeitamente disposto a comprometer ou negar suas próprias necessidades para se dar bem com os outros São parte de um eco sistema, e só existem se o ambiente é saudável "));

            #endregion

            #region Conscientiousness

            TranslatedData.Add("Conscientiousness",
                new STranslatedDescription("Estado Consciente",
                    "É  uma tendência que leva a agir de forma organizada e cuidadosa."));
            TranslatedData.Add("Self-efficacy",
                new STranslatedDescription("Autoeficiência",
                    "Confiante em sua capacidade de realizar coisas. Na vanguarda do mercado. Dita tendencias, domina a cadeia produtiva."));
            TranslatedData.Add("Cautiousness",
                new STranslatedDescription("Cautela ",
                    "Disposto a pensar por possibilidades cuidadosamente antes de agir. Tem na sua reputação o seu maior valor "));
            TranslatedData.Add("Achievement striving",
                new STranslatedDescription("Esforço p Realização",
                    "Esforço para atingir excelência.  Alto investimento em pesquisa e desenvolvimento "));
            TranslatedData.Add("Dutifulness",
                new STranslatedDescription("Respeito",
                    "Forte senso de dever e obrigação Compromisso com causas e crenças"));
            TranslatedData.Add("Self-discipline",
                new STranslatedDescription("Autodisciplina ",
                    "Will-power, para persistir em tarefas difíceis ou desagradáveis até que sejam concluídas Trabalhando em condições adversas para atingir grandes objetivos."));
            TranslatedData.Add("Orderliness",
                new STranslatedDescription("Regularidade ",
                    "Organizados, arrumados e limpos. Confiaveis, tradicionais e conservadores "));

            #endregion

            #region Emotional Range

            TranslatedData.Add("Emotional range",
                new STranslatedDescription("Escala Emocional",
                    "É a medida em que as emoções  são sensíveis ao ambiente."));
            TranslatedData.Add("Melancholy",
                new STranslatedDescription("Melancolia ",
                    "Cuida para reagir mais rapidamente a altos e baixos da vida. Já viveu dias melhoras hoje luta para se re inventar "));
            TranslatedData.Add("Fiery",
                new STranslatedDescription("Furioso ",
                    "Tem uma tendência a sentir raiva. Quebra paradgmas, transgride e provoca"));
            TranslatedData.Add("Self-consciousness",
                new STranslatedDescription("Autoconciência ",
                    "Sensível sobre o que os outros pensam. Preocupações sobre rejeição, comentários deixam-o tímido e desconfortável. Inclusiva, não provoca conflitos, pensa no equilibrio"));
            TranslatedData.Add("Prone to worry",
                new STranslatedDescription("Propenso a se preocupar ",
                    "Aquele que sente que algo desagradável, ou perigoso está ameaçando acontecer.  Empresas que tem no seu core business o risco e formas de ameniza-lo"));
            TranslatedData.Add("Susceptible to stress",
                new STranslatedDescription("Suscetível ao stress",
                    "Tem dificuldade em lidar com estresse. Sentem pânico, confusão e desamparo quando, sob pressão, ou quando enfrentar situações de emergência. Empresas sensiveis a mudanças no seu amiente de negócio e sem respaldo 100% legal na sua operação "));
            TranslatedData.Add("Immoderation",
                new STranslatedDescription("Imoderação",
                    "Sente fortes desejos que têm dificuldade em resistir. Tende a ser orientada para curto prazeres e recompensas em vez de consequências de longo prazo. Afetadas pela moda, mudam rapidamente para se adaptar as tendencias. "));

            #endregion

            #region Extraversion

            TranslatedData.Add("Extraversion",
                new STranslatedDescription("Extroversão",
                    "Tendência da pessoa que a leva a buscar estímulos na companhia de outros."));
            TranslatedData.Add("Assertiveness",
                new STranslatedDescription("Assertividade",
                    "Gosta de cuidar e direcionar as atividades de terceiros. Tendem a ser líderes em grupos. Empresas lideres, que dão o drive do mercado."));
            TranslatedData.Add("Activity level",
                new STranslatedDescription("Nível de atividade",
                    "Mostra vida agitada e ocupada. Faz coisas com rapidez, firmeza e, normalmente,  estão envolvidos em várias atividades. Multi plataforma. Fica dificil dizer o que elas realmente são."));
            TranslatedData.Add("Excitement-seeking",
                new STranslatedDescription("Busca de Empolgação",
                    "Facilmente aborrecido sem altos níveis de estímulo. Alto investimento em mídia, lançamento de produtos, patrocínios e novidades midiáticas."));
            TranslatedData.Add("Outgoing",
                new STranslatedDescription("Extrovertido",
                    "Gostam genuinamente de outras pessoas e demonstram abertamente sentimentos positivos para os outros. Descolados, modernos, proximos."));
            TranslatedData.Add("Gregariousness",
                new STranslatedDescription("Gregarismo",
                    "Localize a companhia de outros positivamente estimulante e recompensador. Gosta da excitação de multidões. Promovem a reunião de pessoas e o desfrute de bons momentos e experiencias "));
            TranslatedData.Add("Cheerfulness",
                new STranslatedDescription("Bom Humor",
                    "Experimenta um intervalo de sentimentos positivos, incluindo felicidade, entusiasmo, otimismo, e alegria Se utilizam do humor como ferramenta de comunicação, se apropriando desse tipo de linguagem."));

            #endregion

            #region Openness

            TranslatedData.Add("Openness",
                new STranslatedDescription("Abertura",
                    "É a extensão na qual uma pessoa é aberta a experimentar uma variedade de atividades."));
            TranslatedData.Add("Intellect",
                new STranslatedDescription("Intelecto ",
                    "É intelectualmente curioso e tendem a pensar em símbolos e abstrações.  Culto a inteligencia, espirito empreendedor em larga escala."));
            TranslatedData.Add("Authority-challenging",
                new STranslatedDescription("Desafio a autoridade",
                    "Prontidão para desafiar autoridade, convenção e valores tradicionais. São disruptora e não aceitam as regras impostas. Criam sua propria lei."));
            TranslatedData.Add("Imagination",
                new STranslatedDescription("Imaginação ",
                    "Visualiza o mundo real como muito simples e comum.  Criantividade e invoação como caracteristicas principais."));
            TranslatedData.Add("Artistic interests",
                new STranslatedDescription("Interesse Artístico ",
                    "Facilmente envolvido e absorvido em eventos artísticos e naturais.  Apoiam manifestações artisticas mas não necessariamente tem isso em seu core business."));
            TranslatedData.Add("Adventurousness",
                new STranslatedDescription("Desejo de aventura ",
                    "Tem vontade de experimentar novas atividades e experimentar coisas diferentes.  Se destacam pelo apelo a natureza e o out door. Embora estejam presentes nos grandes centros."));
            TranslatedData.Add("Emotionality",
                new STranslatedDescription("Emotividade ",
                    "Tem boa noção de seus próprios sentimentos. Despertam emoção, criam situacoes emocionantes para se comunicar."));

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

    public class PersonalityDescriptionViewModel : APersonalityDescription
    {
        [JsonProperty("details")]
        public List<PersonalityDetailDescriptionViewModel> Details { get; set; }
         = new List<PersonalityDetailDescriptionViewModel>();
    }
    
    public class PersonalityDetailDescriptionViewModel : APersonalityDescription
    {
    }
    
}