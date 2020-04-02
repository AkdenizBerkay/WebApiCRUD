using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApi.Filters
{
    public class PersonelAddValidation : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            if (actionContext.ModelState.IsValid)
            {
                base.OnActionExecuting(actionContext);
            }
            else
            {
                var errors = actionContext.ModelState.Values.SelectMany(k => k.Errors).Select(l => l.ErrorMessage).ToList();
                var errormessages = string.Join(",", errors);
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errormessages);
            }
        }
    }
}