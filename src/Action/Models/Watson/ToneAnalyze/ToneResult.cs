using System;
using System.Collections.Generic;
using IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model;

namespace Action.Models.Watson.ToneAnalyze
{
    public class ToneResult
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DocumentTone DocumentTone { get; set; }

        public List<SentencesTone> SetenceTones { get; set; } = new List<SentencesTone>();
        public Guid NluEntityId { get; set; }
        public Guid ScrapedPageId { get; set; }
        public long? EntityId {  get; set; }

        public static ToneResult Parse(ToneAnalysis pResult)
        {
            var lResultadoTone = new ToneResult();

            var lDocumentTone = new DocumentTone();
            if (pResult?.DocumentTone?.ToneCategories != null)
                foreach (var toneCategory in pResult.DocumentTone.ToneCategories)
                {
                    var lTone = new ToneCategories();
                    lTone.CategoryId = toneCategory.CategoryId;
                    lTone.CategoryName = toneCategory.CategoryName;

                    foreach (var tone in toneCategory.Tones)
                        lTone.Tones.Add(new Tone
                        {
                            ToneId = tone.ToneId,
                            ToneName = tone.ToneName,
                            Score = tone.Score
                        });

                    lDocumentTone.ToneCategories.Add(lTone);
                }

            var lSetencesTones = new List<SentencesTone>();
            if (pResult?.SentencesTone != null)
                foreach (var lItem in pResult.SentencesTone)
                {
                    var lSetenceTone = new SentencesTone();
                    lSetenceTone.Text = lItem.Text;
                    lSetenceTone.InputFrom = lItem.InputFrom;
                    lSetenceTone.InputTo = lItem.InputTo;
                    lSetenceTone.SentenceId = lItem.SentenceId;

                    foreach (var lItemToneCategory in lItem?.ToneCategories)
                    {
                        var lTone = new ToneCategories();
                        lTone.CategoryId = lItemToneCategory.CategoryId;
                        lTone.CategoryName = lItemToneCategory.CategoryName;

                        foreach (var tone in lItemToneCategory.Tones)
                            lTone.Tones.Add(new Tone
                            {
                                ToneId = tone.ToneId,
                                ToneName = tone.ToneName,
                                Score = tone.Score
                            });

                        lSetenceTone.ToneCategories.Add(lTone);
                    }
                    lSetencesTones.Add(lSetenceTone);
                }
            else
                return null;

            lResultadoTone.DocumentTone = lDocumentTone;
            lResultadoTone.SetenceTones.AddRange(lSetencesTones);

            return lResultadoTone;
        }
    }
}