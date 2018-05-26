using Microsoft.AspNetCore.Mvc;
using Piranha;
using Piranha.Extend.Blocks;
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

            var media = api.Media.GetById(bannerId);

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
            post.Heading.PrimaryImage = bannerId;
            post.Heading.Ingress = "<p>Donec sed odio dui. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Cras mattis consectetur purus sit amet fermentum.</p>";
            post.Blocks.Add(new HtmlBlock {
                Body = "<p>Maecenas sed diam eget risus varius blandit sit amet non magna. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum. Cras justo odio, dapibus ac facilisis in, egestas eget quam. Praesent commodo cursus magna, vel scelerisque nisl consectetur et. Nullam id dolor id nibh ultricies vehicula ut id elit. Curabitur blandit tempus porttitor. Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit.</p>"
            });
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
            post.Heading.PrimaryImage = bannerId;
            post.Heading.Ingress = "<p>Donec sed odio dui. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Cras mattis consectetur purus sit amet fermentum.</p>";
            post.Blocks.Add(new HtmlBlock {
                Body = "<p>Cras justo odio, dapibus ac facilisis in, egestas eget quam. Etiam porta sem malesuada magna mollis euismod. Nullam quis risus eget urna mollis ornare vel eu leo. Aenean lacinia bibendum nulla sed consectetur.</p>"
            });
            post.Blocks.Add(new QuoteBlock {
                Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec id elit non mi porta gravida at eget metus. Praesent commodo cursus magna, vel scelerisque nisl consectetur et."
            });
            post.Blocks.Add(new HtmlBlock {
                Body = "<p>Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit. Etiam porta sem malesuada magna mollis euismod. Aenean lacinia bibendum nulla sed consectetur.</p>" +
                    "<ul>" +
                    "  <li>Fringilla Adipiscing Nibh</li>" +
                    "  <li>Porta Condimentum</li>" +
                    "  <li>Ullamcorper Nullam</li>" +
                    "  <li>Dapibus Ornare</li>" +
                    "</ul>"
            });
            post.Blocks.Add(new HtmlBlock {
                Body = "<p>Donec ullamcorper nulla non metus auctor fringilla. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus.</p>" +
                    "<ol>" +
                    "  <li>Dapibus Egestas Risus</li>" +
                    "  <li>Sit Nibh</li>" +
                    "  <li>Justo Mollis</li>" +
                    "  <li>Vestibulum Pellentesque</li>" +
                    "</ol>"
            });
            post.Blocks.Add(new HtmlBlock {
                Body =
                    "<pre><code>" +
                    "    Sem Ipsum Fermentum {\n" +
                    "        Sem Condimentum\n" +
                    "        Ridiculus Quam Ornare\n" +
                    "    }" +
                    "</code></pre>"
            });
            post.Published = DateTime.Now;
            api.Posts.Save(post);

            // Add about page
            var page = Models.StandardPage.Create(api);
            page.SiteId = site.Id;
            page.SortOrder = 1;
            page.Title = "About Me";
            page.MetaKeywords = "Inceptos, Tristique, Pellentesque, Lorem, Vestibulum";
            page.MetaDescription = "Morbi leo risus, porta ac consectetur ac, vestibulum at eros.";
            page.Blocks.Add(new HtmlBlock {
                Body = "<p>Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Praesent commodo cursus magna, vel scelerisque nisl consectetur et. Donec sed odio dui. Vestibulum id ligula porta felis euismod semper. Nulla vitae elit libero, a pharetra augue. Donec id elit non mi porta gravida at eget metus. Donec ullamcorper nulla non metus auctor fringilla.</p>"
            });
            page.Blocks.Add(new QuoteBlock {
                Body = "Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod."
            });
            page.Blocks.Add(new HtmlColumnBlock {
                Column1 = $"<p><img src=\"{media.PublicUrl.Replace("~", "")}\"></p>",
                Column2 = "<p>Maecenas faucibus mollis interdum. Aenean lacinia bibendum nulla sed consectetur. Integer posuere erat a ante venenatis dapibus posuere velit aliquet.</p>"
            });
            page.Published = DateTime.Now;
            api.Pages.Save(page);

            return Redirect("~/");
        }
    }
}
