using System;
using WatsonProfile = IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model.Profile;
//using Profile = AutoMapper.Profile;
using Preferences = Action.Models.Watson.PersonalityInsights.ConsumptionPreferences;

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