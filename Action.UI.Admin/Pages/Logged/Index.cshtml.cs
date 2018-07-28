using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Action.Services;
using ActionUI.Admin.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ActionUI.Admin.Pages.Logged
{
    public class IndexModel : PageModel
    {

        private readonly EntityService _entityService;
        private readonly SourceService _sourceService;
        //private readonly Action.Repository.Base.IRepository<Action.Data.Models.Core.Watson.Entity> _entityRepositor;
        public IndexModel(EntityService entityService, SourceService sourceService)
        {
            _entityService = entityService;
            _sourceService = sourceService;
        }


        public IndexViewModel IndexViewModel { get; set; } = new IndexViewModel();

        public void OnGet()
        {
            //ActionUI.Admin.Pages.Entities.ListModel
            var listEntities = this._entityService.Get();
            var listSource = this._sourceService.Get();

            if (listEntities != null)
                this.IndexViewModel.TotalEntities = listEntities.Count;

            if (listSource != null)
                this.IndexViewModel.TotalScrapSources = listSource.Count;

        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }
    }
}
