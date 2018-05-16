using Piranha.AttributeBuilder;
using Piranha.Models;

namespace Blog.Models
{
    [PageType(Title = "Blog archive", UseBlocks = false)]
    public class BlogArchive  : BlogPage<BlogArchive>
    {
    }
}