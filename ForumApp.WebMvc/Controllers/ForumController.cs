using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ForumApp.Data;
using ForumApp.Models;
using ForumApp.Services;
using Microsoft.AspNet.Identity;

namespace ForumApp.WebMvc.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        // GET: Forum
        public ActionResult Index()
        {
            var service = CreateForumService();
            var forums = service.GetForums();
            return View(forums);
        }

        // GET: Forum/Details/5
        public ActionResult Details(int? id)
        {
            if (id is null)
            {
                return HttpBadRequest();
            }

            var service = CreateForumService();
            var forum = service.GetForumById(id.Value);

            if (forum is null)
            {
                return HttpNotFound();
            }

            return View(forum);
        }

        // GET: Forum/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ForumCreate model)
        {
            if (ModelState.IsValid)
            {
                var service = CreateForumService();
                if (service.CreateForum(model))
                {
                    TempData["SaveResult"] = "New forum was created.";
                    return RedirectToAction("Index");
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

            var service = CreateForumService();
            var forum = service.GetForumEditById(id.Value);

            if (forum is null)
            {
                return HttpNotFound();
            }

            return View(forum);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ForumEdit model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            if (id != model.ForumId)
            {
                ModelState.AddModelError("", "Id Mismatch");
            }

            var service = CreateForumService();

            if (service.EditForum(model))
            {
                TempData["SaveResult"] = "Forum was edited.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Forum could not be updated.");
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id is null)
            {
                return HttpBadRequest();
            }

            var service = CreateForumService();
            var model = service.GetForumEditById(id.Value);

            if (model is null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ForumEdit model)
        {
            if (id != model.ForumId)
            {
                ModelState.AddModelError("", "Id Mismatch");
            }

            var service = CreateForumService();
            if (service.DeleteForum(model.ForumId))
            {
                TempData["SaveResult"] = "Forum was deleted";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Forum could not be deleted");
            return View(model);
        }

        private ForumService CreateForumService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new ForumService(userId);
        }

        private HttpStatusCodeResult HttpBadRequest()
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}
