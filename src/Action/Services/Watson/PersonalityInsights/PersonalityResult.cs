using System;
using System.Collections.Generic;
using IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model;

namespace Action.Services.Watson.PersonalityInsights
{
    public class PersonalityResult
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        private List<Personality> _personality = new List<Personality>();
        public List<Personality> Personality
        {
            get { return _personality; }
            set { _personality = value; }
        }

        private List<Detail> _needs = new List<Detail>();
        public List<Detail> Needs
        {
            get { return _needs; }
            set { _needs = value; }
        }

        private List<Detail> _values = new List<Detail>();
        public List<Detail> Values
        {
            get { return _values; }
            set { _values = value; }
        }

        private List<ConsumptionPreferences> _consumptionPreferences = new List<ConsumptionPreferences>();
        public List<ConsumptionPreferences> ConsumptionPreferences
        {
            get { return _consumptionPreferences; }
            set { _consumptionPreferences = value; }
        }

        public static PersonalityResult Parse(Profile pResult)
        {
            PersonalityResult lPersonalityResult = new PersonalityResult();

            foreach (var lPersonality in pResult.Personality)
            {
                List<Detail> lDetails = new List<Detail>();
                lPersonality.Children.ForEach(x => lDetails.Add(new Detail { Name = x.Name, Percentile = x.Percentile }));

                Personality lNewPersonality = new Personality
                {
                    Name = lPersonality.Name,
                    Percentile = lPersonality.Percentile,
                    Details = lDetails
                };

                lPersonalityResult.Personality.Add(lNewPersonality);
            }

            pResult.Needs.ForEach(x => lPersonalityResult.Needs.Add(new Detail { Name = x.Name, Percentile = x.Percentile }));

            pResult.Values.ForEach(x => lPersonalityResult.Values.Add(new Detail { Name = x.Name, Percentile = x.Percentile }));

            foreach (var lConsumption in pResult.ConsumptionPreferences)
            {
                List<ConsumptionDetail> lDetails = new List<ConsumptionDetail>();
                lConsumption.ConsumptionPreferences.ForEach(x => lDetails.Add(new ConsumptionDetail { Name = x.Name, Score = Convert.ToInt32(x.Score) }));

                ConsumptionPreferences lNewConsumption = new ConsumptionPreferences
                {
                    ConsumptionPreferenceId = lConsumption.ConsumptionPreferenceCategoryId,
                    Name = lConsumption.Name,
                    ConsumptionDetails = lDetails
                };

                lPersonalityResult.ConsumptionPreferences.Add(lNewConsumption);
            }

            return lPersonalityResult;
        }
    }
}
