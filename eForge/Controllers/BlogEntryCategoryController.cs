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
    public class BlogEntryCategoryController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: /BlogEntryCategory/
        public ActionResult Index()
        {
            return View(db.BlogEntryCategories.ToList());
        }

        // GET: /BlogEntryCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogEntryCategory blogentrycategory = db.BlogEntryCategories.Find(id);
            if (blogentrycategory == null)
            {
                return HttpNotFound();
            }
            return View(blogentrycategory);
        }

        // GET: /BlogEntryCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /BlogEntryCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="BlogEntryCategoryId,Name,Description")] BlogEntryCategory blogentrycategory)
        {
            if (ModelState.IsValid)
            {
                db.BlogEntryCategories.Add(blogentrycategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogentrycategory);
        }

        // GET: /BlogEntryCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogEntryCategory blogentrycategory = db.BlogEntryCategories.Find(id);
            if (blogentrycategory == null)
            {
                return HttpNotFound();
            }
            return View(blogentrycategory);
        }

        // POST: /BlogEntryCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="BlogEntryCategoryId,Name,Description")] BlogEntryCategory blogentrycategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogentrycategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogentrycategory);
        }

        // GET: /BlogEntryCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogEntryCategory blogentrycategory = db.BlogEntryCategories.Find(id);
            if (blogentrycategory == null)
            {
                return HttpNotFound();
            }
            return View(blogentrycategory);
        }

        // POST: /BlogEntryCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogEntryCategory blogentrycategory = db.BlogEntryCategories.Find(id);
            db.BlogEntryCategories.Remove(blogentrycategory);
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
