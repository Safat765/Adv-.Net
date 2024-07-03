using ProductManagementSystem.Auth;
using ProductManagementSystem.DTO;
using ProductManagementSystem.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagementSystem.Controllers
{
    [Customer]
    // authorization overcome korar jonno [AllowAnonymous] use kora hoi
    public class OrderController : Controller
    {
        // GET: Order

        PMSDemoEntities db = new PMSDemoEntities();
        public ActionResult Index()
        {
            var demo = db.Products.ToList();
            return View(ProductController.Convert(demo));
        }
        public ActionResult AddToCart(int id)
        {
            var demo = db.Products.Find(id);
            var con = ProductController.Convert(demo);
            
            con.Qty = 1;

            if (Session["cart"] == null)
            {
                var cart = new List<ProductDTO>(); // boxing
                cart.Add(con);
                Session["cart"] = cart;
            }
            else
            {
                var cart = Session["cart"];
                var data = (List<ProductDTO>)cart; // unboxing
                data.Add(con);
                Session["cart"] = data;
            }
            
            TempData["msg"] = con.Name + " Added on cart Successfully";

            return RedirectToAction("Index");
        }
        public ActionResult Cart()
        {
            var demo = Session["cart"];
            if(demo == null)
            {
                TempData["msg"] = "Cart is empty";
                return RedirectToAction("Index");
            }
            var data = (List<ProductDTO>)demo; // unboxing
            return View(data);
        }
        [HttpPost]
        public ActionResult PlaceOrder(decimal Total)
        {
            var order = new Order();
            order.OrderDate = DateTime.Now;
            order.Status = "Ordered";
            order.TotalAmount = Total;
            order.UId = ((User)Session["user"]).Id;
            db.Orders.Add(order);
            db.SaveChanges();

            var cart = (List<ProductDTO>)Session["cart"];

            
            foreach(var item in cart)
            {
                var op = new OrderProduct();
                op.Qty = item.Qty;
                op.PId = item.Id;
                op.UnitPrice = item.Price;
                op.OId = order.Id;
                db.OrderProducts.Add(op);
            }
            db.SaveChanges();

            Session["cart"] = null;
            TempData["msg"] = "Order has benn placed successfully";
            return RedirectToAction("Index");
        }
        public static OrderDTO Convert(Order o)
        {
            return new OrderDTO(){
                Id = o.Id,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                OrderDate = o.OrderDate,
                UId = o.UId
            };
        }
        public static Order Convert(OrderDTO o)
        {
            return new Order(){
                Id = o.Id,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                OrderDate = o.OrderDate,
                UId = o.UId
            };
        }
        public static List<OrderDTO> Convert(List<Order> o)
        {
            var list = new List<OrderDTO>();
            foreach (var item in o)
            {
                list.Add(Convert(item));
            }
            return list;
        }
    }
}