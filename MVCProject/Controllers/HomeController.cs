using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models;
using PagedList;
using PagedList.Mvc;

namespace MVCProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string searchText, string currentFilter, int? page)
        {
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
            return View(products.ToPagedList(page ?? 1, 1));
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