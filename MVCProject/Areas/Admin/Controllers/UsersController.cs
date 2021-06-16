using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        // GET: Admin/User

        public ActionResult Index()
        {
            var db = new ApplicationDbContext();
            return View(db.Users.ToList());
        }

        public ActionResult Delete(string id)
        {
            var db = new ApplicationDbContext();
            var user = db.Users.Where(u => u.Id == id).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public ActionResult Delete(ApplicationUser appuser)
        {
            var db = new ApplicationDbContext();
            var user = db.Users.Where(u => u.Id == appuser.Id).FirstOrDefault();
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("UserList");
        }

        public ActionResult Edit(string id)
        {
            var context = new ApplicationDbContext();
            var user = context.Users.Where(u => u.Id == id).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(ApplicationUser appuser)
        {
            var context = new ApplicationDbContext();
            var user = context.Users.Where(u => u.Id == appuser.Id).FirstOrDefault();
            user.Email = appuser.Email;
            user.UserName = appuser.UserName;
            user.PhoneNumber = appuser.PhoneNumber;
            user.PasswordHash = user.PasswordHash;
            context.SaveChanges();
            return RedirectToAction("UserList");
        }
    }
}