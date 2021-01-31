using BlogMVC.Controllers;
using BlogMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminArticleController : BaseController
    {
        // GET: Admin/Article
        public ActionResult Index(string status)
        {
            ViewBag.categorySuccess = status;
            ViewBag.active = "Articles";
            ViewBag.adminCategories = db.Categories.ToList();
            return View(db.Articles.ToList());
        }

        public ActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            db.Entry(category).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteArticle(int? id)
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
            TempData["ArticleDelete"] = "success";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddCategory(string categoryName, string description)
        {
            string status = "";
            if (categoryName != null && description != null)
            {
                Category category = new Category();
                category.Name = categoryName.Trim();
                category.Description = description.Trim();
                db.Entry(category).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                status = "Category Successfuly Added";
            }
            else
            {
                status = "Category is not Added";
            }
            return RedirectToAction("Index", new { status = status });
        }

        public JsonResult EditCategory(string id)
        {
            int categoryId = Convert.ToInt32(id.Substring(4));
            Category category = db.Categories.Find(categoryId);
            return Json(new { Id = category.Id, Description = category.Description, Name = category.Name }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditCategory(int catId, string editCategoryName, string editDescription)
        {
            TempData["status"] = "error";
            if (catId != 0 && editCategoryName != null && editDescription != null && editCategoryName.Length <= 30 && editCategoryName.Length <= 300)
            {
                Category category = db.Categories.Find(catId);
                category.Name = editCategoryName.Trim();
                category.Description = editDescription.Trim();
                db.SaveChanges();
                TempData["status"] = "success";
            }
            return RedirectToAction("Index");
        }

        public ActionResult ArticleDetails(int? id,bool? IsInvestigated,int? reportId)
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
            if (IsInvestigated != null && reportId != null)
            {
                Report report = db.Reports.Find(reportId);
                report.IsInvestigated = IsInvestigated;
                db.SaveChanges();
            }
            return View(article);
        }

        public ActionResult DeleteComment(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Comment comment = db.Comments.Find(id);
            
            if (comment == null)
            {
                return HttpNotFound();
            }
            db.Entry(comment).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            TempData["commentProcess"] = "true";
            return RedirectToAction("ArticleDetails",new {id = comment.ArticleId });
        }
    }
}