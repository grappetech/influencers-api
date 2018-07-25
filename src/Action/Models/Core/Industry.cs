using Action.Models.Scrap;
using System.Collections.Generic;
using Entity = Action.Models.Watson.Entity;

namespace Action.Models.Core
{
	public class Industry
	{
		public int Id { get; set; }
		public string Name { get; set; }


        public virtual ICollection<Entity> Entities { get; set; }
        public virtual ICollection<ScrapSourceIndustry> ScrapSources { get; set; }
    }
}