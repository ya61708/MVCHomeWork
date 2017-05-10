using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class 數量Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        public ActionResult Index()
        {
            var all = db.數量View.AsQueryable();

            return View(all);
        }
    }
}