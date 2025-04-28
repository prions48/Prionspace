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
                blog.Categories = _context.BlogCategories.Where(e => e.BlogID == blog.ID).ToList();
                blog.Tags = _context.BlogTags.Where(e => e.BlogID == blog.ID).ToList();
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
                post.Categories = LoadCategories(post.ID);
            }
            return post;
        }
        private List<BlogCategory> LoadCategories(Guid postid)
        {
            List<BlogPostCategory> pcs = _context.BlogPostCategories.Where(e => e.PostID == postid).ToList();
            return _context.BlogCategories.Where(e => pcs.Select(f => f.CategoryID).Contains(e.ID)).ToList();
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
        public void CreatePostCategory(BlogPostCategory pc)
        {
            _context.BlogPostCategories.Add(pc);
            _context.SaveChanges();
        }
        public void DeletePostCategory(Guid postid, Guid catid)
        {
            List<BlogPostCategory> pcs = _context.BlogPostCategories.Where(e => e.CategoryID == catid && e.PostID == postid).ToList();
            foreach (BlogPostCategory cat in pcs)
            {
                _context.BlogPostCategories.Remove(cat);
            }
            _context.SaveChanges();
        }
        public void CreateCategory(BlogCategory category)
        {
            _context.BlogCategories.Add(category);
            _context.SaveChanges();
        }
        public void DeleteCategory(BlogCategory category)
        {
            _context.BlogCategories.Remove(category);
            _context.SaveChanges();
        }
        public void CreateTag(BlogTag tag)
        {
            _context.BlogTags.Add(tag);
            _context.SaveChanges();
        }
        public void DeleteTag(BlogTag tag)
        {
            _context.BlogTags.Remove(tag);
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