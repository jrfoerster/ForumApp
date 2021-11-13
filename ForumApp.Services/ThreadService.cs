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
    public class ThreadService
    {
        private Guid _userId;

        public ThreadService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateThread(ThreadCreate model)
        {
            var now = DateTimeOffset.UtcNow;
            var thread = new Thread()
            {
                ForumId = model.ForumId,
                Title = model.Title,
                UserId = _userId,
                CreatedUtc = now,
                ModifiedUtc = now
            };

            using (var context = ApplicationDbContext.Create())
            {
                context.Threads.Add(thread);
                context.SaveChanges();
                
                var post = new Post()
                {
                    ThreadId = thread.Id,
                    UserId = _userId,
                    Content = model.PostContent,
                    CreatedUtc = now,
                };

                context.Posts.Add(post);
                return context.SaveChanges() == 1;
            }
        }

        public ThreadDetail GetThreadById(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var thread = context.Threads
                    .Include(t => t.Posts)
                    .SingleOrDefault(t => t.Id == id);

                if (thread is null)
                {
                    return null;
                }

                var model = new ThreadDetail()
                {
                    ThreadId = thread.Id,
                    Title = thread.Title,
                    Posts = thread.Posts.Select(p => new PostListItem()
                    {
                        PostId = p.Id,
                        UserId = p.UserId,
                        Content = p.Content,
                        CreatedUtc = p.CreatedUtc,
                        ModifiedUtc = p.ModifiedUtc
                    }).ToList()
                };

                return model;
            }
        }

        public ThreadEdit GetThreadEditById(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var thread = context.Threads
                    .Include(t => t.Forum)
                    .SingleOrDefault(t => t.Id == id);

                if (thread is null)
                {
                    return null;
                }

                var model = new ThreadEdit()
                {
                    ForumId = thread.Forum.Id,
                    ThreadId = thread.Id,
                    Title = thread.Title
                };

                return model;
            }
        }

        public bool EditThread(ThreadEdit model)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var thread = context.Threads.Single(t => t.Id == model.ThreadId && t.UserId == _userId);
                thread.Title = model.Title;
                return context.SaveChanges() == 1;
            }
        }

        public bool DeleteThread(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var thread = context.Threads.Single(t => t.Id == id && t.UserId == _userId);
                context.Threads.Remove(thread);
                return context.SaveChanges() == 1;
            }
        }
    }
}
