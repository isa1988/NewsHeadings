using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase.Working;
using NewsHeadingsWeb.Models;

namespace NewsHeadingsWeb.Controllers
{
    public class ArticleController : Controller
    {
        [HttpGet]
        public ActionResult Insert(int id)
        {
           return View(new ArticleModel{HeadingID = id});
        }

        [HttpPost]
        public ActionResult Insert(ArticleModel article)
        {
            try
            {
                var db = new MainWorker();
                db.Article.Insert(article);
                return Redirect("/News/Show");

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(article);
            }

        }
    }
}