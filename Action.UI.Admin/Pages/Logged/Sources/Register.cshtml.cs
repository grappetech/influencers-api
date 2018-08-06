using Action.Data.Context;
using Action.Data.Models.Core.Scrap;
using Action.Services;
using ActionUI.Admin.ViewModel.SourceScrap;
using ActionUI.Admin.ViewModel.Industry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ActionUI.Admin.Pages.Logged.Sources
{
    public class RegisterModel : PageModel
    {

        private readonly SourceService _sourceService;
        private readonly IndustryService _industryService;
        private readonly ApplicationDbContext _applicationDbContext;
        public RegisterModel(SourceService sourceService, IndustryService industryService, ApplicationDbContext context)
        {
            _sourceService = sourceService;
            _industryService = industryService;
            _applicationDbContext = context;
        }

        [BindProperty]
        public SourceScrapViewModel SourceScrap { get; set; } = new SourceScrapViewModel();

        public void OnGet(int id)
        {
            this.SourceScrap.Industries = this._industryService.Get().Select(x => new IndustryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Selected = false

            }).ToList();

            if (id > 0)
            {
                var entity = this._sourceService.Get(id);
                this.SourceScrap = parseToViewModel(entity);

                //marcar os que ja estiverem marcados
                if (entity.Industries != null)
                {
                    var industriesId = entity.Industries.Select(x => x.IndustryId).ToList();
                    this.SourceScrap.Industries.Where(i => industriesId.Any(selectedIndustryId => selectedIndustryId == i.Id)).ToList().ForEach(i => i.Selected = true);

                }

            }

            this.SourceScrap.ListTags.AddRange(this.ListHtmlTags());

        }

        private SourceScrapViewModel parseToViewModel(Action.Data.Models.Core.Scrap.ScrapSource source)
        {

            if (source == null)
                return new SourceScrapViewModel();


            if (this.SourceScrap == null)
                this.SourceScrap = new SourceScrapViewModel();


            this.SourceScrap.Id = source.Id;
            this.SourceScrap.Alias = source.Alias;
            this.SourceScrap.Dept = source.Dept;
            this.SourceScrap.EndTag = source.EndTag;
            this.SourceScrap.StarTag = source.StarTag;
            this.SourceScrap.PageStatus = source.PageStatus;
            this.SourceScrap.Url = source.Url;
            this.SourceScrap.TagException = source.TagException.Replace("|", "\n");
            
            return this.SourceScrap;

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                var sourceModel = parseToModel();

                List<int> listIdSources = sourceModel.Industries.Select(s => s.ScrapSourceId).ToList();
                long souceId = await this._sourceService.Save(sourceModel);

                return RedirectToPage("Register", new { id = sourceModel.Id });

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            //return RedirectToPage("/Index");
        }

        private Action.Data.Models.Core.Scrap.ScrapSource parseToModel()
        {
            var sourceScarp = new Action.Data.Models.Core.Scrap.ScrapSource
            {
                Id = this.SourceScrap.Id,
                Alias = this.SourceScrap.Alias,
                Dept = this.SourceScrap.Dept,
                StarTag = this.SourceScrap.StarTag,
                EndTag = this.SourceScrap.EndTag,
                Limit = this.SourceScrap.Limit,
                TagException = this.SourceScrap.TagException.Replace(" ","|"),
                PageStatus = this.SourceScrap.PageStatus,
                Url = this.SourceScrap.Url
            };


            if (!string.IsNullOrEmpty(this.SourceScrap.SelectedIndustries))
            {
                string[] arrIndustries = this.SourceScrap.SelectedIndustries.Split(';');
                if (arrIndustries != null && arrIndustries.Count() > 0)
                {
                    int industryId;

                    foreach (string _industryId in arrIndustries)
                    {
                        if (int.TryParse(_industryId, out industryId))
                        {
                            sourceScarp.Industries.Add(new ScrapSourceIndustry
                            {
                                IndustryId = industryId
                            });

                            industryId = 0;
                        }
                    }

                }
            }


            return sourceScarp;
        }

        private List<string> ListHtmlTags()
        {

            List<string> tags = new List<string> {

                "DOCTYPE",
                "a",
                "abbr",
                "acronym",
                "address",
                "applet",
                "area",
                "article",
                "aside",
                "audio",
                "b",
                "base",
                "basefont",
                "bdi",
                "bdo",
                "big",
                "blockquote",
                "body",
                "br",
                "button",
                "canvas",
                "caption",
                "center",
                "cite",
                "code",
                "col",
                "colgroup",
                "command",
                "datalist",
                "dd",
                "del",
                "details",
                "dfn",
                "dir",
                "div",
                "dl",
                "dt",
                "em",
                "embed",
                "fieldset",
                "figcaption",
                "figure",
                "font",
                "footer",
                "form",
                "frame",
                "frameset",
                "h1-<h6>",
                "head",
                "header",
                "hgroup",
                "hr",
                "html",
                "i",
                "iframe",
                "img",
                "input",
                "ins",
                "kbd",
                "keygen",
                "label",
                "legend",
                "li",
                "link",
                "map",
                "mark",
                "menu",
                "meta",
                "meter",
                "nav",
                "noframes",
                "noscript",
                "object",
                "ol",
                "optgroup",
                "option",
                "output",
                "p",
                "param",
                "pre",
                "progress",
                "q",
                "rp",
                "rt",
                "ruby",
                "s",
                "samp",
                "script",
                "section",
                "select",
                "small",
                "source",
                "span",
                "strike4",
                "strong",
                "style",
                "sub",
                "summary",
                "sup",
                "table",
                "tbody",
                "td",
                "textarea",
                "tfoot",
                "th",
                "thead",
                "time",
                "title",
                "tr",
                "track",
                "tt",
                "u",
                "ul",
                "var",
                "video5",
                "wbr"

            };

            return tags;




        }
    }
}