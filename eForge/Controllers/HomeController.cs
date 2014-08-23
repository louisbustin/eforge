using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eForge.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "About this website.";
            

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "How to get in touch with me";

            return View();
        }
    }
}