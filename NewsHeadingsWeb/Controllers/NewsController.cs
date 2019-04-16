using System;
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
    /// Главная страница контроллер
    /// </summary>
    public class NewsController : Controller
    {
        private IDataProvider dataProvider;

        /// <summary>
        /// Главная страница контроллер
        /// </summary>
        /// <param name="dataProvider">Работа с данными</param>
        public NewsController(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }
        /// <summary>
        /// Покатать даннык
        /// </summary>
        /// <param name="pathLink">Путь из модели рубрик</param>
        /// <returns></returns>
        // GET: Home
        public ActionResult Show(string pathLink)
        {
            HeadingModel headingModel = new HeadingModel();
            headingModel.Title = "Новости 24";
            headingModel.Headings = dataProvider.Heading.GetAll().Select(x => new Models.HeadingModel()
            {
                ID = x.ID,
                Name = x.Name,
                PathLink = x.PathLink
            }).ToList();
            HeadingInfo headingInfo = dataProvider.Heading.GetByPathLink(pathLink);
            if (headingInfo != null) // если не нашел рубрику то показать все статьи
            {
                headingModel.ID = headingInfo.ID;
                headingModel.Name = headingInfo.Name;
                headingModel.PathLink = headingInfo.PathLink;
                List<ArticleModel> articlesResults = dataProvider.Heading.ArticleByHeading(headingModel.ID)
                    .Select(x => new Models.ArticleModel()
                    {
                        ID = x.ID,
                        Name = x.Name,
                        Text = x.Text,
                        Author = x.Author,
                        DateCreate = x.DateCreate, 
                        FileName = x.FileName
                    }).ToList();
                headingModel.Articles = articlesResults;
            }
            else // если нашел рубрику то показать все статьи данной рубрикм
            {
                List<ArticleModel> articlesResults = dataProvider.Article.GetAll()
                    .Select(x => new Models.ArticleModel()
                    {
                        ID = x.ID,
                        Name = x.Name,
                        Text = x.Text,
                        Author = x.Author,
                        DateCreate = x.DateCreate,
                        FileName = x.FileName
                    }).ToList();
                headingModel.Articles = articlesResults;
            }
            return View(headingModel);
        }

    }
}