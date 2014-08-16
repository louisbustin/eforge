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
    [Authorize]
    public class BlogController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: /Blog/
        public ActionResult Index()
        {
            ViewBag.ClaimsIdentity = System.Threading.Thread.CurrentPrincipal.Identity;
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
            ViewBag.BlogEntryCategoryId = new SelectList(db.BlogEntryCategories, "BlogEntryCategoryId", "Name");
            return View();
        }

        // POST: /Blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlogEntryId,Body,PublicationDate,Subject,Summary,UserId,BlogEntryCategoryId,LinkText")] BlogEntry blogentry)
        {
            blogentry.LastModifiedDate = DateTime.UtcNow;

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

        // GET: /Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogEntry blogentry = db.BlogEntries
                .Where(be => be.BlogEntryId == id)
                .FirstOrDefault()
                ;

            if (blogentry == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "EmailAddress", blogentry.UserId);
            ViewBag.BlogEntryCategoryId = new SelectList(db.BlogEntryCategories, "BlogEntryCategoryId", "Name", blogentry.BlogEntryCategoryId);

            return View(blogentry);
        }

        // POST: /Blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlogEntryId,Body,PublicationDate,Subject,Summary,UserId,BlogEntryCategoryId,LinkText")] BlogEntry blogentry)
        {
            if (ModelState.IsValid)
            {
                db.BlogEntries.Attach(blogentry);
                blogentry.LastModifiedDate = DateTime.UtcNow;

                db.Entry(blogentry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "EmailAddress", blogentry.UserId);
            ViewBag.BlogEntryCategoryId = new SelectList(db.BlogEntryCategories, "BlogEntryCategoryId", "Name", blogentry.BlogEntryCategoryId);

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
