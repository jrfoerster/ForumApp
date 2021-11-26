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
    public class BookmarkService
    {
        private readonly Guid _userId;

        public BookmarkService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateBookmark(int threadId)
        {
            using (var context = ApplicationDbContext.Create())
            {
                bool hasBookmark = context.Bookmarks
                    .Any(b => b.ThreadId == threadId && b.UserId == _userId);

                if (hasBookmark)
                {
                    return false;
                }

                var bookmark = new Bookmark()
                {
                    ThreadId = threadId,
                    UserId = _userId
                };

                context.Bookmarks.Add(bookmark);
                return context.SaveChanges() == 1;
            }
        }

        public bool DeleteBookmark(int threadId)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var bookmark = context.Bookmarks
                    .SingleOrDefault(b => b.ThreadId == threadId && b.UserId == _userId);

                if (bookmark is null)
                {
                    return false;
                }

                context.Bookmarks.Remove(bookmark);
                return context.SaveChanges() == 1;
            }
        }

        public List<ThreadListItem> GetBookmarkedThreads()
        {
            using (var context = ApplicationDbContext.Create())
            {
                var bookmarks = context.Bookmarks
                    .Include(b => b.Thread.Posts)
                    .Where(b => b.UserId == _userId)
                    .OrderByDescending(b => b.Thread.ModifiedUtc)
                    .ToList();

                var threads = new List<ThreadListItem>();

                foreach (var bookmark in bookmarks)
                {
                    var thread = bookmark.Thread;
                    var t = new ThreadListItem()
                    {
                        ThreadId = thread.Id,
                        Title = thread.Title,
                        PostCount = thread.Posts.Count,
                        UserName = GetThreadUserName(context, thread),
                        LastPostUtc = thread.ModifiedUtc,
                        LastPostUserName = GetLastPostUserName(context, thread),
                        IsEditable = thread.UserId == _userId,
                    };
                    threads.Add(t);
                }

                return threads;
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
