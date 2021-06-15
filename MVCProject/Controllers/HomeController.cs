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

        public ActionResult Index(string searchText, string currentFilter, int? page, int? pageSize)
        {
            //var model = new ProductFileViewModel()
            //{
            //    Products = GetProducts(),
            //    Files = db.Files.Include(x => x.Product_Files),
            //};
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

        public ActionResult Popular()
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