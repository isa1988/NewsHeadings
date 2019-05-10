namespace NewsHeadingsWeb.Controllers
{
    using System;
    using System.Web.Mvc;
    using Moveax.Mvc.ErrorHandler;
    using NewsHeadingsWeb.Models;

    public class ErrorController : ErrorHandlerControllerBase
    {
        /// <summary>Default action.</summary>
        /// <param name="errorDescription">The error description.</param>
        public override ActionResult Default(ErrorDescription errorDescription)
        {
            ErrorModdel errorModdel = new ErrorModdel
            {
                ExceptionType = errorDescription.Exception.GetType(),
                ExceptionMessage = errorDescription.Exception.Message,
                ExceptionStackTrace = errorDescription.Exception.StackTrace,
                HttpCode = errorDescription.HttpCode
                //Request = errorDescription.Request
            };
            return this.View("Default", errorModdel);
        }

        /// <summary>HTTP 404 Error: Not found.</summary>
        /// <param name="errorDescription">The error description.</param>
        public ActionResult Http404(ErrorDescription errorDescription)
        {
            ErrorModdel errorModdel = new ErrorModdel
            {
                ExceptionType = errorDescription.Exception.GetType(),
                ExceptionMessage = errorDescription.Exception.Message,
                ExceptionStackTrace = errorDescription.Exception.StackTrace,
                HttpCode = errorDescription.HttpCode
                //Request = errorDescription.Request
            };
            return this.View(errorModdel);
        }
        
        /// <summary>Called before the action method is invoked. Use it for error logging etc</summary>
        /// <param name="errorDescription">The error description.</param>
        protected override void HandleError(ErrorDescription errorDescription)
        {
            // TODO: Add a logging code here (just a reminder).
        }
    }
}
