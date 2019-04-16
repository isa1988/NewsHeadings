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
    /// Рубрика контроллер
    /// </summary>
    public class HeadingController : Controller
    {
        private IDataProvider dataProvider;

        /// <summary>
        /// Рубрика контроллер
        /// </summary>
        /// <param name="dataProvider">Работа с данными</param>
        public HeadingController(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }
        /// <summary>
        /// Добавление рубрики
        /// </summary>
        /// <returns></returns>
        public ActionResult Insert()
        {
            return PartialView("Insert", new HeadingModel{Title = "Добавление рубрики" });
        }

        /// <summary>
        /// Post метод для добавление рубрики
        /// </summary>
        /// <param name="heading">Модель рубрики</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insert(HeadingModel heading)
        {
            if (ModelState.IsValid)
            {
                dataProvider.Heading.Insert(new HeadingInfo
                {
                    ID = heading.ID,
                    Name = heading.Name
                });
                return PartialView("Success");
            }
            return PartialView(heading);
        }

        /// <summary>
        /// Редактирование рубрики
        /// </summary>
        /// <param name="id">ID рубрики</param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            HeadingInfo headingInfo = dataProvider.Heading.GetByID(id);
            HeadingModel headingModel = new HeadingModel
            {
                ID = headingInfo.ID,
                Name = headingInfo.Name,
                PathLink = headingInfo.PathLink,
                Title = "Редактирование рубрики"
            };
            return PartialView(headingModel);
        }

        /// <summary>
        /// Post метод для редактирования рубрики
        /// </summary>
        /// <param name="heading">Модель рурики</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HeadingModel heading)
        {
            if (ModelState.IsValid)
            {
                dataProvider.Heading.Edit(new HeadingInfo
                {
                    ID = heading.ID,
                    Name = heading.Name
                });
                return PartialView("Success");
            }
            return PartialView(heading);
        }

        /// <summary>
        /// Удаление рубрики 
        /// </summary>
        /// <param name="id">ID рубрики</param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            HeadingInfo headingInfo = dataProvider.Heading.GetByID(id);
            HeadingModel headingModel = new HeadingModel
            {
                ID = headingInfo.ID,
                Name = headingInfo.Name,
                PathLink = headingInfo.PathLink,
                Title = "Удаление рубрики"
            };
            return PartialView(headingModel);
        }

        /// <summary>
        /// Post метод для удаления рублрики
        /// </summary>
        /// <param name="heading">Модель рубрики</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(HeadingModel heading)
        {
            if (ModelState.IsValid)
            {
                dataProvider.Heading.Delete(heading.ID);
                return PartialView("DeleteImfo");
            }
            return PartialView(heading);
        }
    }
}