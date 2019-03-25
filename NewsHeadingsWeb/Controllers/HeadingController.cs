using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase.DataModel;
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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dp = new MainWorker();
            HeadingInfo headingInfo = dp.Heading.GetByID(id);
            HeadingModel headingModel = new HeadingModel
            {
                ID = headingInfo.ID,
                Name = headingInfo.Name,
                PathLink = headingInfo.PathLink
            };
            return View(headingModel);
        }

        [HttpPost]
        public ActionResult Edit(HeadingModel heading)
        {
            try
            {
                var db = new MainWorker();
                db.Heading.Edit(heading);
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