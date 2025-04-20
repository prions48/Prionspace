using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prionspace.Data.Blog
{
    public class Blog
    {
        [Key] public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public string BlogTitle { get; set; }
        public string BlogSubtitle { get; set; }
        public string BlogSlug { get; set; }
        public string BlogURL
        {
            get
            {
                if (!String.IsNullOrEmpty(BlogSlug))
                    return BlogSlug;
                return ID.ToString();
            }
        }
        [NotMapped] public List<BlogPost> Posts { get; set; } = [];
        [NotMapped] public List<BlogTag> Tags { get; set; } = [];
        [NotMapped] public List<BlogCategory> Categories { get; set; } = [];
        public Blog()
        {
            ID = Guid.NewGuid();
        }
    }
}