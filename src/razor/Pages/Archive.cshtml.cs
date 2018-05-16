using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Piranha;

namespace Blog.Pages
{
    public class Archive : PageModel
    {
        private readonly IApi _api;

        public Models.BlogArchive Data { get; private set; }

        public string BlogLink 
        {
            get 
            {
                return Data.Permalink
                    + (Data.Archive.Category != null ? $"/category/{Data.Archive.Category.Slug}"  : "")
                    + (Data.Archive.Year.HasValue ? $"/{Data.Archive.Year}" : "" )
                    + (Data.Archive.Month.HasValue ? $"/{Data.Archive.Month}" : "");
            }
        }

        public string MonthName
        {
            get 
            {
                if (Data.Archive.Month.HasValue)
                    return new DateTime(2018, Data.Archive.Month.Value, 1) .ToString("MMMM", CultureInfo.InvariantCulture);
                return "";

            }
        }
        
        public Archive(IApi api)
        {
            _api = api;
        }

        public void OnGet(Guid id, int? year = null, int? month = null, int? page = null, Guid? category = null, Guid? tag = null)
        {
            if (category.HasValue)
                Data = _api.Archives.GetByCategoryId<Models.BlogArchive>(id, category.Value, page, year, month);
            else if (tag.HasValue)
                Data = _api.Archives.GetByTagId<Models.BlogArchive>(id, tag.Value, page, year, month);
            else Data = _api.Archives.GetById<Models.BlogArchive>(id, page, year, month);

            ViewData["CurrentPage"] = Data.Id;
        }
    }        
}