using Action.Data.Models.Core;
using ActionUI.Admin.ViewModel.Industry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionUI.Admin.ViewModel
{
    public class Entity
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }

        //set selected industry
        public int CategoryId { get; set; }
        //public string Category => Enum.GetName(typeof(ECategory), CategoryId);
        public string Category { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public string FacebookUser { get; set; }
        public string TweeterUser { get; set; }
        public string InstagranUser { get; set; }
        public string YoutubeUser { get; set; }
        public string PictureUrl { get; set; }
        public string SiteUrl { get; set; }
        public int Tier { get; set; } = 3;
        public int ExecutionInterval { get; set; }

        public int IndustryId { get; set; }


        public string SelectedUnRelatedWatsonEntity { get; set; }
        public string UnSelectedUnRelatedWatsonEntity { get; set; }
        public int SelectedSourceId { get; set; }


        public List<WatsonEntityiewModel> RelatedWatsonEntity { get; set; } = new List<WatsonEntityiewModel>();
        public List<WatsonEntityiewModel> UnRelatedWatsonEntity { get; set; } = new List<WatsonEntityiewModel>();


        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>()
        {
            new CategoryViewModel{ Id = (int) ECategory.Brand, Name="Marca"},
            new CategoryViewModel{ Id = (int) ECategory.Person, Name="Pessoa"}
          //  new CategoryViewModel{ Id = (int) ECategory.Personality, Name="Personalidade"}

        };


        public List<IndustryViewModel> Industries { get; set; } = new List<IndustryViewModel>();

        public List<IndustryViewModel> SourceListIndustry { get; set; } = new List<IndustryViewModel>();
        public List<EntitySourceViewModel> EntitySourceScraps { get; set; } = new List<EntitySourceViewModel>();



        //public virtual ICollection<WatsonEntity> RelatedEntities { get; set; } = new List<WatsonEntity>();
        //public ICollection<Briefing> Briefings { get; set; } = new List<Briefing>();
    }
}
