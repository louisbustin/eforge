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
    public class BlogController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: /Blog/
        public ActionResult Index()
        {
            var blogentries = db.BlogEntries.Include(b => b.Author);
            return View(blogentries.ToList());
        }

        // GET: /Blog/Details/5
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

        // GET: /Blog/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "EmailAddress");
            ViewBag.BlogEntryCategories = new MultiSelectList(db.BlogEntryCategories, "BlogEntryCategoryId", "Name");
            return View();
        }

        // POST: /Blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlogEntryId,Body,PublicationDate,Subject,Summary,UserId")] BlogEntry blogentry)
        {
            blogentry.LastModifiedDate = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                var becIds = Request["BlogEntryCategories"].Split(',').Select(id => int.Parse(id)).ToList();
                blogentry.BlogEntryCategories = db.BlogEntryCategories.Where(bec => becIds.Contains(bec.BlogEntryCategoryId)).ToList();

                db.BlogEntries.Add(blogentry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "EmailAddress", blogentry.UserId);
            ViewBag.BlogEntryCategories = new MultiSelectList(db.BlogEntryCategories, "BlogEntryCategoryId", "Name");

            return View(blogentry);
        }

        // GET: /Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogEntry blogentry = db.BlogEntries
                .Include(be => be.BlogEntryCategories)
                .Where(be => be.BlogEntryId == id)
                .FirstOrDefault()
                ;

            if (blogentry == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "EmailAddress", blogentry.UserId);
            ViewBag.BlogEntryCategories = new MultiSelectList(db.BlogEntryCategories, "BlogEntryCategoryId", "Name");

            return View(blogentry);
        }

        // POST: /Blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlogEntryId,Body,PublicationDate,Subject,Summary,UserId")] BlogEntry blogentry)
        {
            if (ModelState.IsValid)
            {
                blogentry.LastModifiedDate = DateTime.UtcNow;

                var becIds = Request["BlogEntryCategories"].Split(',').Select(id => int.Parse(id)).ToList();
                blogentry.BlogEntryCategories = db.BlogEntryCategories.Where(bec => becIds.Contains(bec.BlogEntryCategoryId)).ToList();


                db.Entry(blogentry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "EmailAddress", blogentry.UserId);
            ViewBag.BlogEntryCategories = new MultiSelectList(db.BlogEntryCategories, "BlogEntryCategoryId", "Name");

            return View(blogentry);
        }

        // GET: /Blog/Delete/5
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

        // POST: /Blog/Delete/5
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
