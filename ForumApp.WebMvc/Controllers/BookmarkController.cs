using ForumApp.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Net;
using System.Web.Mvc;

namespace ForumApp.WebMvc.Controllers
{
    [Authorize]
    public class BookmarkController : Controller
    {
        // GET: Bookmark
        public ActionResult Index()
        {
            var service = CreateBookmarkService();
            var threads = service.GetBookmarkedThreads();
            return View(threads);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromThread(int threadId)
        {
            var service = CreateBookmarkService();
            service.CreateBookmark(threadId);
            return RedirectToAction("Details", "Thread", new { id = threadId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromForumDetail(int threadId, int forumId)
        {
            var service = CreateBookmarkService();
            service.CreateBookmark(threadId);
            return RedirectToAction("Details", "Forum", new { id = forumId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFromThread(int threadId)
        {
            var service = CreateBookmarkService();
            service.DeleteBookmark(threadId);
            return RedirectToAction("Details", "Thread", new { id = threadId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFromIndex(int threadId)
        {
            var service = CreateBookmarkService();
            service.DeleteBookmark(threadId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFromForumDetail(int threadId, int forumId)
        {
            var service = CreateBookmarkService();
            service.DeleteBookmark(threadId);
            return RedirectToAction("Details", "Forum", new { id = forumId });
        }

        private BookmarkService CreateBookmarkService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new BookmarkService(userId);
        }

        private HttpStatusCodeResult HttpBadRequest()
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}