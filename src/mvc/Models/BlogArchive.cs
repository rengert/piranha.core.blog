using Piranha.AttributeBuilder;
using Piranha.Models;

namespace Blog.Models
{
    [PageType(Title = "Blog archive")]
    public class BlogArchive  : BlogPage<BlogArchive>
    {
    }
}