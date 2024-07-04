using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogDemo.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Deshboard
        public ActionResult Index()
        {
            return View();
        }
    }
}