using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Piranha;

namespace Blog.Pages
{
    public class Post : PageModel
    {
        private readonly IApi _api;

        public Models.BlogPost Data { get; private set; }

        public Post(IApi api)
        {
            _api = api;
        }

        public void OnGet(Guid id)
        {
            Data = _api.Posts.GetById<Models.BlogPost>(id);
            ViewData["CurrentPage"] = Data.BlogId;
        }
    }   
}