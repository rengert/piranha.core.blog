using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Piranha;

namespace Blog.Pages
{
    public class Page : PageModel
    {
        private readonly IApi _api;

        public Models.StandardPage Data { get; private set; }

        public Page(IApi api)
        {
            _api = api;
        }

        public void OnGet(Guid id)
        {
            Data = _api.Pages.GetById<Models.StandardPage>(id);
            ViewData["CurrentPage"] = Data.Id;
        }
    }  
}