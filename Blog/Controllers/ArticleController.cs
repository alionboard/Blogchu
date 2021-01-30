using Blog.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class ArticleController : BaseController
    {
        // GET: Article
        [Route("Article/{id}")]
        public ActionResult Index(int? id)
        {
            ViewBag.ReportCategories = new SelectList(db.ReportCategories.ToList(), "Id", "Name");
            Article article = db.Articles.FirstOrDefault(x => x.Id == id);
            return View(article);
        }

        public ActionResult CommentPartial()
        {
            return PartialView("_CommentPartial", db.Comments.ToList());
        }

        public ActionResult AddComment(AddCommentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Comment comment = new Comment()
                {
                    ArticleId = vm.ArticleId,
                    Content = vm.Content,
                    ApplicationUserId = User.Identity.GetUserId(),
                    CommentTime = DateTime.Now
                };
                db.Comments.Add(comment);
                db.SaveChanges();

                CommentAddedViewModel result = new CommentAddedViewModel()
                {
                    CommentSuccess = "Your comment added successfully",
                    Author = User.Identity.GetNickname(),
                    Content = comment.Content,
                    CommentId = comment.Id,
                    CommentTime = comment.CommentTime.Value.ToString("g")
                };

                return Json(result);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { ErrorMessage = "Check your message" });
        }

        [HttpPost]
        public ActionResult SendReport(int? articleId, int? reportId)
        {
            if (reportId == null || articleId == null)
                return HttpNotFound();

            Article article = db.Articles.Find(articleId);
            ReportCategory reportCategory = db.ReportCategories.Find(reportId);
            if (article == null || reportCategory == null)
                return HttpNotFound();

            Report report = new Report() { ReportCategoryId = reportCategory.Id, ApplicationUserId = User.Identity.GetUserId(), ArticleId = article.Id,ReportTime=DateTime.Now,IsInvestigated=false };
            db.Reports.Add(report);
            db.SaveChanges();
            return Json(new EmptyResult());

        }

        [HttpPost]
        public ActionResult Like(int? LikedArticleId)
        {
            if (LikedArticleId == null)
            {
                HttpNotFound();
            }
            string userId = User.Identity.GetUserId();

            if (db.Users.FirstOrDefault(x => x.Id == userId).Likes.Any(x => x.ArticleId == LikedArticleId))
            {
                ApplicationUser user = db.Users.Find(userId);
                Like like = db.Likes.ToList().Where(x => x.ApplicationUser == user).FirstOrDefault(x => x.ArticleId == LikedArticleId);
                Article article = db.Articles.Find(LikedArticleId);
                user.Likes.Remove(like);
                article.Likes.Remove(like);
                db.Likes.Remove(like);
                db.SaveChanges();
                return Json("Unlike");
            }
            else
            {
                Like like = new Like();
                ApplicationUser user = db.Users.Find(userId);
                user.Likes.Add(like);
                Article article = db.Articles.Find(LikedArticleId);
                article.Likes.Add(like);
                db.Likes.Add(like);
                db.SaveChanges();
                return Json("Like");
            }
        }
    }
}