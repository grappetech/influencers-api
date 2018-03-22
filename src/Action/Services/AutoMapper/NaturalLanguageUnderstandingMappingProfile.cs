using System;
using Action.Services.Watson.NLU;
//using AutoMapper;
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;
using Author = Action.Services.Watson.NLU.Author;
using MetadataAuthor = IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model.Author;

namespace Action.Services.AutoMapper
{
    public class NaturalLanguageUnderstandingMappingProfile //: Profile
    {
        public NaturalLanguageUnderstandingMappingProfile()
        {
           /* CreateMap<AnalysisResults, NLUResult>();
            CreateMap<EmotionResult, Emotion>();
            CreateMap<DocumentEmotionResults, EmotionDocument>();
            CreateMap<EmotionScores, EmotionDetail>();
            CreateMap<CategoriesResult, Category>();
            CreateMap<ConceptsResult, Concept>();
            CreateMap<EntitiesResult, Entity>();
            CreateMap<DisambiguationResult, Disambiguation>();
            CreateMap<KeywordsResult, Keywords>();
            CreateMap<FeatureSentimentResults, SentimentKeyword>();
            CreateMap<EmotionScores, EmotionsKeyword>();
            CreateMap<RelationsResult, Relation>();
            CreateMap<RelationArgument, Argument>();
            CreateMap<RelationEntity, EntityRelation>();
            CreateMap<SemanticRolesResult, SemanticRole>();
            CreateMap<SemanticRolesSubject, SemanticSubject>();
            CreateMap<SemanticRolesAction, SemanticAction>();
            CreateMap<SemanticRolesVerb, SemanticVerb>();
            CreateMap<SemanticRolesObject, SemanticObject>();
            CreateMap<MetadataResult, Metadata>();
            CreateMap<Author, MetadataAuthor>();
            CreateMap<SentimentResult, Sentiment>();
            CreateMap<TargetedSentimentResults, SentimentTarget>();*/
        }

        public static void Register()
        {
            Activator.CreateInstance<NaturalLanguageUnderstandingMappingProfile>();
        }
    }
}