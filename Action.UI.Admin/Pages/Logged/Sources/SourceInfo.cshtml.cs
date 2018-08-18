using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Action.Data.Context;
using Action.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ActionUI.Admin.Pages.Logged.Sources
{
    public class SourceInfoModel : PageModel
    {

        private readonly SourceService _sourceService;
        private readonly IndustryService _industryService;
        private readonly ApplicationDbContext _applicationDbContext;
        public SourceInfoModel(SourceService sourceService, IndustryService industryService, ApplicationDbContext context)
        {
            _sourceService = sourceService;
            _industryService = industryService;
            _applicationDbContext = context;
        }


        public IActionResult OnGet(int id)
        {
            object result = new { Status = false };
            var scrapSource = this._sourceService.GetDetailed(id);

            if (scrapSource != null)
            {
                var config = new Newtonsoft.Json.JsonSerializerSettings();
                config.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                
                string js = Newtonsoft.Json.JsonConvert.SerializeObject(scrapSource, config);
                result = new { Status = true, Data = scrapSource };
            }

            return new JsonResult(result);


        }
    }
}