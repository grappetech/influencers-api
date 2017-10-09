using System;
using System.Collections.Generic;
using IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model;

namespace Action.Services.Watson.PersonalityInsights
{
    public class PersonalityResult
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public List<Personality> Personality { get; set; } = new List<Personality>();

        public List<Detail> Needs { get; set; } = new List<Detail>();

        public List<Detail> Values { get; set; } = new List<Detail>();

        public List<ConsumptionPreferences> ConsumptionPreferences { get; set; } = new List<ConsumptionPreferences>();

        public static PersonalityResult Parse(Profile pResult)
        {
            var lPersonalityResult = new PersonalityResult();

            foreach (var lPersonality in pResult.Personality)
            {
                var lDetails = new List<Detail>();
                lPersonality.Children.ForEach(x => lDetails.Add(new Detail {Name = x.Name, Percentile = x.Percentile}));

                var lNewPersonality = new Personality
                {
                    Name = lPersonality.Name,
                    Percentile = lPersonality.Percentile,
                    Details = lDetails
                };

                lPersonalityResult.Personality.Add(lNewPersonality);
            }

            pResult.Needs.ForEach(x =>
                lPersonalityResult.Needs.Add(new Detail {Name = x.Name, Percentile = x.Percentile}));

            pResult.Values.ForEach(x =>
                lPersonalityResult.Values.Add(new Detail {Name = x.Name, Percentile = x.Percentile}));

            foreach (var lConsumption in pResult.ConsumptionPreferences)
            {
                var lDetails = new List<ConsumptionDetail>();
                lConsumption.ConsumptionPreferences.ForEach(x =>
                    lDetails.Add(new ConsumptionDetail {Name = x.Name, Score = Convert.ToInt32(x.Score)}));

                var lNewConsumption = new ConsumptionPreferences
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