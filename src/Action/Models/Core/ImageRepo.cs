using System;

namespace Action.Models.Core
{
    public class ImageRepo
    {
        public Guid Id { get; set; }
        public string Base64Image { get; set; }
        public string ImageName { get; set; }
    }
}