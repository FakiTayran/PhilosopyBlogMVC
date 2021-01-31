using BlogMVC.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMVC.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        // GET: Profil
        public ActionResult Index()
        {
            ViewBag.Active = "MyArticles";
            var user = User.Identity.GetUserId();
            return View(db.Articles.Where(x => x.ApplicationUserId == user).OrderByDescending(x => x.ArticleTime).ToList());
        }
        public ActionResult NewArticle()
        {
            Article article = new Article();
            ViewBag.cats = new MultiSelectList(db.Categories.ToList(), "Id", "Name");
            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewArticle(Article article, HttpPostedFileBase file, int[] cats)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in cats)
                {
                    var category = db.Categories.Find(item);
                    article.Categories.Add(category);
                }
                var supportedTypes = new string[] { "jpg", "jpeg", "png" };

                if (file != null)
                {
                    var type = System.IO.Path.GetExtension(file.FileName).ToLower().Replace(".", "");
                    if (supportedTypes.Contains(type) && file != null)
                    {
                        string delimiter = Guid.NewGuid().ToString();
                        string path = @"\UploadedFile\" + delimiter + "_" + file.FileName;
                        file.SaveAs(Server.MapPath("~") + path);
                        article.PhotoPath = path;
                    }
                }
                article.ApplicationUserId = User.Identity.GetUserId();
                article.ArticleTime = DateTime.Now;
                db.Entry(article).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("NewArticle");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            TempData["DeleteArticle"] = "Success";

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            ViewBag.cats = new MultiSelectList(db.Categories.ToList(), "Id", "Name");
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Article article, HttpPostedFileBase file, int[] cats, string Title, string Content, string Summary)
        {
            article = db.Articles.FirstOrDefault(x => x.Id == article.Id);
            if (ModelState.IsValid)
            {
                foreach (var item in cats)
                {
                    var category = db.Categories.Find(item);
                    article.Categories.Add(category);
                }
                var supportedTypes = new string[] { "jpg", "jpeg", "png" };
                if (file != null)
                {
                    var type = System.IO.Path.GetExtension(file.FileName).ToLower().Replace(".", "");
                    if (supportedTypes.Contains(type))
                    {
                        string delimiter = Guid.NewGuid().ToString();
                        string path = @"\UploadedFile\" + delimiter + "_" + file.FileName;
                        file.SaveAs(Server.MapPath("~") + path);
                        article.PhotoPath = path;
                    }
                }
                else
                {
                    article.PhotoPath = db.Articles.FirstOrDefault(x => x.Id == article.Id).PhotoPath;
                }
                article.Summary = Summary;
                article.Title = Title;
                article.Content = Content;
                article.ApplicationUserId = User.Identity.GetUserId();
                article.ArticleTime = DateTime.Now;
                //db.Entry(article).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData["EditArticle"] = "Error";
            return View();
        }
    }
}