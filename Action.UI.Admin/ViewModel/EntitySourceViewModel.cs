using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionUI.Admin.ViewModel
{
    public class EntitySourceViewModel
    {
       

        public int Id { get; set; }
        public string Alias { get; set; }
        public string Url { get; set; } 
        public int Limit { get; set; }
        public int IndustryId { get; set; }
        public string IndustryName { get; set; }

        public bool IsSameIndustryOfEntity { get; set; }


    }
}
