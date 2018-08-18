using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Action.Data.Models.Core.Scrap
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
        [MaxLength(1000)]
        [Column(TypeName = "varchar(1000)")]
        public string TagException { get; set; }
        public EPageStatus PageStatus { get; set; } = EPageStatus.Enabled;   
        public virtual ICollection<ScrapSourceIndustry> Industries { get; set; } = new List<ScrapSourceIndustry>();
        public virtual ICollection<ScrapSourceEntity> Entities { get; set; }
    }
}