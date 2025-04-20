using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prionspace.Data.Blog
{
    public class BlogPostComment
    {
        [Key] public Guid ID { get; set; }
        public Guid BlogPostID { get; set; }
        public Guid? ParentCommentID { get; set; }
        public string CommentUser { get; set; }
        public string CommentText { get; set; }
        public DateTime PostDate { get; set; }
        [NotMapped] public List<BlogPostComment> ChildComments { get; set; } = [];
        public BlogPostComment()
        {
            ID = Guid.NewGuid();
        }
    }
}