using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components;

namespace Prionspace.Data.Blog
{
    public class BlogPost
    {
        [Key] public Guid ID { get; set; }
        public Guid BlogID { get; set; }
        public string PostTitle { get; set; } = "";
        public string PostBody { get; set; } = "";
        public MarkupString PostBodyHTML
        {
            get
            {
                return (MarkupString)PostBody;
            }
        }
        public string PostBodyPreview
        {
            get
            {
                if (string.IsNullOrEmpty(PostBody))
                    return "";
                if (PostBody.Length < 50)
                    return PostBody;
                return PostBody.Substring(0,50);
            }
        }
        public string PostSlug { get; set; } = "";
        public string PostURL
        {
            get
            {
                if (!string.IsNullOrEmpty(PostSlug))
                    return PostSlug;
                return ID.ToString();
            }
        }
        public DateTime PostDate { get; set; } = DateTime.Now;
        public string PostUser { get; set; } = "";
        public PostStatus Status { get; set; } = PostStatus.Draft;
        public string? TagRecord { get; set; } = null;
        [NotMapped] public List<BlogCategory> Categories { get; set; } = [];
        public List<string> Tags
        {
            get
            {
                if (string.IsNullOrEmpty(TagRecord))
                    return new List<string>();
                return TagRecord.Split("|").ToList();
            }
        }
        [NotMapped] public List<BlogPostComment> Comments { get; set; } = [];
        public BlogPost()
        {
            ID = Guid.NewGuid();
        }
        public bool PublicViewable
        {
            get
            {
                return Status == PostStatus.Public;
            }
        }
    }
    public enum PostStatus
    {
        Draft = 0,
        Private = 1,
        Limited = 2,
        Public = 3
    }
}