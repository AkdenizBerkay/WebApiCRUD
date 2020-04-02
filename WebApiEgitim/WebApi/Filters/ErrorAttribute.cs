using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace WebApi.Helper
{
    public class ErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            /*
             * Loglama
             * Response Hazırlama
             */

            Logger.Logla(actionExecutedContext.Exception.Message, actionExecutedContext.Exception.StackTrace);
            ErrorResponse errorResponse = new ErrorResponse();
            errorResponse.ErrorAction = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            errorResponse.ErrorController = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
            errorResponse.ErrorMessage = actionExecutedContext.Exception.ToString();
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.BadRequest, errorResponse);
        }
    }
}