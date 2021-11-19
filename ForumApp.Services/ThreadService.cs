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
                    .Include(t => t.Forum)
                    .Include(t => t.Posts)
                    .SingleOrDefault(t => t.Id == id);

                if (thread is null)
                {
                    return null;
                }

                var model = new ThreadDetail()
                {
                    ThreadId = thread.Id,
                    ForumId = thread.ForumId,
                    ForumName = thread.Forum.Name,
                    Title = thread.Title,
                    Posts = thread.Posts
                        .Select(post => new PostListItem()
                        {
                            PostId = post.Id,
                            UserName = GetUserName(context, post),
                            Content = post.Content,
                            CreatedUtc = post.CreatedUtc,
                            ModifiedUtc = post.ModifiedUtc,
                            IsEditable = post.UserId == _userId
                        }).ToList()
                };

                return model;
            }
        }

        private string GetUserName(ApplicationDbContext context, Post post)
        {
            string userId = post.UserId.ToString();
            var user = context.Users.Find(userId);
            return user.UserName;
        }

        public ThreadEdit GetThreadEditById(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var thread = context.Threads
                    .Include(t => t.Forum)
                    .SingleOrDefault(t => t.Id == id && t.UserId == _userId);

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
                var thread = context.Threads
                    .SingleOrDefault(t => t.Id == model.ThreadId && t.UserId == _userId);

                if (thread is null)
                {
                    return false;
                }

                thread.Title = model.Title;
                return context.SaveChanges() == 1;
            }
        }

        public bool DeleteThread(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var thread = context.Threads
                    .SingleOrDefault(t => t.Id == id && t.UserId == _userId);

                if (thread is null)
                {
                    return false;
                }

                context.Threads.Remove(thread);
                return context.SaveChanges() == 1;
            }
        }
    }
}
