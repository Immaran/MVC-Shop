using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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
    }
}