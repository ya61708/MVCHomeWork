using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.ViewModel;

namespace WebApplication2.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
        客戶聯絡人Repository repo = RepositoryHelper.Get客戶聯絡人Repository();
        客戶資料Repository repo1 = RepositoryHelper.Get客戶資料Repository();

        public ActionResult Index(String searchString,String 職稱篩選)
        {
            ViewBag.職稱篩選 = repo.Get職位分類選單();

            return View(repo.Get全部客戶聯絡人資料包含搜尋(searchString, 職稱篩選));
        }

        public ActionResult OnlyDetailView(int? id)
        {
            Get客戶聯絡人(id.Value);
            return View();
        }

        [HttpPost]
        public ActionResult OnlyDetailView(int id,客戶聯絡人VM[] items)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    var data = repo.Get單筆客戶聯絡人資料ById(item.Id);
                    data.職稱 = item.職稱;
                    data.手機 = item.手機;
                    data.電話 = item.電話;
                }
                repo.UnitOfWork.Commit();

            }

            Get客戶聯絡人(id);

            return View();
        }

        private void Get客戶聯絡人(int id)
        {
            var 客戶聯絡人 = repo.Get單個客戶所有聯絡人(id);
            ViewData.Model = 客戶聯絡人;
        }

        public ActionResult Details(int? id)
        {
            return View(repo.Get單筆客戶聯絡人資料ById(id.Value));
        }

       
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repo1.Get全部客戶資料包含搜尋("",""), "Id", "客戶名稱");

            return View();
        }

      
        [HttpPost]
        public ActionResult Create(客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                repo.新增客戶聯絡人資料(客戶聯絡人);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repo1.Get全部客戶資料包含搜尋("",""), "Id", "客戶名稱", 客戶聯絡人.客戶Id);

            return View(客戶聯絡人);
        }


        public ActionResult Edit(int? id)
        {
            var 客戶聯絡人 = repo.Get單筆客戶聯絡人資料ById(id.Value);

            ViewBag.客戶Id = new SelectList(repo1.Get全部客戶資料包含搜尋("",""), "Id", "客戶名稱", 客戶聯絡人.客戶Id);

            return View(客戶聯絡人);
        }

   
        [HttpPost]
        public ActionResult Edit(int id, 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                var item = repo.Get單筆客戶聯絡人資料ById(id);

                if (TryUpdateModel<客戶聯絡人>(item))
                {
                    repo.UnitOfWork.Commit();
                    return RedirectToAction("Index");
                }
            }
        
            ViewBag.客戶Id = new SelectList(repo1.Get全部客戶資料包含搜尋("",""), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        public ActionResult Delete(int id)
        {
            var 客戶聯絡人 = repo.Get單筆客戶聯絡人資料ById(id);
            repo.Delete(客戶聯絡人);
            repo.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult ExportData()
        {
            return File(repo.Get匯出Excel檔(), "application/vnd.ms-excel", "客戶聯絡人.xls");
        }
    }
}
