using System.ComponentModel.DataAnnotations;
using Action.Models.Core;

namespace Action.Models.Scrap
{
    public class ScrapSource
    {
        [Key]
        public int Id { get; set; }

        public string Alias { get; set; }
        public string Url { get; set; }
        public string StarTag { get; set; }
        public string EndTag { get; set; }
        public int Limit { get; set; }
        public int Dept { get; set; } = 3;
        public EPageStatus PageStatus { get; set; } = EPageStatus.Enabled;
    }
}