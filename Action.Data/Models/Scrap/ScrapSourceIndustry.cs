using Action.Data.Models.Core.Watson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Action.Data.Models.Core.Scrap
{
    public class ScrapSourceIndustry
    {
        public int IndustryId { get; set; }
        public int ScrapSourceId { get; set; }

        public Industry Industry { get; set; }
        public ScrapSource ScrapSource { get; set; }
    }
}
