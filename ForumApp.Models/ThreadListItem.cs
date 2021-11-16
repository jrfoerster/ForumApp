using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Models
{
    public class ThreadListItem
    {
        public int ThreadId { get; set; }
        public string Title { get; set; }
        public int PostCount { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset LastPostUtc { get; set; }
        public bool IsEditable { get; set; }
    }
}
