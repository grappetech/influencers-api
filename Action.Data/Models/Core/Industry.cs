using Action.Data.Models.Core.Scrap;
using System.Collections.Generic;
using Entity = Action.Data.Models.Watson;

namespace Action.Data.Models.Core.Watson
{
	public class Industry
	{
		public int Id { get; set; }
		public string Name { get; set; }


        public virtual ICollection<Entity> Entities { get; set; }
        public virtual List<ScrapSourceIndustry> ScrapSources { get; set; }
    }
}