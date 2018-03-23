using System.ComponentModel.DataAnnotations;

namespace Action.Models.Core
{
    public class Permition
    {
        [Key]
        public int Id { get; set; }

        public string ActionName { get; set; }

        public EPermition ActionPermition { get; set; } = EPermition.None;
    }
}