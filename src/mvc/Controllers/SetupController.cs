using Microsoft.AspNetCore.Mvc;
using Piranha;
using System;

namespace Blog.Controllers
{
    /// <summary>
    /// This controller is only used when the project is first started
    /// and no pages has been added to the database. Feel free to remove it.
    /// </summary>
    public class SetupController : Controller
    {
        private readonly IApi api; 

        public SetupController(IApi api) {
            this.api = api;
        }

        [Route("/")]
        public IActionResult Index() {
            return View();
        }

        [Route("/seed")]
        public IActionResult Seed() {
            // Get the default site
            var site = api.Sites.GetDefault();

            // Add media assets
            var bannerId = Guid.NewGuid();

            using (var stream = System.IO.File.OpenRead("seed/pexels-photo-355423.jpeg")) {
                api.Media.Save(new Piranha.Models.StreamMediaContent() {
                    Id = bannerId,
                    Filename = "pexels-photo-355423.jpeg",
                    Data = stream
                });
            }

            // Add the blog archive
            var blogId = Guid.NewGuid();
            var blogPage = Models.BlogArchive.Create(api);
            blogPage.Id = blogId;
            blogPage.SiteId = site.Id;
            blogPage.Title = "Blog Archive";
            blogPage.MetaKeywords = "Inceptos, Tristique, Pellentesque, Lorem, Vestibulum";
            blogPage.MetaDescription = "Morbi leo risus, porta ac consectetur ac, vestibulum at eros.";
            blogPage.NavigationTitle = "Blog";
            blogPage.Published = DateTime.Now;

            api.Pages.Save(blogPage);

            // Add a blog post
            var post = Models.BlogPost.Create(api);
            post.BlogId = blogPage.Id;
            post.Category = "Uncategorized";
            post.Tags.Add("Ornare", "Pellentesque", "Fringilla Ridiculus");  
            post.Title = "Dapibus Cursus Justo";
            post.MetaKeywords = "Nullam, Mollis, Cras, Sem, Ipsum";
            post.MetaDescription = "Aenean lacinia bibendum nulla sed consectetur.";
            post.Body = "Donec sed odio dui. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum.";
            post.Heading.PrimaryImage = bannerId;
            post.Heading.Ingress = "Donec sed odio dui. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Cras mattis consectetur purus sit amet fermentum.";
            post.Published = DateTime.Now;
            api.Posts.Save(post);
            
            // Add another blog post
            post = Models.BlogPost.Create(api);
            post.BlogId = blogPage.Id;
            post.Category = "Uncategorized";
            post.Tags.Add("Ornare", "Pellentesque");  
            post.Title = "Fringilla Aenean Commodo";
            post.MetaKeywords = "Mattis, Tristique, Parturient, Fringilla";
            post.MetaDescription = "Aenean lacinia bibendum nulla sed consectetur.";
            post.Body = 
                "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Etiam porta sem malesuada magna mollis euismod. Nullam quis risus eget urna mollis ornare vel eu leo. Aenean lacinia bibendum nulla sed consectetur.\n\n" +
                "> Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec id elit non mi porta gravida at eget metus. Praesent commodo cursus magna, vel scelerisque nisl consectetur et.\n\n" +
                "Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit. Etiam porta sem malesuada magna mollis euismod. Aenean lacinia bibendum nulla sed consectetur.\n\n" + 
                "* Fringilla Adipiscing Nibh\n" +
                "* Porta Condimentum\n" +
                "* Ullamcorper Nullam\n" +
                "* Dapibus Ornare\n\n" +
                "Donec ullamcorper nulla non metus auctor fringilla. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus.\n\n" +
                "1. Dapibus Egestas Risus\n" +
                "2. Sit Nibh\n" +
                "3. Justo Mollis\n" +
                "4. Vestibulum Pellentesque\n\n" +
                "Curabitur blandit tempus porttitor. Donec sed odio dui. Maecenas faucibus `mollis interdum`. Cras justo odio, dapibus ac facilisis in, egestas eget quam. Nulla vitae elit libero, a pharetra augue. Nullam **id dolor** id nibh ultricies vehicula ut id elit.\n\n" +
                "    Sem Ipsum Fermentum {\n" +
                "        Sem Condimentum\n" +
                "        Ridiculus Quam Ornare\n" +
                "    }\n\n" +
                "Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Praesent commodo cursus magna, vel scelerisque nisl consectetur et. Curabitur blandit tempus porttitor. Sed posuere consectetur est at lobortis. Vestibulum id ligula porta felis euismod semper. Cras mattis consectetur purus sit amet fermentum. Praesent commodo cursus magna, vel scelerisque nisl consectetur et.\n";
            post.Heading.PrimaryImage = bannerId;
            post.Heading.Ingress = "Donec sed odio dui. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Cras mattis consectetur purus sit amet fermentum.";
            post.Published = DateTime.Now;
            api.Posts.Save(post);

            // Add about page
            var page = Models.StandardPage.Create(api);
            page.SiteId = site.Id;
            page.SortOrder = 1;
            page.Title = "About Me";
            page.MetaKeywords = "Inceptos, Tristique, Pellentesque, Lorem, Vestibulum";
            page.MetaDescription = "Morbi leo risus, porta ac consectetur ac, vestibulum at eros.";
            page.Body = "Cras mattis consectetur purus sit amet fermentum. Nullam id dolor id nibh ultricies vehicula ut id elit.";
            page.Published = DateTime.Now;
            api.Pages.Save(page);

            return Redirect("~/");
        }
    }
}
