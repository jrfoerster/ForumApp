using ForumApp.Data;
using ForumApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public PostEdit GetPostEditById(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var post = context.Posts
                    .Include(p => p.Thread)
                    .SingleOrDefault(t => t.Id == id && t.UserId == _userId);

                if (post is null)
                {
                    return null;
                }

                var model = new PostEdit()
                {
                    PostId = post.Id,
                    ThreadId = post.Thread.Id,
                    Content = post.Content
                };

                return model;
            }
        }

        public bool EditPost(PostEdit model)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var post = context.Posts
                    .SingleOrDefault(p => p.Id == model.PostId && p.UserId == _userId);

                if (post is null)
                {
                    return false;
                }

                post.Content = model.Content;
                post.ModifiedUtc = DateTimeOffset.UtcNow;
                return context.SaveChanges() == 1;
            }
        }

        public bool DeletePost(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var post = context.Posts
                    .SingleOrDefault(p => p.Id == id && p.UserId == _userId);

                if (post is null)
                {
                    return false;
                }

                context.Posts.Remove(post);
                return context.SaveChanges() == 1;
            }
        }
    }
}
