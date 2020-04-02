using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class PersonellerController : Controller
    {
        // GET: Personeller
        public ActionResult Index()
        {
            return View();
        }
    }
}