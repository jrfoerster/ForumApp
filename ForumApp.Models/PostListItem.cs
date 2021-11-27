using System;

namespace ForumApp.Models
{
    public class PostListItem
    {
        public int PostId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
        public bool IsEditable { get; set; }
    }
}
