using ClosedXML.Excel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication2.ActionFilter;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class 客戶資料Controller : Controller
    {
        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();
    
        public ActionResult Index(String searchString,String 客戶分類)
        {

           ViewBag.客戶分類 = repo.Get客戶分類選單();

           return View(repo.Get全部客戶資料包含搜尋(searchString,客戶分類));
        }


        public ActionResult Details(int id)
        {
            return View(repo.Get單筆客戶資料ById(id));
        }

 
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(DbEntityValidationException), View = "Error_DbEntityValidationException")]
        public ActionResult Create(客戶資料 客戶資料)
        {
            //if (ModelState.IsValid)
            {
                repo.新增客戶資料(客戶資料);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Get單筆客戶資料ById(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

       
        [HttpPost]
        public ActionResult Edit(int id, 客戶資料 客戶資料)
        {
             var data = repo.Get單筆客戶資料ById(id);

            if (TryUpdateModel<客戶資料>(data))
            {
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        public ActionResult Delete(int? id)
        {
            var 客戶資料 = repo.Get單筆客戶資料ById(id.Value);

            repo.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
            repo.Delete(客戶資料);
            repo.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult ExportData()
        {
            return File(repo.Get匯出Excel檔(), "application/vnd.ms-excel", "客戶資料.xls");
        }

      

    }
}
