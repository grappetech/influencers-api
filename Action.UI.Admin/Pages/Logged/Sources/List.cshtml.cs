﻿using Action.Data.Models.Core.Scrap;
using Action.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace ActionUI.Admin.Pages.Logged.Sources
{
    public class ListModel : PageModel
    {
        private readonly SourceService _sourceService;
        public ListModel(SourceService sourceService)
        {
            _sourceService = sourceService;
            this.ScrapSourceList = new List<ScrapSource>();
        }

        public List<ScrapSource> ScrapSourceList { get; set; }

        public void OnGet()
        {            
            this.ScrapSourceList = this._sourceService.Get().OrderByDescending(s=>s.Id).ToList();
        }

      
       

    }
}