using ForumApp.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ForumApp.WebMvc.Controllers
{
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
        [ActionName("CreateFromThread")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBookmark(int threadId)
        {
            var service = CreateBookmarkService();
            service.CreateBookmark(threadId);
            return RedirectToAction("Details", "Thread", new { id = threadId });
        }

        [HttpPost]
        [ActionName("DeleteFromThread")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBookmarkFromThread(int threadId)
        {
            var service = CreateBookmarkService();
            service.DeleteBookmark(threadId);
            return RedirectToAction("Details", "Thread", new { id = threadId });
        }

        [HttpPost]
        [ActionName("DeleteFromIndex")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBookmarkFromIndex(int threadId)
        {
            var service = CreateBookmarkService();
            service.DeleteBookmark(threadId);
            return RedirectToAction("Index");
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