using Blog.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Categories
        [Route("Category/{name}")]
        public ActionResult Index(string name, int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);

            if (name == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Category category = db.Categories.FirstOrDefault(x => x.CategoryName == name);
            ViewBag.name = category.CategoryName;
            ViewBag.category = category.Description;
            if (category==null)
            {
                return HttpNotFound();
            }

            return View(category.Articles.OrderByDescending(x => x.ArticleTime).ToList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CategoryNavPartial()
        {
            return PartialView("_CategoryNavPartial",db.Categories.ToList());
        }
    }
}