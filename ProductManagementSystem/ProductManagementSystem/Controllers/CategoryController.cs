using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductManagementSystem.DTO;
using ProductManagementSystem.EF;

namespace ProductManagementSystem.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category

        PMSDemoEntities db = new PMSDemoEntities();

        public static CategoryDTO Convert(Category c)
        {
            return new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name
            };
        }
        public static Category Convert(CategoryDTO c)
        {
            return new Category
            {
                Id = c.Id,
                Name = c.Name
            };
        }
        public static List<CategoryDTO> Convert(List<Category> cl)
        {
            var list = new List<CategoryDTO>();
            foreach(var c in cl)
            {
                list.Add(Convert(c));
            }
            return list;
        }
        public ActionResult Index()
        {
            var demo = db.Categories.ToList();
            return View(Convert(demo));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var demo = db.Categories.Find(id);
            return View(Convert(demo));
        }
        [HttpPost]
        public ActionResult Edit(CategoryDTO c)
        {
            if (ModelState.IsValid)
            {
                var demo = db.Categories.Find(c.Id);
                demo.Name = c.Name;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(c);
        }
    }
}