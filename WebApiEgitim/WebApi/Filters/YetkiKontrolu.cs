using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebApi.DataModel;
using WebApi.Models;

namespace WebApi.Filters
{
    public class YetkiKontrolu : ActionFilterAttribute
    {
        NorthwindEntities db = new NorthwindEntities();
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var controllerName = actionContext.ActionDescriptor.ActionName;

            Users currentuser = Helper.UserInfo.CurrentUser();

            List<string> Roller = (from ur in db.UserRoles.ToList()
                                   join r in db.Roles.ToList() on ur.RolId equals r.RolId
                                   where ur.UserId == currentuser.UserId
                                   select r.RolName).ToList();

            if (Roller.Contains(controllerName))
            {
                base.OnActionExecuting(actionContext);
            }

            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Yetkiniz Yok");
            }
            
        }
    }
}