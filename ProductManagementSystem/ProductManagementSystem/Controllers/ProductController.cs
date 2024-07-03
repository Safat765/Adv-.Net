using ProductManagementSystem.DTO;
using ProductManagementSystem.EF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace ProductManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        // GET: Order

        PMSDemoEntities db = new PMSDemoEntities();

        public static ProductDTO Convert(Product c)
        {
            return new ProductDTO
            {
                Id = c.Id,
                Name = c.Name,
                Price = c.Price,
                Qty = c.Qty,
                CId = c.CId
            };
        }
        public static Product Convert(ProductDTO c)
        {
            return new Product
            {
                Id = c.Id,
                Name = c.Name,
                Price = c.Price,
                Qty = c.Qty,
                CId = c.CId
            };
        }
        public static List<ProductDTO> Convert(List<Product> cl)
        {
            var list = new List<ProductDTO>();
            foreach (var c in cl)
            {
                list.Add(Convert(c));
            }
            return list;
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}