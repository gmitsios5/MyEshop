using Microsoft.AspNetCore.Mvc;
using MyEshop.Areas.Identity.Data;
using System.Net;
using MyEshop.Models;
using System.Linq;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyEshop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly MyEshopDBContext _context;

        public ShoppingCartController(MyEshopDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        /*METHOD */
        public ActionResult OrderNow(int? id)
        {
            /*HttpContext.Session.SetInt32("ShoppingCart",id);*/
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }



            //This gives you the byte array.

            /*iF CART IS EMPTY THEN ADD PRODUCT*/
            if (HttpContext.Session.Get("Mycart") == null)
            {
                List<MyCart> cart = new List<MyCart>
                {
                    new MyCart(_context.Products.Find(id),1)
                };
                string data = Serializer.Serialize(cart);
                HttpContext.Session.SetString("Mycart", data);

            }
            else
                /*CREATE NEW CART LIST WITH PRODUCTS AND REFRESH QUANTITY*/
            {
                List<MyCart> Iscart = Serializer.Deserialize(HttpContext.Session.GetString("Mycart"));
                int check = IsExistingCheck(id);
                if (check == -1)
                {
                    Iscart.Add(new MyCart(_context.Products.Find(id), 1));
                }
                else
                {
                    Iscart[check].Quantity++;
                }

                HttpContext.Session.SetString("Mycart", Serializer.Serialize(Iscart));

            }
            return View("Index");
        }
            /*CHECKS IF PRODUCT EXISTS IN CART*/
        private int IsExistingCheck(int? id)
        {
            List<MyCart> Iscart = Serializer.Deserialize(HttpContext.Session.GetString("Mycart"));
            for (int i = 0; i < Iscart.Count; i++)
            {
                if (Iscart[i].Products.ProductID == id) return i;
            }
            return -1;
        }

        public ActionResult Delete(int id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int check = IsExistingCheck(id);

            List<MyCart> Iscart = Serializer.Deserialize(HttpContext.Session.GetString("Mycart"));
            Iscart.RemoveAt(check);
            HttpContext.Session.SetString("Mycart", Serializer.Serialize(Iscart));
            return View("Index");
        }
         /* UPDATE QUANTITY OF CART*/
        public ActionResult UpdateCart(IFormCollection formc)
        {
            string[] quantities = formc["quantity"];
            List<MyCart> CartList = Serializer.Deserialize(HttpContext.Session.GetString("Mycart"));
            for (int i = 0; i < CartList.Count; i++)
            {
                CartList[i].Quantity = Convert.ToInt32(quantities[i]);
            }
            HttpContext.Session.SetString("Mycart", Serializer.Serialize(CartList));
            return View("Index");
        }
            /*CHECKOUT PAGE*/
        public ActionResult Checkout()
        {
            return View("Checkout");
        }
        /*FINISH ORDER WITH SHIPPING AND CUSTOMER CREDENTIALS*/
        public ActionResult ProcessOrder(IFormCollection formc)
        {
            List<MyCart> CartList = Serializer.Deserialize(HttpContext.Session.GetString("Mycart"));
            Orders order = new Orders()
            {
                CustomerName = formc["customer_full_name"],
                Email = formc["customer_email"],
                Address = formc["customer_address"],
                City = formc["customer_city"],
                PhoneNumber = formc["customer_phone"],
                StateCode = formc["customer_state_code"],
                Notes = formc["customer_notes"],
                OrderDate = DateTime.Now,
                OrderStatus = "In Process",
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            foreach (MyCart cart in CartList)
            {
                OrderDetails orderdetails = new OrderDetails()
                {
                    OrderId = order.OrderID,
                    ProductId = cart.Products.ProductID,
                    Quantity = cart.Quantity,
                    Value = cart.Products.Value
                };
                Products product = _context.Products.FirstOrDefault(p => p.ProductID == orderdetails.ProductId);
                _context.Products.FirstOrDefault(p => p.ProductID == orderdetails.ProductId).Quantity = product.Quantity - orderdetails.Quantity;
                _context.OrderDetails.Add(orderdetails);
                if (product.Quantity < orderdetails.Quantity)
                {
                   //return error msg
                    _context.Orders.Remove(order);
                }
                
            }
            _context.SaveChanges();


            /*HttpContext.Session.SetString("Mycart", Serializer.Serialize(CartList));*/
            return View("ProcessOrder");
        }
    }

    
}
