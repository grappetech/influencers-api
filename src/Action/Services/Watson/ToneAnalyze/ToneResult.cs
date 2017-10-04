using System;
using System.Collections.Generic;
using IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model;

namespace Action.Services.Watson.ToneAnalyze
{
    public class ToneResult
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DocumentTone DocumentTone { get; set; }

        private List<SentencesTone> _setenceTones = new List<SentencesTone>();

        public List<SentencesTone> SetenceTones
        {
            get { return _setenceTones; }
            set { _setenceTones = value; }
        }

        public static ToneResult Parse(ToneAnalysis pResult)
        {
            ToneResult lResultadoTone = new ToneResult();

            DocumentTone lDocumentTone = new DocumentTone();
            if (pResult.DocumentTone != null)
            {
                foreach (var toneCategory in pResult.DocumentTone.ToneCategories)
                {
                    ToneCategories lTone = new ToneCategories();
                    lTone.CategoryId = toneCategory.CategoryId;
                    lTone.CategoryName = toneCategory.CategoryName;

                    foreach (var tone in toneCategory.Tones)
                    {
                        lTone.Tones.Add(new Tone()
                        {
                            ToneId = tone.ToneId,
                            ToneName = tone.ToneName,
                            Score = tone.Score
                        });
                    }

                    lDocumentTone.ToneCategories.Add(lTone);
                }
            }

            List<SentencesTone> lSetencesTones = new List<SentencesTone>();
            foreach (var lItem in pResult.SentencesTone)
            {
                SentencesTone lSetenceTone = new SentencesTone();
                lSetenceTone.Text = lItem.Text;
                lSetenceTone.InputFrom = lItem.InputFrom;
                lSetenceTone.InputTo = lItem.InputTo;
                lSetenceTone.SentenceId = lItem.SentenceId;

                foreach (var lItemToneCategory in lItem.ToneCategories)
                {
                    ToneCategories lTone = new ToneCategories();
                    lTone.CategoryId = lItemToneCategory.CategoryId;
                    lTone.CategoryName = lItemToneCategory.CategoryName;

                    foreach (var tone in lItemToneCategory.Tones)
                    {
                        lTone.Tones.Add(new Tone()
                        {
                            ToneId = tone.ToneId,
                            ToneName = tone.ToneName,
                            Score = tone.Score
                        });
                    }

                    lSetenceTone.ToneCategories.Add(lTone);
                }
                lSetencesTones.Add(lSetenceTone);
            }

            lResultadoTone.DocumentTone = lDocumentTone;
            lResultadoTone.SetenceTones.AddRange(lSetencesTones);

            return lResultadoTone;
        }

    }
}
