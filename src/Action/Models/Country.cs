using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Action.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public List<State> States { get; set; }
    }
}