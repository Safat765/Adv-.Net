using ProductManagementSystem.DTO;
using ProductManagementSystem.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagementSystem.Controllers
{
    public class LoginController : Controller
    {

        PMSDemoEntities db = new PMSDemoEntities();
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginDTO l)
        {
            if (ModelState.IsValid)
            {
                var user = (from u in db.Users
                            where u.UName.Equals(l.UName) && 
                            u.Password.Equals(l.Password)
                            select u).SingleOrDefault();
                if (user == null)
                {
                    TempData["msg"] = "User name or password missmatch";
                    return RedirectToAction("Index");
                }
                Session["user"] = user;
                TempData["msg"] = "Login Successful";
                if (user.Type.Equals("admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                return RedirectToAction("Index", "Order");
            }
            return View();
        }
    }
}