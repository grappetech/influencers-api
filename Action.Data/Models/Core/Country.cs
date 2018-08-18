using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Action.Data.Models.Core
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
    }
}