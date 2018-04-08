using Piranha.AttributeBuilder;
using Piranha.Extend.Fields;

namespace Blog.Models.Regions
{
    public class Heading
    {
        [Field(Title = "Primary image")]
        public ImageField PrimaryImage { get; set; }

        [Field]
        public MarkdownField Ingress { get; set; }
    }
}