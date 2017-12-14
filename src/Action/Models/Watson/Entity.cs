using System;
using System.ComponentModel.DataAnnotations;

namespace Action.Models.Watson
{
    public class Entity
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Alias { get; set; }
        public ECategory CategoryId { get; set; }
        public string Category => Enum.GetName(typeof(ECategory), CategoryId);
        public DateTime Date { get; set; } = DateTime.Today;
        public string FacebookUser { get; set; }
        public string TweeterUser { get; set; }
        public string InstagranUser { get; set; }
        public string YoutubeUser { get; set; }

        private string _pictureUrl;
        public string PictureUrl
        {
            get
            {
                if (String.IsNullOrEmpty(_pictureUrl))
                    return "https://cdn1.iconfinder.com/data/icons/social-messaging-productivity-1-1/128/gender-male2-512.png";
                else
                    return _pictureUrl;
            }
            set { _pictureUrl = value; }
        }
        public string SiteUrl { get; set; }
    }
}