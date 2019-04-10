using DataBase.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace NewsHeadingsWeb.Models
{
    /// <summary>
    /// Статьи новостей мрдель 
    /// </summary>
    public class ArticleModel : PageModel
    {
        /// <summary>
        /// Индефикатор
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        [DisplayName("Наименование")]
        public string Name { get; set; }

        /// <summary>
        /// Текст статьи
        /// </summary>
        [DisplayName("Текст")]
        public string Text { get; set; }
        /// <summary>
        /// Автор
        /// </summary>
        [DisplayName("Автор")]
        public string Author { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// Наименование файла
        /// </summary>
        [DisplayName("Наименование файла")]
        public string FileName { get; set; }

        /// <summary>
        /// Удалить файл
        /// </summary>
        [DisplayName("Удалить картинку")]
        public bool IsDelete { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public byte[] FileMain { get; set; }

        [DisplayName("Рублика")]
        public int HeadingID { get; set; }
        public HeadingModel HeadingCurr { get; set; }
        /// <summary>
        /// Список рублик для DropDownListFor
        /// </summary>
        public List<SelectListItem> Headings { get; set; }
        
        public override string ToString()
        {
            return Name;
        }
    }
}