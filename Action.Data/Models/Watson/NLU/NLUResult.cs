using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Action.Data.Models.Watson.NLU
{
    public class NLUResult
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ScrapedPageId { get; set; }

        public List<Concept> Concept { get; set; } = new List<Concept>();

        public List<Entity> Entity { get; set; } = new List<Entity>();

        public List<Keywords> Keywords { get; set; } = new List<Keywords>();

        public List<Category> Category { get; set; } = new List<Category>();

        public Sentiment Sentiment { get; set; }

        public List<SemanticRole> SemanticRoles { get; set; } = new List<SemanticRole>();

        public List<Relation> Relations { get; set; } = new List<Relation>();

        public Metadata Metadata { get; set; }
        public Emotion Emotion { get; set; }

        //public static NLUResult Parse(AnalysisResults pResult)
        //{
        //    var lNLUResult = new NLUResult();
        //    if (pResult.Emotion != null)
        //        lNLUResult.Emotion = new Emotion
        //        {
        //            EmotionDocument = new EmotionDocument
        //            {
        //                EmotionDetail = new EmotionDetail
        //                {
        //                    Anger = pResult.Emotion.Document.Emotion.Anger,
        //                    Disgust = pResult.Emotion.Document.Emotion.Disgust,
        //                    Fear = pResult.Emotion.Document.Emotion.Fear,
        //                    Sadness = pResult.Emotion.Document.Emotion.Fear,
        //                    Joy = pResult.Emotion.Document.Emotion.Joy
        //                }
        //            }
        //        };
        //    else
        //        lNLUResult.Emotion = null;

        //    if (pResult.Categories != null)
        //        foreach (var categoriesResult in pResult.Categories)
        //            lNLUResult.Category.Add(new Category
        //            {
        //                Score = categoriesResult.Score,
        //                Label = categoriesResult.Label
        //            });
        //    else
        //        lNLUResult.Category = null;

        //    if (pResult.Concepts != null)
        //        foreach (var conceptsResult in pResult.Concepts)
        //            lNLUResult.Concept.Add(new Concept
        //            {
        //                Text = conceptsResult.Text,
        //                Dbpedia_resource = conceptsResult.DbpediaResource,
        //                Relevance = conceptsResult.Relevance
        //            });
        //    else
        //        lNLUResult.Concept = null;

        //    if (pResult.Entities != null)
        //        foreach (var entitiesResult in pResult.Entities)
        //            lNLUResult.Entity.Add(new Entity
        //            {
        //                Text = entitiesResult.Text,
        //                Relevance = entitiesResult.Relevance,
        //                Disambiguation = new Disambiguation
        //                {
        //                    Dbpedia_resource = entitiesResult.Disambiguation.DbpediaResource,
        //                    Name = entitiesResult.Disambiguation.Name
        //                },
        //                Count = entitiesResult.Count,
        //                Type = entitiesResult.Type
        //            });
        //    else
        //        lNLUResult.Entity = null;

        //    if (pResult.Keywords != null)
        //        foreach (var keywordsResult in pResult.Keywords)
        //            lNLUResult.Keywords.Add(new Keywords
        //            {
        //                relevance = keywordsResult.Relevance,
        //                text = keywordsResult.Text,
        //                emotions = keywordsResult.Emotion != null
        //                    ? new EmotionsKeyword
        //                    {
        //                        anger = keywordsResult.Emotion != null ? keywordsResult.Emotion.Anger : 0,
        //                        disgust =
        //                            (double) (keywordsResult.Emotion != null ? keywordsResult.Emotion.Disgust : 0),
        //                        fear = (double) (keywordsResult.Emotion != null ? keywordsResult.Emotion.Fear : 0),
        //                        joy = (double) (keywordsResult.Emotion != null ? keywordsResult.Emotion.Joy : 0),
        //                        sadness = (double) (keywordsResult.Emotion != null ? keywordsResult.Emotion.Sadness : 0)
        //                    }
        //                    : null,
        //                sentiment = keywordsResult.Sentiment != null
        //                    ? new SentimentKeyword
        //                    {
        //                        score = (double) (keywordsResult.Sentiment != null ? keywordsResult.Sentiment.Score : 0)
        //                    }
        //                    : null
        //            });

        //    if (pResult.Relations != null)
        //        foreach (var relationsResult in pResult.Relations)
        //        {
        //            var lRelation = new Relation();
        //            lRelation.score = relationsResult.Score;
        //            lRelation.sentence = relationsResult.Sentence;
        //            lRelation.type = relationsResult.Type;
        //            foreach (var relationsResultArgument in relationsResult.Arguments)
        //            {
        //                var lArgument = new Argument();
        //                lArgument.text = relationsResultArgument.Text;
        //                foreach (var relationEntity in relationsResultArgument.Entities)
        //                    lArgument.EntityRelations.Add(new EntityRelation
        //                    {
        //                        text = relationEntity.Text,
        //                        type = relationEntity.Type
        //                    });
        //                lRelation.Arguments.Add(lArgument);
        //            }

        //            lNLUResult.Relations.Add(lRelation);
        //        }

        //    if (pResult.SemanticRoles != null)
        //        foreach (var semanticRolesResult in pResult.SemanticRoles)
        //            lNLUResult.SemanticRoles.Add(new SemanticRole
        //            {
        //                Sentence = semanticRolesResult.Sentence,
        //                Subject = new SemanticSubject
        //                {
        //                    Text = semanticRolesResult.Subject.Text
        //                },
        //                Action = new SemanticAction
        //                {
        //                    Text = semanticRolesResult.Action.Text,
        //                    Normalized = semanticRolesResult.Action.Normalized,
        //                    Verb = new SemanticVerb
        //                    {
        //                        Tense = semanticRolesResult.Action.Verb.Tense,
        //                        Text = semanticRolesResult.Action.Verb.Text
        //                    }
        //                },
        //                Object = new SemanticObject
        //                {
        //                    Text = semanticRolesResult._Object.Text
        //                }
        //            });

        //    if (pResult.Metadata != null)
        //    {
        //        lNLUResult.Metadata = new Metadata
        //        {
        //            publication_date = Convert.ToDateTime(pResult.Metadata.PublicationDate),
        //            title = pResult.Metadata.Title
        //        };
        //        foreach (var metadataAuthor in pResult.Metadata.Authors)
        //            lNLUResult.Metadata.Authors.Add(new Author
        //            {
        //                name = metadataAuthor.Name
        //            });
        //    }

        //    if (pResult.Sentiment != null)
        //    {
        //        var lSentiment = new Sentiment();
        //        lSentiment.SentimentDoc = new SentimentDocument
        //        {
        //            Score = pResult.Sentiment.Document.Score
        //        };

        //        foreach (var targetedSentimentResultse in pResult.Sentiment.Targets)
        //            lSentiment.SentimentTarget.Add(new SentimentTarget
        //            {
        //                Text = targetedSentimentResultse.Text,
        //                Score = targetedSentimentResultse.Score
        //            });
        //        lNLUResult.Sentiment = lSentiment;
        //    }

        //    return lNLUResult;
        //}
    }
}