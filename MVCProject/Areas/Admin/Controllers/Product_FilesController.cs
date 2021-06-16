using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models;

namespace MVCProject.Areas.Admin.Controllers
{
    public class Product_FilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Product_Files
        public ActionResult Index()
        {
            var product_Files = db.Product_Files.Include(p => p.File).Include(p => p.Product);
            return View(product_Files.ToList());
        }

        // GET: Admin/Product_Files/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_File product_File = db.Product_Files.Find(id);
            if (product_File == null)
            {
                return HttpNotFound();
            }
            return View(product_File);
        }

        // GET: Admin/Product_Files/Create
        public ActionResult Create()
        {
            ViewBag.FileID = new SelectList(db.Files, "FileID", "Name");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            return View();
        }

        // POST: Admin/Product_Files/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_FileID,ProductID,FileID")] Product_File product_File)
        {
            if (ModelState.IsValid)
            {
                db.Product_Files.Add(product_File);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FileID = new SelectList(db.Files, "FileID", "Name", product_File.FileID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", product_File.ProductID);
            return View(product_File);
        }

        // GET: Admin/Product_Files/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_File product_File = db.Product_Files.Find(id);
            if (product_File == null)
            {
                return HttpNotFound();
            }
            ViewBag.FileID = new SelectList(db.Files, "FileID", "Name", product_File.FileID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", product_File.ProductID);
            return View(product_File);
        }

        // POST: Admin/Product_Files/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_FileID,ProductID,FileID")] Product_File product_File)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_File).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FileID = new SelectList(db.Files, "FileID", "Name", product_File.FileID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", product_File.ProductID);
            return View(product_File);
        }

        // GET: Admin/Product_Files/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_File product_File = db.Product_Files.Find(id);
            if (product_File == null)
            {
                return HttpNotFound();
            }
            return View(product_File);
        }

        // POST: Admin/Product_Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_File product_File = db.Product_Files.Find(id);
            db.Product_Files.Remove(product_File);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
