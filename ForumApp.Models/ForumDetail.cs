using System.Collections.Generic;

namespace ForumApp.Models
{
    public class ForumDetail
    {
        public int ForumId { get; set; }
        public string Name { get; set; }
        public List<ThreadListItem> Threads { get; set; }
    }
}
