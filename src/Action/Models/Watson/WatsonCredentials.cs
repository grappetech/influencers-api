using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Action.Models.Watson
{
    public class WatsonCredentials
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public EWatsonServices Service { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Version { get; set; }
        public string Model { get; set; }
    }
}