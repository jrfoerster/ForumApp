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
        public string Title { get; set; }
        public List<PostListItem> Posts { get; set; }
    }
}
