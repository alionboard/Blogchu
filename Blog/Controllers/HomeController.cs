using Blog.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class HomeController : BaseController
    {
        readonly Random rnd = new Random();
        public ActionResult Index(int? page)
        {

            ViewBag.headerPosts = GetHeaderPosts();

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(db.Articles.OrderByDescending(x => x.ArticleTime).ToList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult LatestPostsPartial()
        {
            return PartialView("_LatestPostsPartial", db.Articles.OrderByDescending(x => x.ArticleTime).Take(6).ToList());
        }

        private List<Article> GetHeaderPosts()
        {
            List<Article> articles = new List<Article>();
            int count = 3;
            int articleCount = db.Articles.Count();
            if (articleCount < 3)
                count = articleCount;
            Article lastArticle = db.Articles.OrderByDescending(x => x.Id).FirstOrDefault();

            for (int i = 0; i < count; i++)
            {
                int randomNumber = rnd.Next(1, lastArticle.Id+1);
                Article article = db.Articles.FirstOrDefault(x => x.Id == randomNumber);
                if (article == null || articles.FirstOrDefault(x => x.Id == article.Id) != null || string.IsNullOrEmpty(article.PhotoPath))
                    i--;
                else
                    articles.Add(article);
            }
            return articles;
        }


    }
}