using System.ComponentModel.DataAnnotations;

namespace Prionspace.Data.Blog
{
    public class BlogLink
    {
        [Key] public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid BlogID { get; set; }
        public string BlogSlug { get; set; }
        public string LinkName { get; set; }
        public bool OwnBlog { get; set; }
        public int OrderNumber { get; set; }
        public BlogLink()
        {
            ID = Guid.NewGuid();
        }
    }
}