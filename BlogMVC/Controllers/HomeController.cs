using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMVC.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(db.Articles.OrderByDescending(x => x.ArticleTime).ToList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Active = "About";
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Active = "Contact";
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PopularPostPartial()
        {
            return PartialView("_PopularPostPartial", db.Articles.ToList());
        }
    }
}