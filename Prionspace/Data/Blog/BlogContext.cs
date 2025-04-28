using Microsoft.EntityFrameworkCore;

namespace Prionspace.Data.Blog
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }
        public DbSet<BlogUser> BlogUsers { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPostComment> BlogPostComments { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogPostCategory> BlogPostCategories { get; set; }
        public DbSet<BlogLink> BlogLinks { get; set; }

        public DbSet<BlogPostView> BlogPostView { get; set; }
    }
}