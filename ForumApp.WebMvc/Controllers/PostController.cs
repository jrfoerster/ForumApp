using ForumApp.Models;
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
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Create(int id)
        {
            var model = new PostCreate()
            {
                ThreadId = id,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostCreate model)
        {
            if (ModelState.IsValid)
            {
                var service = CreatePostService();
                if (service.CreatePost(model))
                {
                    TempData["SaveResult"] = "New thread was created.";
                    return RedirectToAction("Details", "Thread", new { id = model.ThreadId });
                }
            }

            return View(model);
        }

        //TODO: Edit post

        //TODO: Delete post

        private PostService CreatePostService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new PostService(userId);
        }

        private HttpStatusCodeResult HttpBadRequest()
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}