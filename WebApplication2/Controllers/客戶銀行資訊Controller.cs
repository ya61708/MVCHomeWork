using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class 客戶銀行資訊Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        public ActionResult Index(String searchString)
        {
            var all = db.客戶銀行資訊.AsQueryable();


            if (!String.IsNullOrEmpty(searchString))
            {
                var 客戶銀行資訊 = all.Include(客 => 客.客戶資料).Where(p => p.Is刪除 == false && p.銀行名稱.Contains(searchString));
                return View(客戶銀行資訊);
            }
            else
            {
                var 客戶銀行資訊 = all.Include(客 => 客.客戶資料).Where(p => p.Is刪除 == false);
                return View(客戶銀行資訊);
            }
        }

        public ActionResult Details(int id)
        {
            客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            return View(客戶銀行資訊);
        }

        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");

            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                db.客戶銀行資訊.Add(客戶銀行資訊);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");

            return View(客戶銀行資訊);
        }
        
        public ActionResult Edit(int id)
        {
            
            var item = db.客戶銀行資訊.Find(id);

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");


            return View(item);
        }

       
        [HttpPost]
        public ActionResult Edit(int id,客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                var item = db.客戶銀行資訊.Find(id);
                item.分行代碼 = 客戶銀行資訊.分行代碼;
                item.客戶Id = 客戶銀行資訊.客戶Id;
                item.客戶資料 = 客戶銀行資訊.客戶資料;
                item.帳戶名稱 = 客戶銀行資訊.帳戶名稱;
                item.帳戶號碼 = 客戶銀行資訊.帳戶號碼;
                item.銀行代碼 = 客戶銀行資訊.銀行代碼;
                item.銀行名稱 = 客戶銀行資訊.銀行名稱;


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");

            return View(客戶銀行資訊);
        }

        public ActionResult Delete(int id)
        {
            var 客戶銀行資訊 = db.客戶銀行資訊.Find(id);

            客戶銀行資訊.Is刪除 = true;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
