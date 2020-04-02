using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.DataModel;

namespace WebApi.Helper
{
    public class Logger
    {
        public static void Logla(string message, string messagedetail)
        {
            NorthwindEntities db = new NorthwindEntities();
            ApiLogs log = new ApiLogs();
            log.LogDate = DateTime.Now;
            log.Exception = message;
            log.StackTrace = messagedetail;
            db.ApiLogs.Add(log);
            db.SaveChanges();

        }
    }
}