using ForumApp.Data;
using ForumApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Services
{
    public class PostService
    {
        private Guid _userId;

        public PostService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePost(PostCreate model)
        {
            var now = DateTimeOffset.UtcNow;
            var post = new Post()
            {
                ThreadId = model.ThreadId,
                UserId = _userId,
                Content = model.Content,
                CreatedUtc = now
            };

            using (var context = ApplicationDbContext.Create())
            {
                context.Posts.Add(post);
                var thread = context.Threads.Find(model.ThreadId);
                thread.ModifiedUtc = now;
                return context.SaveChanges() == 2;
            }
        }
    }
}
