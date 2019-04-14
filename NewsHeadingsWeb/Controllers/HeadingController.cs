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
        public ActionResult Insert()
        {
            return PartialView("Insert", new HeadingModel{Title = "Добавление рублики" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insert(HeadingModel heading)
        {
            if (ModelState.IsValid)
            {
                var db = new MainWorker();
                db.Heading.Insert(new HeadingInfo
                {
                    ID = heading.ID,
                    Name = heading.Name
                });
                return PartialView("Success");
            }
            return PartialView(heading);
        }

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
            return PartialView(headingModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HeadingModel heading)
        {
            if (ModelState.IsValid)
            {
                var db = new MainWorker();
                db.Heading.Edit(new HeadingInfo
                {
                    ID = heading.ID,
                    Name = heading.Name
                });
                return PartialView("Success");
            }
            return PartialView(heading);
        }

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
            return PartialView(headingModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(HeadingModel heading)
        {
            if (ModelState.IsValid)
            {
                var db = new MainWorker();
                db.Heading.Delete(heading.ID);
                return PartialView("DeleteImfo");
            }
            return PartialView(heading);
        }
    }
}