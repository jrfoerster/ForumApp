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
            if (id == null)
            {
                return HttpBadRequest();
            }

            var service = CreateForumService();
            var forum = service.GetForumById(id.Value);

            if (forum == null)
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

        //// GET: Forum/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpBadRequest();
        //    }
        //    Forum forum = _context.Forums.Find(id);
        //    if (forum == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(forum);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Description")] Forum forum)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Entry(forum).State = EntityState.Modified;
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(forum);
        //}

        //// GET: Forum/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpBadRequest();
        //    }
        //    Forum forum = _context.Forums.Find(id);
        //    if (forum == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(forum);
        //}

        //// POST: Forum/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Forum forum = _context.Forums.Find(id);
        //    _context.Forums.Remove(forum);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
