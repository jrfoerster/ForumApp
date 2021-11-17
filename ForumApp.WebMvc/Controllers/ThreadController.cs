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
    public class ThreadController : Controller
    {
        public ActionResult Create(int? id)
        {
            if (id is null)
            {
                return HttpBadRequest();
            }

            var model = new ThreadCreate()
            {
                ForumId = id.Value
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ThreadCreate model)
        {
            if (ModelState.IsValid)
            {
                var service = CreateThreadService();
                if (service.CreateThread(model))
                {
                    TempData["SaveResult"] = "New thread was created.";
                    return RedirectToAction("Details", "Forum", new { id = model.ForumId });
                }
            }

            return View(model);
        }

        // GET: Thread
        public ActionResult Details(int? id)
        {
            if (id is null)
            {
                return HttpBadRequest();
            }

            var service = CreateThreadService();
            var detail = service.GetThreadById(id.Value);

            if (detail is null)
            {
                return HttpNotFound();
            }

            return View(detail);
        }

        public ActionResult Edit(int? id)
        {
            if (id is null)
            {
                return HttpBadRequest();
            }

            var service = CreateThreadService();
            var model = service.GetThreadEditById(id.Value);

            if (model is null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ThreadEdit model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            if (model.ThreadId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
            }

            var service = CreateThreadService();

            if (service.EditThread(model))
            {
                TempData["SaveResult"] = "Your thread title was updated";
                return RedirectToAction("Details", "Forum", new { id = model.ForumId });
            }

            ModelState.AddModelError("", "Your thread title could not be updated.");
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id is null)
            {
                return HttpBadRequest();
            }

            var service = CreateThreadService();
            var model = service.GetThreadEditById(id.Value);

            if (model is null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ThreadEdit model)
        {
            if (id != model.ThreadId)
            {
                ModelState.AddModelError("", "Id Mismatch");
            }

            var service = CreateThreadService();
            if (service.DeleteThread(model.ThreadId))
            {
                TempData["SaveResult"] = "Your thread was deleted";
                return RedirectToAction("Details", "Forum", new { id = model.ForumId });
            }

            ModelState.AddModelError("", "Your thread could not be deleted");
            return View(model);
        }

        private ThreadService CreateThreadService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new ThreadService(userId);
        }

        private HttpStatusCodeResult HttpBadRequest()
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}