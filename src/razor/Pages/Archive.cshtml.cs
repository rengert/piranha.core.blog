using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Piranha;

namespace Blog.Pages
{
    public class Archive : PageModel
    {
        private readonly IApi _api;

        public Models.BlogArchive Data { get; private set; }

        public string BlogLink {
            get {
                return Data.Permalink
                    + (Data.Archive.Category != null ? $"/category/{Data.Archive.Category.Slug}"  : "")
                    + (Data.Archive.Year.HasValue ? $"/{Data.Archive.Year}" : "" )
                    + (Data.Archive.Month.HasValue ? $"/{Data.Archive.Month}" : "");
            }
        }        

        public Archive(IApi api)
        {
            _api = api;
        }

        public void OnGet(Guid id, int? year = null, int? month = null, int? page = null, Guid? category = null)
        {
            Data = _api.Archives.GetById<Models.BlogArchive>(id, page, category, year, month);
            ViewData["CurrentPage"] = Data.Id;
        }
    }        
}