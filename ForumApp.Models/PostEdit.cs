using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Models
{
    public class PostEdit
    {
        public int PostId { get; set; }
        public int ThreadId { get; set; }
        public string Content { get; set; }
    }
}
