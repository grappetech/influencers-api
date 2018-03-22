using System;
using Action.Services.Watson.PersonalityInsights;
using WatsonProfile = IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model.Profile;
using IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model;
using ConsumptionPreferences = IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model.ConsumptionPreferences;
//using Profile = AutoMapper.Profile;
using Preferences = Action.Services.Watson.PersonalityInsights.ConsumptionPreferences;

namespace Action.Services.AutoMapper
{
    public class PersonalityInsightsMappingProfile// : Profile
    {
        public PersonalityInsightsMappingProfile()
        {
            /*CreateMap<WatsonProfile, PersonalityResult>();
            CreateMap<Trait, Detail>();
            CreateMap<Trait, Personality>();
            CreateMap<ConsumptionPreferencesCategory, ConsumptionDetail>();
            CreateMap<ConsumptionPreferencesCategory, Preferences>();
            CreateMap<ConsumptionPreferences, ConsumptionDetail>();*/
        }
        
        
        public static void Register()
        {
            Activator.CreateInstance<PersonalityInsightsMappingProfile>();
        }
    }
}