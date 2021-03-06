﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase.Contract;
using DataBase.DataModel;
using DataBase.Working;
using NewsHeadingsWeb.Models;

namespace NewsHeadingsWeb.Controllers
{
    /// <summary>
    /// Статьи контроллер
    /// </summary>
    public class ArticleController : BaseController
    {
        
        /// <summary>
        /// Статьи контроллер
        /// </summary>
        /// <param name="dataProvider">Работа с данными</param>
        public ArticleController(IDataProvider dataProvider) : base(dataProvider)
        {
            this.dataProvider = dataProvider;
        }
        /// <summary>
        /// Добавить статью
        /// </summary>
        /// <param name="headingID">ID рубрики</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Insert(int headingID)
        {
            return View(new ArticleModel {HeadingID = headingID,
                                          Title = "Добавление статьи" });
        }
        
        /// <summary>
        /// Post метод для добавления статьи 
        /// </summary>
        /// <param name="article">Модель статьи</param>
        /// <param name="file">Работа с файлом</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Insert(ArticleModel article, HttpPostedFileBase file)
        {
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
            dataProvider.Article.Insert(articleInfo);
            return Redirect("/News/Show");

        }

        /// <summary>
        /// Редактирование статьи
        /// </summary>
        /// <param name="id">ID статьи</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ArticleInfo articleInfo = dataProvider.Article.GetByID(id);
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
            articleModel.Headings = dataProvider.Heading.GetAll().Select(x =>
                new SelectListItem { Text = x.Name, Value = x.ID.ToString()}).ToList();
            return View(articleModel);
        }

        /// <summary>
        /// Post метод для редактирование статьи
        /// </summary>
        /// <param name="article">Модель статьи</param>
        /// <param name="file">Работа с файлами</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(ArticleModel article, HttpPostedFileBase file)
        {
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
            dataProvider.Article.Edit(articleInfo);
            return Redirect("/News/Show");
        }

        /// <summary>
        /// Удаление статьи
        /// </summary>
        /// <param name="id">ID статьи</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            ArticleInfo articleInfo = dataProvider.Article.GetByID(id);
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
            articleModel.Headings = dataProvider.Heading.GetAll().Select(x =>
                new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            return View(articleModel);
        }

        /// <summary>
        /// Post метод для удаления статьи
        /// </summary>
        /// <param name="article">Модель статьи</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(ArticleModel article)
        {
            if (ModelState.IsValid)
            {
                dataProvider.Article.Delete(article.ID);
                return PartialView("DeleteImfo");
            }
            return PartialView(article);
        }
    }
}