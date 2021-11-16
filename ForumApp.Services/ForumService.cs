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
    public class ForumService
    {
        private Guid _userId;

        public ForumService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateForum(ForumCreate model)
        {
            var forum = new Forum()
            {
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
                    Description = forum.Description
                });

                return query.ToList();
            }
        }

        public ForumDetail GetForumById(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var forum = context.Forums
                    .Include(f => f.Threads)
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
                            UserId = thread.Posts.First().UserId,
                            LastPostUtc = thread.ModifiedUtc,
                            IsEditable = thread.UserId == _userId
                        }).ToList()
                };

                return model;
            }
        }
    }
}
