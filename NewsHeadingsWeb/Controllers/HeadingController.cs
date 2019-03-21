using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase.Working;
using NewsHeadingsWeb.Models;

namespace NewsHeadingsWeb.Controllers
{
    public class HeadingController : Controller
    {
        [HttpGet]
        public ActionResult Insert()
        {
            //ViewBag.BookId = id;
            return View();
        }
        [HttpPost]
        public ActionResult Insert(HeadingModel heading)
        {
            try
            {
                var db = new MainWorker();
                db.Heading.Insert(heading);
                return Redirect("/News/Show");

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(heading);
            }

        }
    }
}