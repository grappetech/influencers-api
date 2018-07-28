using Action.Data.Models.Core;
using ActionUI.Admin.ViewModel.Industry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionUI.Admin.ViewModel.SourceScrap
{
    public class SourceScrapViewModel
    {

        public SourceScrapViewModel()
        {
            this.DicPageStaus = new Dictionary<int, string>();
            DicPageStaus.Add((int)EPageStatus.Disabled, "Desabilitado");
            DicPageStaus.Add((int)EPageStatus.Enabled, "Habilitado");
            DicPageStaus.Add((int)EPageStatus.Error, "Erro");
        }

        public int Id { get; set; }

        public string Alias { get; set; }
        public string Url { get; set; }
        public string StarTag { get; set; }
        public string EndTag { get; set; }
        public int Limit { get; set; }
        public int Dept { get; set; } = 3;
        public EPageStatus PageStatus { get; set; } = EPageStatus.Enabled;


        public Dictionary<int, string> DicPageStaus { get; set; }

        public List<IndustryViewModel> Industries { get; set; } = new List<IndustryViewModel>();
        public string SelectedIndustries { get; set; }

        public List<string> ListTags { get; set; } = new List<string>();


    }
}
