using BlogMVC.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMVC.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Category
        [Route("Category/{categoryName}")]
        public ActionResult Index(string categoryName,int? page)
        {
            ViewBag.Active = "Category";
            if (categoryName == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.FirstOrDefault(x => x.Name == categoryName);
            ViewBag.categoryName = category.Name;
            ViewBag.categoryDescription = category.Description;
            if (category == null)
            {
                return HttpNotFound();
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(category.Articles.OrderByDescending(x => x.ArticleTime).ToList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CategoryNavPartial()
        {
            return PartialView("_CategoryNavPartial",db.Categories.ToList());
        }


    }
}