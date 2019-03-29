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
            return View(new ArticleModel {HeadingID = headingID,
                                          Title = "Добавление статьи" });
        }

        [HttpPost]
        public ActionResult Insert(ArticleModel article)
        {
            try
            {
                var worker = new MainWorker();
                worker.Article.Insert(new ArticleInfo
                {
                    ID = article.ID,
                    Name = article.Name,
                    Autor = article.Autor,
                    Text = article.Text,
                    HeadingID = article.HeadingID
                });
                return Redirect("/News/Show");

            }
            catch (Exception ex)
            {
                article.Title = "Добавление статьи";
                article.ErrorMessage = ex.Message;
                return View(article);
            }

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var worker = new MainWorker();
            ArticleInfo articleInfo = worker.Article.GetByID(id);
            ArticleModel articleModel = new ArticleModel
            {
                ID = articleInfo.ID,
                Name = articleInfo.Name,
                Text = articleInfo.Text,
                Autor = articleInfo.Autor,
                DateCreate = articleInfo.DateCreate,
                HeadingID = articleInfo.HeadingID
            };
            articleModel.Title = "Редактирование статьи";
            articleModel.Headings = worker.Heading.GetAll().Select(x =>
                new SelectListItem { Text = x.Name, Value = x.ID.ToString()}).ToList();
            return View(articleModel);
        }

        [HttpPost]
        public ActionResult Edit(ArticleModel article)
        {
            try
            {
                var worker = new MainWorker();
                worker.Article.Edit(new ArticleInfo
                {
                    ID = article.ID,
                    Name = article.Name,
                    Autor = article.Autor,
                    Text = article.Text,
                    HeadingID = article.HeadingID
                });
                return Redirect("/News/Show");

            }
            catch (Exception ex)
            {
                article.Title = "Редактирование статьи";
                var db = new MainWorker();
                article.Headings = db.Heading.GetAll().Select(x =>
                    new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
                article.ErrorMessage = ex.Message;
                return View(article);
            }
        }
    }
}