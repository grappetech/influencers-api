using System.ComponentModel.DataAnnotations;

namespace Action.Models
{
    public class Permition
    {
        public Permition()
        {
        }

        [Key]
        public int Id
        {
            get;
            set;
        }

        public string ActionName
        {
            get;
            set;
        }

        public EPermition ActionPermition
        {
            get;
            set;
        } = EPermition.None;
    }
}
