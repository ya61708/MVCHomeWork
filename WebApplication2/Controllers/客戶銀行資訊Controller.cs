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
        客戶銀行資訊Repository repo = RepositoryHelper.Get客戶銀行資訊Repository();
        客戶資料Repository repo1 = RepositoryHelper.Get客戶資料Repository();


        public ActionResult Index(String searchString)
        {
            return View(repo.Get全部客戶銀行資料包含搜尋(searchString));
        }

        public ActionResult Details(int? id)
        {
            return View(repo.Get單筆客戶銀行資料ById(id.Value));
        }

        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repo1.Get全部客戶資料包含搜尋("",""), "Id", "客戶名稱");

            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                repo.新增客戶銀行資訊資料(客戶銀行資訊);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repo1.Get全部客戶資料包含搜尋("",""), "Id", "客戶名稱");

            return View(客戶銀行資訊);
        }
        
        public ActionResult Edit(int? id)
        {

            var item = repo.Get單筆客戶銀行資料ById(id.Value);

            ViewBag.客戶Id = new SelectList(repo1.Get全部客戶資料包含搜尋("",""), "Id", "客戶名稱");


            return View(item);
        }

       
        [HttpPost]
        public ActionResult Edit(int id,客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                var item = repo.Get單筆客戶銀行資料ById(id);

                if (TryUpdateModel<客戶銀行資訊>(item))
                {
                    repo.UnitOfWork.Commit();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.客戶Id = new SelectList(repo1.Get全部客戶資料包含搜尋("",""), "Id", "客戶名稱");

            return View(客戶銀行資訊);
        }

        public ActionResult Delete(int id)
        {
            var 客戶銀行資訊 = repo.Get單筆客戶銀行資料ById(id);
            repo.Delete(客戶銀行資訊);
            repo.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult ExportData()
        {
            return File(repo.Get匯出Excel檔(), "application/vnd.ms-excel", "客戶銀行資訊.xls");
        }

    }
}
