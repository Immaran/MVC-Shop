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
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Address).Include(o => o.DeliveryMethod).Include(o => o.Invoice).Include(o => o.PaymentMethod).Include(o => o.Status).Include(o => o.User);
            return View(orders.ToList());
        }

        // GET: Admin/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin/Orders/Create
        public ActionResult Create()
        {
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Name");
            ViewBag.DeliveryMethodID = new SelectList(db.DeliveryMethods, "DeliveryMethodID", "Name");
            ViewBag.OrderID = new SelectList(db.Invoices, "OrderID", "Name");
            ViewBag.PaymentMethodID = new SelectList(db.PaymentMethods, "PaymentMethodID", "Name");
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName");
            ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,UserID,AddressID,DeliveryMethodID,StatusID,PaymentMethodID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Name", order.AddressID);
            ViewBag.DeliveryMethodID = new SelectList(db.DeliveryMethods, "DeliveryMethodID", "Name", order.DeliveryMethodID);
            ViewBag.OrderID = new SelectList(db.Invoices, "OrderID", "Name", order.OrderID);
            ViewBag.PaymentMethodID = new SelectList(db.PaymentMethods, "PaymentMethodID", "Name", order.PaymentMethodID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName", order.StatusID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName", order.UserID);
            return View(order);
        }

        // GET: Admin/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Name", order.AddressID);
            ViewBag.DeliveryMethodID = new SelectList(db.DeliveryMethods, "DeliveryMethodID", "Name", order.DeliveryMethodID);
            ViewBag.OrderID = new SelectList(db.Invoices, "OrderID", "Name", order.OrderID);
            ViewBag.PaymentMethodID = new SelectList(db.PaymentMethods, "PaymentMethodID", "Name", order.PaymentMethodID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName", order.StatusID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName", order.UserID);
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,UserID,AddressID,DeliveryMethodID,StatusID,PaymentMethodID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Name", order.AddressID);
            ViewBag.DeliveryMethodID = new SelectList(db.DeliveryMethods, "DeliveryMethodID", "Name", order.DeliveryMethodID);
            ViewBag.OrderID = new SelectList(db.Invoices, "OrderID", "Name", order.OrderID);
            ViewBag.PaymentMethodID = new SelectList(db.PaymentMethods, "PaymentMethodID", "Name", order.PaymentMethodID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName", order.StatusID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName", order.UserID);
            return View(order);
        }

        // GET: Admin/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
