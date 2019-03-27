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
           return View();
        }

        [HttpPost]
        public ActionResult Insert(ArticleModel article)
        {
            try
            {
                string[] id = Request.Path.Split('/');
                int idbridge = Convert.ToInt32(id[id.Length - 1]);
                article.HeadingID = idbridge;
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