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
    public class NewsController : Controller
    {
        // GET: Home
        public ActionResult Show(string pathLink)
        {
            var dp = new MainWorker();
            HeadingModel headingModel = new HeadingModel();
            headingModel.Title = "Новости 24";
            headingModel.Headings = dp.Heading.GetAll().Select(x => new Models.HeadingModel()
            {
                ID = x.ID,
                Name = x.Name,
                PathLink = x.PathLink
            }).ToList();
            HeadingInfo headingInfo = dp.Heading.GetByPathLink(pathLink);
            if (headingInfo != null)
            {
                headingModel.ID = headingInfo.ID;
                headingModel.Name = headingInfo.Name;
                headingModel.PathLink = headingInfo.PathLink;
                List<ArticleModel> articlesResults = dp.Heading.ArticleByHeading(headingModel.ID)
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
            else
            {
                List<ArticleModel> articlesResults = dp.Article.GetAll()
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