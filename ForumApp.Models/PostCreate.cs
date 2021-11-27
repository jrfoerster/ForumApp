using System.ComponentModel.DataAnnotations;

namespace ForumApp.Models
{
    public class PostCreate
    {
        [Required]
        public int ThreadId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
