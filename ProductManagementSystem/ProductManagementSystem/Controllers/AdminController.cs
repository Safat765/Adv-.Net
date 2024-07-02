using ProductManagementSystem.Auth;
using ProductManagementSystem.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagementSystem.Controllers
{
    [Admin]
    public class AdminController : Controller
    {
        PMSDemoEntities db = new PMSDemoEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session["user"] = null;

            TempData["msg"] = "Logout Successfully";

            return RedirectToAction("Index", "Login");
        }

    }
}