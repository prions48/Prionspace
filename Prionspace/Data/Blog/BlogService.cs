using Prionspace.Data.Users;

namespace Prionspace.Data.Blog
{
    public class BlogService
    {
        private readonly BlogContext _context;
        public BlogService(BlogContext context)
        {
            _context = context;
        }
        #region page load functions
        public List<Blog> GetBlogsByUser(Guid userid)//when loading user page
        {
            List<Blog> blogs = _context.Blogs.Where(e => e.UserID == userid).ToList();
            foreach (Blog blog in blogs)
            {
                blog.Posts = _context.BlogPosts.Where(e => e.BlogID == blog.ID).ToList();
            }
            return blogs;
        }
        public Blog? GetBlogByURL(string blogid)
        {
            Blog? blog = null;
            if (Guid.TryParse(blogid, out Guid id))
            {
                blog = _context.Blogs.FirstOrDefault(e => e.ID == id);
            }
            else
            {
                blog = _context.Blogs.FirstOrDefault(e => e.BlogSlug == blogid);
            }
            if (blog != null)
            {
                blog.Posts = _context.BlogPosts.Where(e => e.BlogID == blog.ID).ToList();
            }
            return blog;
        }
        public BlogPost? GetPostByURL(string postid, BlogUser? user)//when loading post page
        {
            BlogPost? post = null;
            if (Guid.TryParse(postid, out Guid id))
            {
                post = _context.BlogPosts.FirstOrDefault(e => e.ID == id);
            }
            else
            {
                post = _context.BlogPosts.FirstOrDefault(e => e.PostSlug == postid);
            }
            if (post != null)
            {
                Blog? blog = _context.Blogs.FirstOrDefault(e => e.ID == post.BlogID);
                if (blog != null)
                {
                    if (blog.UserID != user?.UserID && !post.PublicViewable)
                        return null; //permissions
                }
                List<BlogPostComment> comments = _context.BlogPostComments.Where(e => e.BlogPostID == post.ID).ToList();
                post.Comments = LoadComments(null, comments);
            }
            return post;
        }
        private List<BlogPostComment> LoadComments(Guid? parentcommentid, List<BlogPostComment> comments)
        {
            List<BlogPostComment> ret = comments.Where(e => e.ParentCommentID == parentcommentid).ToList();
            foreach (BlogPostComment comm in ret)
            {
                comm.ChildComments = LoadComments(comm.ID, comments);
            }
            return ret;
        }
        #endregion

        #region DB writing

        #region blog
        public void CreateBlog(Blog blog)
        {
            _context.Blogs.Add(blog);
            _context.SaveChanges();
        }
        public void UpdateBlog(Blog blog)
        {
            _context.Blogs.Update(blog);
            _context.SaveChanges();
        }
        #endregion

        public void CreatePost(BlogPost post)
        {
            _context.BlogPosts.Add(post);
            _context.SaveChanges();
        }
        public void UpdatePost(BlogPost post)
        {
            _context.BlogPosts.Update(post);
            _context.SaveChanges();
        }

        #endregion

        public BlogUser GetOrCreateBlogUser(Auth0User user)
        {
            BlogUser? bloguser = _context.BlogUsers.FirstOrDefault(e => e.UserID == user.UserID);
            if (bloguser == null)
            {
                bloguser = new BlogUser(user);
                _context.BlogUsers.Add(bloguser);
                _context.SaveChanges();
            }
            return bloguser;
        }
        public void UpdateUser(BlogUser user)
        {
            _context.BlogUsers.Update(user);
            _context.SaveChanges();
        }
    }
}