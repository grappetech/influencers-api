using System;
//using AutoMapper;

namespace Action.Services.AutoMapper
{
    public class ToneAnalyzerMappingProfile //: Profile
    {
        public ToneAnalyzerMappingProfile()
        {
           /* CreateMap<ToneAnalysis, ToneResult>();
            CreateMap<SentenceAnalysis, SentencesTone>();
            CreateMap<DocumentAnalysis, DocumentTone>();
            CreateMap<ToneCategory, ToneCategories>();
            CreateMap<ToneScore, Tone>();*/
            
        }
        public static void Register()
        {
            Activator.CreateInstance<ToneAnalyzerMappingProfile>();
        }
    }
}