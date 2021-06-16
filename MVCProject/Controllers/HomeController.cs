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
using PagedList;
using PagedList.Mvc;
using Rotativa;

namespace MVCProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string searchText, string currentFilter, int? page, int? pageSize)
        {
            //var model = new ProductFileViewModel()
            //{
            //    Products = GetProducts(),
            //    Files = db.Files.Include(x => x.Product_Files),
            //};
            var productFiles = db.Product_Files.Include(p => p.File).ToList();
            ViewBag.Image = productFiles;

            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            int defaSize = (pageSize ?? 5);
            ViewBag.psize = defaSize;

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
             };
            if (searchText != null)
            {
                page = 1;
            }
            else
            {
                searchText = currentFilter;
            }
            ViewBag.CurrentFilter = searchText;
            var products = GetProducts();
            if (!String.IsNullOrEmpty(searchText))
            {
                products = products.Where(a => a.Name.ToLower().Contains(searchText?.ToLower()) || a.Producer.Name.ToLower().Contains(searchText?.ToLower())).ToList();
            }
            return View(products.ToPagedList(pageIndex, defaSize));
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
            products = products.OrderByDescending(i => i.Sold_units).Take(1).ToList();
            return View(products);
        }

        

        //public PartialViewResult SearchProducts(string searchText)
        //{
        //    var products = GetProducts();
        //    if(!String.IsNullOrEmpty(searchText))
        //    {
        //        var result = products.Where(a => a.Name.ToLower().Contains(searchText?.ToLower()) || a.Producer.Name.ToLower().Contains(searchText?.ToLower())).ToList();
        //        return PartialView("_GridView", result);
        //    }
        //    return PartialView(products);
        //}

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
            var productFiles = db.Product_Files.Include(p => p.File).Where(p => p.ProductID == id).ToList();
            ViewBag.Image = productFiles;
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

        public ActionResult PDF()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        public ActionResult PrintPartialViewToPdf(int id)
        {
            var category = db.Categories.Where(c => c.CategoryID == id).ToList();
            ViewBag.Products = db.Products.Include(x=>x.Producer).Where(x=>x.CategoryID == id).ToList();
            var report = new PartialViewAsPdf("~/Views/Home/_PDFDetails.cshtml", category);
            return report;
        }
    }
}