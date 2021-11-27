using System.ComponentModel.DataAnnotations;

namespace ForumApp.Models
{
    public class ThreadCreate
    {
        [Required]
        public int ForumId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Post Content")]
        public string PostContent { get; set; }
    }
}
