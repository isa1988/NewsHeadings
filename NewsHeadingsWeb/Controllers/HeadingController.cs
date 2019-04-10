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
            return View(new HeadingModel{Title = "Добавление рублики" });
        }

        [HttpPost]
        public ActionResult Insert(HeadingModel heading)
        {
            try
            {
                var db = new MainWorker();
                db.Heading.Insert(new HeadingInfo
                {
                     ID = heading.ID,
                     Name = heading.Name
                });
                return Redirect("/News/Show");

            }
            catch (Exception ex)
            {
                heading.Title = "Добавление рублики";
                heading.ErrorMessage = ex.Message;
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
                PathLink = headingInfo.PathLink,
                Title = "Редактирование рублики"
            };
            return View(headingModel);
        }

        [HttpPost]
        public ActionResult Edit(HeadingModel heading)
        {
            try
            {
                var db = new MainWorker();
                db.Heading.Edit(new HeadingInfo
                    {
                        ID = heading.ID,
                        Name = heading.Name
                    });
                return Redirect("/News/Show");

            }
            catch (Exception ex)
            {
                heading.Title = "Редактирование рублики";
                heading.ErrorMessage = ex.Message;
                return View(heading);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var dp = new MainWorker();
            HeadingInfo headingInfo = dp.Heading.GetByID(id);
            HeadingModel headingModel = new HeadingModel
            {
                ID = headingInfo.ID,
                Name = headingInfo.Name,
                PathLink = headingInfo.PathLink,
                Title = "Удаление рублики"
            };
            return View(headingModel);
        }

        [HttpPost]
        public ActionResult Delete(HeadingModel heading)
        {
            try
            {
                var db = new MainWorker();
                db.Heading.Delete(heading.ID);
                return Redirect("/News/Show");

            }
            catch (Exception ex)
            {
                heading.Title = "Удаление рублики";
                heading.ErrorMessage = ex.Message;
                return View(heading);
            }
        }
    }
}