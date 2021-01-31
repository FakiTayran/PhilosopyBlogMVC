using BlogMVC.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BlogMVC.Controllers
{
    public class ArticleController : BaseController
    {
        // GET: Article
        //[Route("Article/{id}")]
        public ActionResult Index(int? id)
        {
            ViewBag.reportTypes = new SelectList(db.ReportCategories.ToList(), "Id", "Name");
            ViewBag.Active = "MyArticle";
            Article article = db.Articles.FirstOrDefault(x => x.Id == id);
            ViewBag.popularArticle = db.Articles.ToList();
            return View(article);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
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

                ApplicationUser user = db.Users.Find(comment.ApplicationUserId);
                CommentAddedViewModel result = new CommentAddedViewModel()
                {
                    CommentSuccess = "Your commend successfuly added.",
                    Author = user.NickName,
                    Content = comment.Content,
                    CommentId = comment.Id,
                    CommentTime = comment.CommentTime.ToString("g")
                };
                return Json(result);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { ErrorMessage = "Please control your last message" });
        }

        [HttpPost]
        public ActionResult SendReport(int? articleId, int? reportType)
        {
            if (reportType == null)
            {
                return HttpNotFound();
            }
            Report report = new Report();
            report.ApplicationUserId = User.Identity.GetUserId();
            report.Article = db.Articles.Find(articleId);
            report.ReportCategory = db.ReportCategories.Find(reportType);
            report.ReportTime = DateTime.Now;
            report.IsInvestigated = false;
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
                like.Islike = true;
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