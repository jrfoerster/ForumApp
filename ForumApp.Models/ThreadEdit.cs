using System.ComponentModel.DataAnnotations;

namespace ForumApp.Models
{
    public class ThreadEdit
    {
        public int ForumId { get; set; }

        [Display(Name = "Forum Name")]
        public string ForumName { get; set; }

        public int ThreadId { get; set; }

        [Display(Name = "Thread Title")]
        public string ThreadTitle { get; set; }
    }
}
