using Blog.Controllers;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminArticleController : BaseController
    {
        // GET: Admin/Article
        public ActionResult Index()
        {

            ViewBag.adminCategories = db.Categories.ToList();
            return View(db.Articles.ToList());
        }

        public ActionResult DeleteArticle(int? id,bool? isReported)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }

            db.Articles.Remove(article);
            db.SaveChanges();
            if (isReported!=null)
            {
                return RedirectToAction("Index","Reports");
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCategory(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddCategory(string categoryName, string description)
        {
            TempData["status"] = "error";
            if (categoryName != null && description != null && categoryName.Length <= 30 && description.Length <= 300)
            {
                Category category = new Category() { CategoryName = categoryName.Trim(), Description = description.Trim() };
                db.Categories.Add(category);
                db.SaveChanges();
                TempData["status"] = "success";
            }
            return RedirectToAction("Index");
        }

        public JsonResult EditCategory(string id)
        {
            int categoryId = Convert.ToInt32(id.Substring(4));
            Category category = db.Categories.Find(categoryId);
            return Json(new { Id = category.Id, Description = category.Description, Name = category.CategoryName }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditCategory(int catId, string editCategoryName, string editDescription)
        {
            TempData["status"] = "error";
            if (catId != 0 && editCategoryName != null && editDescription != null && editCategoryName.Length <= 30 && editCategoryName.Length <= 300)
            {
                Category category = db.Categories.Find(catId);
                category.CategoryName = editCategoryName.Trim();
                category.Description = editDescription.Trim();
                db.SaveChanges();
                TempData["status"] = "success";
            }
            return RedirectToAction("Index");
        }


        public ActionResult ArticleDetails(int? id, bool? isInvestigated, int? reportId)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            if (isInvestigated!=null && reportId !=null)
            {
                Report report = db.Reports.Find(reportId);
                report.IsInvestigated = true;
                db.SaveChanges();
            }

            Article article = db.Articles.Find(id);

            return View(article);
        }

        public ActionResult DeleteComment(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            db.Comments.Remove(comment);
            db.SaveChanges();
            TempData["commentProcess"] = "true";
            return RedirectToAction("ArticleDetails", new { id = comment.ArticleId });
        }
    }
}