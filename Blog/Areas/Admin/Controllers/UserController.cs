using Blog.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Areas.Admin.Controllers
{

    [Authorize(Roles ="admin")]
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            ViewBag.Articles = db.Articles.ToList();
            ViewBag.Roles = db.Roles.ToList();
            return View(db.Users.ToList());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteUser(string userId)
        {
            TempData["result"] = "failed";

            if (!string.IsNullOrEmpty(userId))
            {
                var user = UserManager.FindById(userId);
                if (UserManager.IsInRole(user.Id, "admin"))
                {
                    TempData["result"] = "deleteAdminError";
                    return RedirectToAction("Index");
                }
                UserManager.Delete(user);
                TempData["result"] = "succeed";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AssignRole(string userId,bool isAdmin)
        {
            TempData["result"] = "roleFailed";
            if (!string.IsNullOrEmpty(userId))

            {
                var roleStore = new RoleStore<IdentityRole>(db);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var adminRole = roleManager.FindByName("admin");
                var adminCount = adminRole.Users.Count;

                if (adminCount == 1 && !isAdmin)
                {
                    TempData["result"] = "onlyAdmin";
                    return RedirectToAction("Index");

                }

                if (isAdmin)
                {
                    UserManager.AddToRole(userId, "admin");
                    TempData["result"] = "roleAssigned";
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["result"] = "roleUnassigned";
                    UserManager.RemoveFromRole(userId, "admin");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}