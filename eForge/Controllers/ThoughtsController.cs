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
