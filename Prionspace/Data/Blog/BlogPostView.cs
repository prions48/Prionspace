using Microsoft.EntityFrameworkCore;

namespace Prionspace.Data.Blog
{
    [Keyless]
    public class BlogPostView
    {
        public string? BlogSlug { get; set; }
        public Guid BlogID { get; set; }
        public string? PostSlug { get; set; }
        public string? BlogTitle { get; set; }
        public Guid PostID { get; set; }
        public string? PostTitle { get; set; }
        public string? PostBody { get; set; }
        public DateTime? PostDate { get; set; }
        public string? Categories { get; set; }
        public string? TagRecord { get; set; }
        public string? UserName { get; set; }
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
        public string BlogURL
        {
            get
            {
                if (!String.IsNullOrEmpty(BlogSlug))
                    return BlogSlug;
                return BlogID.ToString();
            }
        }
        public string PostURL
        {
            get
            {
                if (!string.IsNullOrEmpty(PostSlug))
                    return PostSlug;
                return PostID.ToString();
            }
        }
    }
}