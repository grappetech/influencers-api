using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Action.VewModels
{
    public class AddEntityViewModel
    {
        //TODO: Adicionado as Propriedades imageUrl e industryId
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("Name")]
        [Required(ErrorMessage = "Nome não informado.")]
        public string Name { get; set; }
        [JsonProperty("alias")]
        public string Alias { get; set; }
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("facebookUser")]
        public string FacebookUser { get; set; }
        [JsonProperty("tweeterUser")]
        public string TweeterUser { get; set; }
        [JsonProperty("instagranUser")]
        public string InstagranUser { get; set; }
        [JsonProperty("youtuberUser")]
        public string YoutuberUser { get; set; }
        [JsonProperty("pictureUrl")]
        public string PictureUrl { get; set; }
        [JsonProperty("siteUrl")]
        public string SiteUrl { get; set; }
        [JsonProperty("industryId")]
        public int industryId{ get; set; }
        [JsonProperty("imageUrl")]
        public string imageUrl { get; set; }
    }
}