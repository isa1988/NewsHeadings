using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase.Working;
using NewsHeadingsWeb.Models;

namespace NewsHeadingsWeb.Controllers
{
    public class NewsController : Controller
    {
        // GET: Home
        
        public ActionResult Show(HeadingModel headingModel)
        {

            var dp = new MainWorker();
            ViewBag.Heading = dp.Heading.GetAll().Select(x => new Models.HeadingModel()
            {
                ID = x.ID,
                Name = x.Name,
                PathLink = x.PathLink
            }).ToList();

            // TODO: добавить выборку новостей в зависимости от id 
            if (headingModel == null)
            {
            } else
            {
                List<ArticleModel> articlesResults = dp.Heading.ArticleByHeading(headingModel.ID)
                                                  .Select(x => new Models.ArticleModel()
                {
                    ID = x.ID,
                    Name = x.Name,
                    Text = x.Text,
                    Autor = x.Autor,
                    DateCreate = x.DateCreate
                }).ToList();
                ViewBag.HeadingID = headingModel.ID;
                ViewBag.Articles = articlesResults;
            }
            return View();
        }
        /*public ActionResult Show(string id)
        {
            var dp = new MainWorker();
            ViewBag.Heading = dp.Heading.GetAll().Select(x => new Models.HeadingModel()
            {
                ID = x.ID,
                Name = x.Name,
                PathLink = x.PathLink
            }).ToList();

            HeadingModel headingModel = ((List<HeadingModel>)ViewBag.Heading.FirstOrDefault(n => n.PathLink == id));
            // TODO: добавить выборку новостей в зависимости от id 
            if (headingModel == null)
            {
            }
            else
            {
                List<ArticleModel> articlesResults = dp.Heading.ArticleByHeading(headingModel.ID)
                    .Select(x => new Models.ArticleModel()
                    {
                        ID = x.ID,
                        Name = x.Name,
                        Text = x.Text,
                        Autor = x.Autor,
                        DateCreate = x.DateCreate
                    }).ToList();
                ViewBag.HeadingID = headingModel.ID;
                ViewBag.Articles = articlesResults;
            }
            return View();
        }*/

    }
}