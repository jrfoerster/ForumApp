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

namespace ForumApp.WebMvc.Controllers
{
    public class ForumController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Forum
        public ActionResult Index()
        {
            var forums = _context.Forums.Select(forum => new ForumListItem()
            {
                ForumId = forum.Id,
                Name = forum.Name,
                Description = forum.Description
            });
            return View(forums);
        }

        // GET: Forum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpBadRequest();
            }
            Forum forum = _context.Forums.Find(id);
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
                var forum = new Forum()
                {
                    Name = model.Name,
                    Description = model.Description
                };
                _context.Forums.Add(forum);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Forum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpBadRequest();
            }
            Forum forum = _context.Forums.Find(id);
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        // POST: Forum/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Forum forum)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(forum).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(forum);
        }

        // GET: Forum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpBadRequest();
            }
            Forum forum = _context.Forums.Find(id);
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        // POST: Forum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Forum forum = _context.Forums.Find(id);
            _context.Forums.Remove(forum);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private HttpStatusCodeResult HttpBadRequest()
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}
