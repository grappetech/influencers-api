using System;
using System.ComponentModel.DataAnnotations;
using Action.Models.Watson;

namespace Action.Models.Scrap
{
    public class Social
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public ESocialNetwork Network { get; set; } = ESocialNetwork.Twitter;
        public long EntityId { get; set; }
        public Entity Entity { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public int Followers { get; set; }
        public int Following { get; set; }
        public int Interactions { get; set; }

        public Decimal Engagement
        {
            get
            {
                var engagement = 0;

                engagement = Following > 0 && Interactions > 0 ? ((Interactions / Followers) - (Followers/Following)) : 1;
                
                return engagement;
            }
        }


    }
}