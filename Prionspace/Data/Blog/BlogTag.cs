using System.ComponentModel.DataAnnotations;

namespace Prionspace.Data.Blog
{
    public class BlogTag
    {
        [Key] public Guid ID { get; set; }
        public Guid BlogID { get; set; }
        public string TagTitle { get; set; }
        public BlogTag()
        {
            ID = Guid.NewGuid();
        }
    }
}