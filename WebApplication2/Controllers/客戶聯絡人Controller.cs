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
    public class 客戶聯絡人Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        public ActionResult Index(String searchString)
        {
            var all = db.客戶聯絡人.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                var 客戶聯絡人 = all.Include(客 => 客.客戶資料).Where(p => p.Is刪除 == false && p.姓名.Contains(searchString));
                return View(客戶聯絡人);
            }
            else
            {
                var 客戶聯絡人 = all.Include(客 => 客.客戶資料).Where(p => p.Is刪除 == false);
                return View(客戶聯絡人);
            }
          
        }

        public ActionResult Details(int id)
        {
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);

            return View(客戶聯絡人);
        }

       
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");

            return View();
        }

      
        [HttpPost]
        public ActionResult Create(客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                db.客戶聯絡人.Add(客戶聯絡人);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);

            return View(客戶聯絡人);
        }


        public ActionResult Edit(int? id)
        {
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);

            return View(客戶聯絡人);
        }

   
        [HttpPost]
        public ActionResult Edit(int id, 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                var item = db.客戶聯絡人.Find(id);
                item.Email = 客戶聯絡人.Email;
                item.姓名 = 客戶聯絡人.姓名;
                item.客戶Id = 客戶聯絡人.客戶Id;
                item.客戶資料 = 客戶聯絡人.客戶資料;
                item.手機 = 客戶聯絡人.手機;
                item.職稱 = 客戶聯絡人.職稱;
                item.電話 = 客戶聯絡人.電話;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        public ActionResult Delete(int id)
        {
            var 客戶聯絡人 = db.客戶聯絡人.Find(id);

            客戶聯絡人.Is刪除 = true;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
