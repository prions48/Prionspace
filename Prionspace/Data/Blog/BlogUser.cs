using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prionspace.Data.Users;

namespace Prionspace.Data.Blog
{
    public class BlogUser
    {
        [Key] public Guid UserID { get; set; }
        public string UserName { get; set; } = "";
        public string UserEmail { get; set; } = "";
        public string UserSlug { get; set; } = "";
        public int MaxBlogs { get; set; } = 2;
        [NotMapped] public List<BlogLink> Links { get; set; } = [];
        public BlogUser(Auth0User user)
        {
            UserID = user.UserID;
            UserName = user.UserName ?? "";
            UserEmail = user.EmailAddress ?? "";
        }
        public bool IsValidUser
        {
            get
            {
                return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(UserEmail);
            }
        }
        public BlogUser()
        {
            //for db
        }
    }
}