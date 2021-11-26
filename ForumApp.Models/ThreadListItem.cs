using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Models
{
    public class ThreadListItem
    {
        public int ThreadId { get; set; }
        public string Title { get; set; }

        [Display(Name = "Posts")]
        public int PostCount { get; set; }

        [Display(Name = "Author")]
        public string UserName { get; set; }

        public DateTimeOffset LastPostUtc { get; set; }
        public string LastPostUserName { get; set; }
        public bool IsEditable { get; set; }
        public bool IsBookmarked { get; set; }

    }
}
