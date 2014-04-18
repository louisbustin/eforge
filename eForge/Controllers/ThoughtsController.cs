using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eForgeModels;

namespace eForge.Controllers
{
    public class ThoughtsController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: /Thoughts/
        public ActionResult Index()
        {
            var blogentries = db.BlogEntries.Include(b => b.Author).Include(b => b.Category);
            return View(blogentries.ToList());
        }

        // GET: /Thoughts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogEntry blogentry = db.BlogEntries.Find(id);
            if (blogentry == null)
            {
                return HttpNotFound();
            }
            return View(blogentry);
        }

        // GET: /Thoughts/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "EmailAddress");
            ViewBag.BlogEntryCategoryId = new SelectList(db.BlogEntryCategories, "BlogEntryCategoryId", "Name");
            return View();
        }

        // POST: /Thoughts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="BlogEntryId,BlogEntryCategoryId,Body,LastModifiedDate,PublicationDate,Subject,Summary,UserId")] BlogEntry blogentry)
        {
            if (ModelState.IsValid)
            {
                db.BlogEntries.Add(blogentry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "EmailAddress", blogentry.UserId);
            ViewBag.BlogEntryCategoryId = new SelectList(db.BlogEntryCategories, "BlogEntryCategoryId", "Name", blogentry.BlogEntryCategoryId);
            return View(blogentry);
        }

        // GET: /Thoughts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogEntry blogentry = db.BlogEntries.Find(id);
            if (blogentry == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "EmailAddress", blogentry.UserId);
            ViewBag.BlogEntryCategoryId = new SelectList(db.BlogEntryCategories, "BlogEntryCategoryId", "Name", blogentry.BlogEntryCategoryId);
            return View(blogentry);
        }

        // POST: /Thoughts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="BlogEntryId,BlogEntryCategoryId,Body,LastModifiedDate,PublicationDate,Subject,Summary,UserId")] BlogEntry blogentry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogentry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "EmailAddress", blogentry.UserId);
            ViewBag.BlogEntryCategoryId = new SelectList(db.BlogEntryCategories, "BlogEntryCategoryId", "Name", blogentry.BlogEntryCategoryId);
            return View(blogentry);
        }

        // GET: /Thoughts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogEntry blogentry = db.BlogEntries.Find(id);
            if (blogentry == null)
            {
                return HttpNotFound();
            }
            return View(blogentry);
        }

        // POST: /Thoughts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogEntry blogentry = db.BlogEntries.Find(id);
            db.BlogEntries.Remove(blogentry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
