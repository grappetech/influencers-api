using Action.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ActionUI.Admin.Pages.Sources
{
    public class RegisterModel : PageModel
    {



        private readonly SourceService _sourceService;
        public RegisterModel(SourceService sourceService)
        {
            
            _sourceService = sourceService;
            string userSession = HttpContext?.Session.GetString("userSession");

        }




        [BindProperty]
        public ViewModel.SourceScrapViewModel SourceScrap { get; set; } = new ViewModel.SourceScrapViewModel();

        public void OnGet(int id)
        {
            string userSession = HttpContext.Session.GetString("userSession");
            if (id > 0)
            {
                var entity = this._sourceService.Get(id);
                this.SourceScrap = parseToViewModel(entity);
            }

            this.SourceScrap.ListTags.AddRange(this.ListHtmlTags());

        }

        private ViewModel.SourceScrapViewModel parseToViewModel(Action.Data.Models.Core.Scrap.ScrapSource source)
        {

            if (source == null)
                return new ViewModel.SourceScrapViewModel();

            return new ViewModel.SourceScrapViewModel()
            {
                Id = source.Id,

                Alias = source.Alias,
                Dept = source.Dept,
                EndTag = source.EndTag,
                StarTag = source.StarTag,
                PageStatus = source.PageStatus,
                Url = source.Url
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                var sourceModel = parseToModel();

                long souceId =await this._sourceService.Save(sourceModel);

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
            return new Action.Data.Models.Core.Scrap.ScrapSource
            {
                Id = this.SourceScrap.Id,
                Alias = this.SourceScrap.Alias,
                Dept = this.SourceScrap.Dept,
                StarTag= this.SourceScrap.StarTag,
                EndTag = this.SourceScrap.EndTag,
                Limit = this.SourceScrap.Limit,
                PageStatus = this.SourceScrap.PageStatus,
                Url = this.SourceScrap.Url
            };
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