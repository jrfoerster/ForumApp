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

        // GET: Forum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id is null)
            {
                return HttpBadRequest();
            }

            var service = CreatePostService();
            var forum = service.GetPostEditById(id.Value);

            if (forum is null)
            {
                return HttpNotFound();
            }

            return View(forum);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PostEdit model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            if (id != model.PostId)
            {
                ModelState.AddModelError("", "Id Mismatch");
            }

            var service = CreatePostService();

            if (service.EditPost(model))
            {
                TempData["SaveResult"] = "New forum was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your forum could not be updated.");
            return View(model);
        }

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