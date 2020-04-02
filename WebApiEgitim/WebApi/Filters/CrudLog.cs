using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using WebApi.DataModel;
using WebApi.Helper;

namespace WebApi.Filters
{
    public class CrudLog : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            NorthwindEntities db = new NorthwindEntities();

            CrudLogs crudLog = new CrudLogs();

            crudLog.LogDate = DateTime.Now;
            var headerLocation = actionExecutedContext.Response.Headers.Location;
            var responseHeaderArray = headerLocation.Query.Split('=');
            crudLog.CrudOperation = responseHeaderArray[responseHeaderArray.Length-1].ToString() ?? null;

            var responseObjectName = (actionExecutedContext.Response.Content as ObjectContent).ObjectType.Name.ToString();
            var controllerName = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName.ToString();
            crudLog.MainTable = responseObjectName.Contains("List") == true ? controllerName : responseObjectName;

            //var recordId = (actionExecutedContext.Response.Content as ObjectContent).Value.GetType().GetProperties().Where(k => k.Name.ToUpper().Contains("ID")).FirstOrDefault().GetValue((actionExecutedContext.Response.Content as ObjectContent).Value);
            //var recordID = headerLocation.Query.Split('&')[0].Split('=')[1];
            var recordId2 = headerLocation.Segments[headerLocation.Segments.Length - 1] ?? null;

            crudLog.RecordId = crudLog.CrudOperation == CrudeStatusCode.Select.ToString() ? 0 : Convert.ToInt32(recordId2);

            //crudLog.RecordId = recordId2.GetType().Name == typeof(string).Name ? 0 : Convert.ToInt32(recordId2);
            db.CrudLogs.Add(crudLog);
            db.SaveChanges();
            base.OnActionExecuted(actionExecutedContext);
            
        }
    }
}