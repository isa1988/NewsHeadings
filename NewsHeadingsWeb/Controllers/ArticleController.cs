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
        public ActionResult Insert(ArticleModel article, HttpPostedFileBase file)
        {
            var worker = new MainWorker();
            ArticleInfo articleInfo = new ArticleInfo
            {
                ID = article.ID,
                Name = article.Name,
                Author = article.Author,
                Text = article.Text,
                HeadingID = article.HeadingID,
                IsDelete = article.IsDelete
            };
            if (file != null)
            {
                articleInfo.FileName = System.IO.Path.GetFileName(file.FileName);
                articleInfo.File = new byte[file.ContentLength];
                file.InputStream.Read(articleInfo.File, 0, articleInfo.File.Length);
            }
            worker.Article.Insert(articleInfo);
            return Redirect("/News/Show");

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
                Author = articleInfo.Author,
                DateCreate = articleInfo.DateCreate,
                HeadingID = articleInfo.HeadingID,
                IsDelete = articleInfo.IsDelete,
                FileName = articleInfo.FileName
            };
            articleModel.Title = "Редактирование статьи";
            articleModel.Headings = worker.Heading.GetAll().Select(x =>
                new SelectListItem { Text = x.Name, Value = x.ID.ToString()}).ToList();
            return View(articleModel);
        }

        [HttpPost]
        public ActionResult Edit(ArticleModel article, HttpPostedFileBase file)
        {
            var worker = new MainWorker();
            ArticleInfo articleInfo = new ArticleInfo
            {
                ID = article.ID,
                Name = article.Name,
                Author = article.Author,
                Text = article.Text,
                HeadingID = article.HeadingID,
                IsDelete = article.IsDelete
            };
            if (file != null)
            {
                articleInfo.FileName = System.IO.Path.GetFileName(file.FileName);
                articleInfo.File = new byte[file.ContentLength];
                file.InputStream.Read(articleInfo.File, 0, articleInfo.File.Length);
            }
            worker.Article.Edit(articleInfo);
            return Redirect("/News/Show");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var worker = new MainWorker();
            ArticleInfo articleInfo = worker.Article.GetByID(id);
            ArticleModel articleModel = new ArticleModel
            {
                ID = articleInfo.ID,
                Name = articleInfo.Name,
                Text = articleInfo.Text,
                Author = articleInfo.Author,
                DateCreate = articleInfo.DateCreate,
                HeadingID = articleInfo.HeadingID,
                IsDelete = articleInfo.IsDelete
            };
            articleModel.Title = "Удаление статьи";
            articleModel.Headings = worker.Heading.GetAll().Select(x =>
                new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            return View(articleModel);
        }

        [HttpPost]
        public ActionResult Delete(ArticleModel article)
        {
            var worker = new MainWorker();
            ArticleInfo articleInfo = new ArticleInfo
            {
                ID = article.ID,
                Name = article.Name,
                Author = article.Author,
                Text = article.Text,
                HeadingID = article.HeadingID,
                IsDelete = article.IsDelete
            };
            worker.Article.Delete(articleInfo.ID);
            return Redirect("/News/Show");
        }
    }
}