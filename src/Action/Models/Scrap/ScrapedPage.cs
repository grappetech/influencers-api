using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Action.Extensions;
using Action.Models.Core;
using Microsoft.AspNetCore.WebUtilities;

namespace Action.Models.Scrap
{
    public class ScrapedPage
    {
        private string _text;

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Url { get; set; }

        public string Text
        {
            get => _text;
            set
            {
                _text = value.RemoveCaracters('\t');
                _text = _text.Replace('\r', ' ');
                _text = _text.Replace("  ", string.Empty);
                Hash = Base64UrlTextEncoder.Encode(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(_text)));
            }
        }

        public string Hash { get; private set; }
        public string Translated { get; set; }
        public EDataExtractionStatus Status { get; set; } = EDataExtractionStatus.Waiting;
    }
}