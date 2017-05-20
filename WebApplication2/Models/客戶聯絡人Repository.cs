using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using ClosedXML.Excel;

namespace WebApplication2.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public IQueryable<客戶聯絡人> Get全部客戶聯絡人資料()
        {
            var data = this.All();

            return data;

        }


        public IQueryable<客戶聯絡人> Get全部客戶聯絡人資料包含搜尋(String searchString,String 職稱)
        {
            var data = this.All().Where(p => p.Is刪除 == false);

            if (!String.IsNullOrEmpty(searchString))
                data = data.Where(p => p.姓名.Contains(searchString));

            if (!String.IsNullOrEmpty(職稱))
                data = data.Where(p => p.職稱 == 職稱);


            return data;

        }

        public IQueryable<客戶聯絡人> Get單個客戶所有聯絡人(int id) {

            return this.All().Where(p => p.Is刪除 == false && p.客戶Id == id);
        }

        public 客戶聯絡人 Get單筆客戶聯絡人資料ById(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public void 新增客戶聯絡人資料(客戶聯絡人 客戶聯絡人)
        {
            this.Add(客戶聯絡人);
        }

        public override void Delete(客戶聯絡人 entity)
        {
            entity.Is刪除 = true;
        }

        public List<SelectListItem> Get職位分類選單()
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            var 客戶資料 = this.Get全部客戶聯絡人資料包含搜尋("","");
            var selectItem = 客戶資料.GroupBy(p => p.職稱);

            selectList.Add(new SelectListItem
            {
                Text = "",
                Value = ""
            });

            foreach (var item in selectItem)
            {
                selectList.Add(new SelectListItem
                {
                    Text = item.Key,Value =item.Key

                });

            }


            return selectList;
        }

        public MemoryStream Get匯出Excel檔()
        {
            List<客戶聯絡人> data = this.Get全部客戶聯絡人資料().ToList();
            List<String> title = new List<string>();

            title.Add("Id");
            title.Add("客戶Id");
            title.Add("職稱");
            title.Add("姓名");
            title.Add("Email");
            title.Add("手機");
            title.Add("電話");
            title.Add("Is刪除");


            List<string> header = new List<string>();

            MemoryStream ms = new MemoryStream();

            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Sheet_1");

            for (int i = 0; i < title.Count(); i++)
            {
                ws.Cell(1, i + 1).SetValue(title.ToArray()[i]);
                ws.ColumnWidth = 20;
            }

            for (int i = 0; i < data.Count; i++)
            {
                ws.Cell(i + 2, 1).SetValue(data.ToArray()[i].Id);
                ws.Cell(i + 2, 2).SetValue(data.ToArray()[i].客戶Id);
                ws.Cell(i + 2, 3).SetValue(data.ToArray()[i].職稱);
                ws.Cell(i + 2, 4).SetValue(data.ToArray()[i].姓名);
                ws.Cell(i + 2, 5).SetValue(data.ToArray()[i].Email);
                ws.Cell(i + 2, 6).SetValue(data.ToArray()[i].手機);
                ws.Cell(i + 2, 7).SetValue(data.ToArray()[i].電話);
                ws.Cell(i + 2, 8).SetValue(data.ToArray()[i].Is刪除);

            }


            workbook.SaveAs(ms);
            ms.Position = 0;
            ms.Flush();
            return ms;

        }

    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}