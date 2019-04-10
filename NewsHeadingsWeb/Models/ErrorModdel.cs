using System;

namespace NewsHeadingsWeb.Models
{
    public class ErrorModdel
    {
        public Type ExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public int HttpCode { get; set; }
        //public System.Web.HttpRequest.ErrorDescription Request { get; set; }

    }
}