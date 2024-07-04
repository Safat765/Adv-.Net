using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogDemo.DTO;
using BlogDemo.EF;

namespace BlogDemo.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        BlogSiteEntities db = new BlogSiteEntities();

        public static UserDTO Convert(User u)
        {
            return new UserDTO(){
                Name = u.Name,
                Password = u.Password
            };
        }
        public static User Convert(UserDTO u)
        {
            return new User(){
                Name = u.Name,
                Password = u.Password
            };
        }
        public static List<UserDTO> Convert(List<User> u)
        {
            var list = new List<UserDTO>();
            foreach(var item in u)
            {
                list.Add(Convert(item));
            }
            return list;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserDTO u)
        {
            if (ModelState.IsValid)
            {
                var demo = (from user in db.Users
                            where user.Name.Equals(u.Name) &&
                            user.Password.Equals(u.Password)
                            select user).SingleOrDefault();
                if (demo == null)
                {
                    TempData["msg"] = "User name or password missmatch";
                    return RedirectToAction("Index");
                }
                Session["user"] = demo;
                TempData["msg"] = "Login Successfully";
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(UserDTO u)
        {
            if (ModelState.IsValid)
            {
                var demo = Convert(u);
                db.Users.Add(demo);
                db.SaveChanges();
                TempData["msg"] = "Registration Successfully";
                return RedirectToAction("Index");
            }
            return View(u);
        }

    }
}