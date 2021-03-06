using ForumApp.Data;
using ForumApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ForumApp.Services
{
    public class ForumService
    {
        private readonly Guid _userId;

        public ForumService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateForum(ForumCreate model)
        {
            var forum = new Forum()
            {
                UserId = _userId,
                Name = model.Name,
                Description = model.Description
            };

            using (var context = ApplicationDbContext.Create())
            {
                context.Forums.Add(forum);
                return context.SaveChanges() == 1;
            }
        }

        public List<ForumListItem> GetForums()
        {
            using (var context = ApplicationDbContext.Create())
            {
                var query = context.Forums.Select(forum => new ForumListItem()
                {
                    ForumId = forum.Id,
                    Name = forum.Name,
                    Description = forum.Description,
                    IsEditable = forum.UserId == _userId
                });

                return query.ToList();
            }
        }

        public ForumDetail GetForumById(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var forum = context.Forums
                    .Include(f => f.Threads.Select(t => t.Posts))
                    .Include(f => f.Threads.Select(t => t.Bookmarks))
                    .SingleOrDefault(f => f.Id == id);

                if (forum is null)
                {
                    return null;
                }

                var model = new ForumDetail()
                {
                    ForumId = forum.Id,
                    Name = forum.Name,
                    Threads = forum.Threads
                        .OrderByDescending(thread => thread.ModifiedUtc)
                        .Select(thread => new ThreadListItem()
                        {
                            ThreadId = thread.Id,
                            Title = thread.Title,
                            PostCount = thread.Posts.Count,
                            UserName = GetThreadUserName(context, thread),
                            LastPostUtc = thread.ModifiedUtc,
                            LastPostUserName = GetLastPostUserName(context, thread),
                            IsEditable = thread.UserId == _userId,
                            IsBookmarked = thread.Bookmarks.Any(b => b.UserId == _userId)
                        }).ToList()
                };

                return model;
            }
        }
        public ForumEdit GetForumEditById(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var forum = context.Forums
                    .SingleOrDefault(f => f.Id == id && f.UserId == _userId);

                if (forum is null)
                {
                    return null;
                }

                var model = new ForumEdit()
                {
                    ForumId = forum.Id,
                    Name = forum.Name,
                    Description = forum.Description
                };

                return model;
            }
        }

        public bool EditForum(ForumEdit model)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var forum = context.Forums
                    .SingleOrDefault(f => f.Id == model.ForumId && f.UserId == _userId);

                if (forum is null)
                {
                    return false;
                }

                forum.Name = model.Name;
                forum.Description = model.Description;
                return context.SaveChanges() == 1;
            }
        }

        public bool DeleteForum(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var forum = context.Forums
                    .SingleOrDefault(f => f.Id == id && f.UserId == _userId);

                if (forum is null)
                {
                    return false;
                }

                context.Forums.Remove(forum);
                return context.SaveChanges() == 1;
            }
        }

        private string GetThreadUserName(ApplicationDbContext context, Thread thread)
        {
            string userId = thread.UserId.ToString();
            return context.Users.Find(userId).UserName;
        }

        private string GetLastPostUserName(ApplicationDbContext context, Thread thread)
        {
            string userId = thread.Posts.Last().UserId.ToString();
            return context.Users.Find(userId).UserName;
        }
    }
}
