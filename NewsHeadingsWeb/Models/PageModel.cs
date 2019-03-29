using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsHeadingsWeb.Models
{
    /// <summary>
    /// Модель страниц 
    /// </summary>
    public class PageModel
    {
        /// <summary>
        /// Название страницы
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Текст ошибки
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}