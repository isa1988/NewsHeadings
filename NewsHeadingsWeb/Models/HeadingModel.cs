﻿using DataBase.DataModel;
using System.Collections.Generic;
using System.ComponentModel;

namespace NewsHeadingsWeb.Models
{
    /// <summary>
    /// Рубрика модель
    /// </summary>
    public class HeadingModel : PageModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        [DisplayName("Наименование")]
        public string Name { get; set; }
        /// <summary>
        // Путь в адресной строки 
        /// </summary>
        [DisplayName("Путь в адресной строки")]
        public string PathLink { get; set; }
        /// <summary>
        /// Статьи к данной рубрики 
        /// </summary>
        public List<ArticleModel> Articles { get; set; }
        /// <summary>
        /// Список рубрик
        /// </summary>
        public List<HeadingModel> Headings { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}