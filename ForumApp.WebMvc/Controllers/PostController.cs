using ForumApp.Models;
using ForumApp.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Net;
using System.Web.Mvc;

namespace ForumApp.WebMvc.Controllers
{
    [Authorize]
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
                TempData["SaveResult"] = "Post was edited.";
                return RedirectToAction("Details", "Thread", new { id = model.ThreadId });
            }

            ModelState.AddModelError("", "Post could not be updated.");
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id is null)
            {
                return HttpBadRequest();
            }

            var service = CreatePostService();
            var model = service.GetPostEditById(id.Value);

            if (model is null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, PostEdit model)
        {
            if (id != model.PostId)
            {
                ModelState.AddModelError("", "Id Mismatch");
            }

            var service = CreatePostService();
            if (service.DeletePost(model.PostId))
            {
                TempData["SaveResult"] = "Post was deleted";
                return RedirectToAction("Details", "Thread", new { id = model.ThreadId });
            }

            ModelState.AddModelError("", "Post could not be deleted");
            return View(model);
        }

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