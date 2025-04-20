using System.ComponentModel.DataAnnotations;

namespace Prionspace.Data.Blog
{
    public class BlogPostCategory
    {
        [Key] public Guid ID { get; set; }
        public Guid PostID { get; set; }
        public Guid CategoryID { get; set; }
        public BlogPostCategory()
        {
            ID = Guid.NewGuid();
        }
    }
}