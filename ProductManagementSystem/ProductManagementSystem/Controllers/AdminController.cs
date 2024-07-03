using ProductManagementSystem.DTO;
using ProductManagementSystem.Auth;
using ProductManagementSystem.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

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
        public ActionResult Order()
        {
            var data = db.Orders.ToList();
            return View(OrderController.Convert(data));
        }
        public ActionResult OrderDetails(int id)
        {
            var data = (from od in db.OrderProducts
                        where od.OId == id
                        select od).ToList();

            //return View(data);

            var con = new MapperConfiguration(cfg => { 
                cfg.CreateMap<OrderProduct, OrderProductDTO>(); 
                cfg.CreateMap<Product, ProductDTO>(); 
            });
            var mapper = new Mapper(con);
            var pd = mapper.Map<List<OrderProductDTO>>(data);

            return View(pd);
        }
        public ActionResult DeclineOrder(int id)
        {
            var demo = db.Orders.Find(id);
            demo.Status = "Decliened";
            db.SaveChanges();
            TempData["msg"] = "Order id " + id + " has been decliened";

            return RedirectToAction("Order");
        }
        public ActionResult AcceptOrder(int id)
        {
            var demo = db.Orders.Find(id);
            var orderProduct = demo.OrderProducts;

            foreach (var item in orderProduct)
            {
                item.Product.Qty -= item.Qty;
            }

            demo.Status = "Processing";
            db.SaveChanges();
            TempData["msg"] = "Order id " + id + " processing";

            return RedirectToAction("Order");
        }

    }
}