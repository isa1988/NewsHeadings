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
    public class ArticleController : Controller
    {
        [HttpGet]
        public ActionResult Insert(int headingID)
        {
            return View(new ArticleModel{ID = headingID});
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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dp = new MainWorker();
            ArticleInfo articleInfo = dp.Article.GetByID(id);
            ArticleModel articleModel = new ArticleModel
            {
                ID = articleInfo.ID,
                Name = articleInfo.Name,
                Text = articleInfo.Text,
                Autor = articleInfo.Autor,
                DateCreate = articleInfo.DateCreate,
                HeadingID = articleInfo.HeadingID
            };
            List<SelectListItem> headings = dp.Heading.GetAll().Select(x =>
                new SelectListItem { Text = x.Name, Value = x.ID.ToString()}).ToList();
            ViewBag.Headings = headings;
            return View(articleModel);
        }

        [HttpPost]
        public ActionResult Edit(ArticleModel article)
        {
            try
            {
                var db = new MainWorker();
                db.Article.Edit(article);
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