using Blog.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    public class ReportsController : BaseController
    {
        // GET: Admin/Reports
        public ActionResult Index()
        {
            return View(db.Reports.ToList());
        }

        public ActionResult ReportsPartial()
        {
            return PartialView("_ReportsPartial",db.Reports.ToList());
        }
    }
}