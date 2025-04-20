using System.ComponentModel.DataAnnotations;

namespace Prionspace.Data.Blog
{
    public class BlogCategory
    {
        [Key] public Guid ID { get; set; }
        public Guid BlogID { get; set; }
        public string CategoryName { get; set; }
        public MudBlazor.Color Color { get; set; }
        public BlogCategory()
        {
            ID = Guid.NewGuid();
        }
    }
}