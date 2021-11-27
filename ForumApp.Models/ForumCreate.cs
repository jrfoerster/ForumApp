using System.ComponentModel.DataAnnotations;

namespace ForumApp.Models
{
    public class ForumCreate
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
