using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Action.Data.Models.Core
{
    public class State
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(2)]
        public string Code { get; set; }
        [MaxLength(180)]
        public string Name { get; set; }

        public Country Country { get; set; }
    }
}