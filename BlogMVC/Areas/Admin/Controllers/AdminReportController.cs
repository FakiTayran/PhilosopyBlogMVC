using BlogMVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMVC.Areas.Admin.Controllers
{
    public class AdminReportController : BaseController
    {
        // GET: Admin/AdminReport
        public ActionResult Index()
        {
            ViewBag.active = "Reports";
            return View(db.Reports.ToList());
        }

        public ActionResult NotificationPartial() 
        {
            return PartialView("_NotificationPartial", db.Reports.ToList());
        }
    }
}