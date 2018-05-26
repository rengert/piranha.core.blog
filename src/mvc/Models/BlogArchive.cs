using Piranha.AttributeBuilder;
using Piranha.Models;

namespace BlogTemplate.Models
{
    [PageType(Title = "Blog archive", UseBlocks = false)]
    public class BlogArchive  : BlogPage<BlogArchive>
    {
    }
}