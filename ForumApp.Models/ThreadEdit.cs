using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
