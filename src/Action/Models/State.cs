using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Action.Models
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
        public List<City> Cities { get; set; }
    }
}