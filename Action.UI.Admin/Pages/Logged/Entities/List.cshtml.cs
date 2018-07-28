using Action.Data.Models.Core.Watson;
using Action.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace ActionUI.Admin.Pages.Logged.Entities
{
    public class ListModel : PageModel
    {
        private readonly EntityService _entityService;
        public ListModel(EntityService entityService)
        {
            _entityService = entityService;
            this.WattsonEntities = new List<Entity>();
        }

        public List<Entity> WattsonEntities { get; set; }

        public void OnGet()
        {
            this.WattsonEntities = this._entityService.Get().OrderByDescending(e=>e.Id).ToList();
        }
    }
}