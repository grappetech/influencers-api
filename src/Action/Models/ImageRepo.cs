using System;

namespace Action.Models
{
    public class ImageRepo
    {
        public Guid Id { get; set; }
        public string Base64Image { get; set; }
        public string ImageName { get; set; }
    }
}