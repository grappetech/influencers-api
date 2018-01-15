using System.ComponentModel.DataAnnotations;

namespace Action.VewModels
{
    public class AddEntityViewModel
    {
        //TODO: Adicionado as Propriedades imageUrl e industryId
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome não informado.")]
        public string Name { get; set; }

        public string Alias { get; set; }
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Categoria não informada.")]
        public string Category { get; set; }

        public string Date { get; set; }
        public string FacebookUser { get; set; }
        public string TweeterUser { get; set; }
        public string InstagranUser { get; set; }
        public string YoutuberUser { get; set; }
        public string PictureUrl { get; set; }
        public string SiteUrl { get; set; }
    }
}