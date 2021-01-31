using BlogMVC.Controllers;
using BlogMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class DashboardController : BaseController
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();

            dataPoints.Add(new DataPoint("Software", db.Categories.FirstOrDefault(x => x.Name == "Software").Articles.Count()));
			dataPoints.Add(new DataPoint("Sports", db.Categories.FirstOrDefault(x => x.Name == "Sports").Articles.Count()));
			dataPoints.Add(new DataPoint("Science", db.Categories.FirstOrDefault(x => x.Name == "Science").Articles.Count()));
			dataPoints.Add(new DataPoint("Politics", db.Categories.FirstOrDefault(x => x.Name == "Politics").Articles.Count()));
			dataPoints.Add(new DataPoint("Movie", db.Categories.FirstOrDefault(x => x.Name == "Movie").Articles.Count()));
			dataPoints.Add(new DataPoint("Research", db.Categories.FirstOrDefault(x => x.Name == "Research").Articles.Count()));
			dataPoints.Add(new DataPoint("Lifestyle", db.Categories.FirstOrDefault(x => x.Name == "Lifestyle").Articles.Count()));
 
			ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            ViewBag.active = "Dashboard";
            return View();
        }
    }
}