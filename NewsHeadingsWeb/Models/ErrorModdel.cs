using System;

namespace NewsHeadingsWeb.Models
{
    /// <summary>
    /// Модель обработки ошибок
    /// </summary>
    public class ErrorModdel
    {
        /// <summary>
        /// Тип ошибки
        /// </summary>
        public Type ExceptionType { get; set; }
        /// <summary>
        /// Текст ошибки
        /// </summary>
        public string ExceptionMessage { get; set; }
        /// <summary>
        /// Описание ошибки
        /// </summary>
        public string ExceptionStackTrace { get; set; }
        public int HttpCode { get; set; }
        //public System.Web.HttpRequest.ErrorDescription Request { get; set; }

    }
}