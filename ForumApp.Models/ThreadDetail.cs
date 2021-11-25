using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Models
{
    public class ThreadDetail
    {
        public int ThreadId { get; set; }
        public int ForumId { get; set; }
        public string ForumName { get; set; }
        public string Title { get; set; }
        public bool IsBookmarked { get; set; }
        public List<PostListItem> Posts { get; set; }
    }
}
