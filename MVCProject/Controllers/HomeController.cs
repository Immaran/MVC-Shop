using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MVCProject.Models;

namespace MVCProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var products = GetProducts();
            return View(products.ToList());
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public async Task<ActionResult> CheckoutDetails()
        {
            var userId = User.Identity.GetUserId();

            List<Address> addresses = await db.Addresses.Where(x => x.UserID == userId).ToListAsync();
            List<PaymentMethod> paymentMethods = await db.PaymentMethods.ToListAsync();
            List<DeliveryMethod> deliverytMethods = await db.DeliveryMethods.ToListAsync();

            ViewBag.Addresses = addresses;
            ViewBag.Payment = paymentMethods;
            ViewBag.Delivery = deliverytMethods;

            if (Session["cart"] == null)
            {
                return Redirect("Index");
            }
            else if (((List<Item>)Session["cart"]).Count == 0)
            {
                return Redirect("Index");
            }

            List<Item> cart = (List<Item>)Session["cart"];

            cart.RemoveAll(item => item.Quantity == 0);

            Session["cart"] = cart;

            return View();
        }

        public PartialViewResult SearchProducts(string searchText)
        {
            var products = GetProducts();
            var result = products.Where(a => a.Name.ToLower().Contains(searchText.ToLower()) || a.Producer.Name.ToLower().Contains(searchText.ToLower())).ToList();
            return PartialView("_GridView", result);
        }

        public List<Product> GetProducts()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Producer).Include(p => p.Tax);
            return products.ToList();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult AddToCart(int productId, string url)
        {
            Product product = db.Products.Find(productId);
            List<Item> cart;

            if (Session["cart"] == null)
            {
                cart = new List<Item>();
                cart.Add(new Item() { Product = product, Quantity = 1 });
            }
            else
            {
                cart = (List<Item>)Session["cart"];
                bool added = false;

                foreach (var item in cart)
                {
                    if (item.Product.ProductID == productId)
                    {
                        int quantity = item.Quantity;

                        cart.Remove(item);

                        cart.Add(new Item() { Product = product, Quantity = quantity + 1 });

                        added = true;
                        
                        break;
                    }
                }

                if (!added)
                {
                    cart.Add(new Item() { Product = product, Quantity = 1 });
                }
            }
            Session["cart"] = cart;

            return Redirect(url);
        }

        public ActionResult DecreaseQty(int productId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = db.Products.Find(productId);

                foreach (var item in cart)
                {
                    if (item.Product.ProductID == productId)
                    {
                        int quantity = item.Quantity;
                        if (quantity > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = quantity - 1
                            });
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Checkout");
        }

        public ActionResult RemoveFromCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];

            foreach (var item in cart)
            {
                if(item.Product.ProductID == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;

            return Redirect("Index");
        }
    }
}