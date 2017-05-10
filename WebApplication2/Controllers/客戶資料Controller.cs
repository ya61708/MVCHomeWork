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
    public class 客戶資料Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: 客戶資料
        public ActionResult Index(String searchString)
        {
            var all= db.客戶資料.AsQueryable();
           
            if (!String.IsNullOrEmpty(searchString)){
                var data = all.Where(p => p.Is刪除 == false && p.客戶名稱.Contains(searchString));
                return View(data);
            }
            else {
                var data = all.Where(p => p.Is刪除 == false);
                return View(data);
            }
        }

        public ActionResult Details(int id)
        {

            var 客戶資料 = db.Database.SqlQuery<客戶資料>("SELECT * FROM dbo.客戶資料 WHERE Id=@p0", id).FirstOrDefault();

            return View(客戶資料);
        }

 
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        public ActionResult Create(客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.客戶資料.Add(客戶資料);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

   
        public ActionResult Edit(int id)
        {
         
            var 客戶資料 = db.客戶資料.Find(id);
            return View(客戶資料);
        }

       
        [HttpPost]
        public ActionResult Edit(int id, 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                var item = db.客戶資料.Find(id);
                item.客戶名稱 = 客戶資料.客戶名稱;
                item.傳真 = 客戶資料.傳真;
                item.地址 = 客戶資料.地址;
                item.客戶聯絡人 = 客戶資料.客戶聯絡人;
                item.客戶銀行資訊 = 客戶資料.客戶銀行資訊;
                item.統一編號 = 客戶資料.統一編號;
                item.電話 = 客戶資料.電話;
                item.Email = 客戶資料.Email;
                db.SaveChanges();


                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        public ActionResult Delete(int id)
        {
            var 客戶資料 = db.客戶資料.Find(id);

            客戶資料.Is刪除 = true;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
