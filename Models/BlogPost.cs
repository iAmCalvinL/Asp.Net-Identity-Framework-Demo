using System.ComponentModel.DataAnnotations;

namespace identity_demo.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }
        public required DateOnly Date { get; set; }
        public required string Text { get; set; }
    }
}
