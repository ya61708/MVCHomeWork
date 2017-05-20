using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using ClosedXML.Excel;

namespace WebApplication2.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{

        public IQueryable<客戶資料> Get全部客戶資料()
        {

            var data = this.All();
            return data;

        }


        public IQueryable<客戶資料> Get全部客戶資料包含搜尋(String searchString, String 客戶分類)
        {

            var data = this.All().Where(p => p.Is刪除 == false);

            if (!String.IsNullOrEmpty(searchString))
                data =   data.Where(p => p.客戶名稱.Contains(searchString));

            if (!String.IsNullOrEmpty(客戶分類))
                data = data.Where(p => p.客戶分類 == 客戶分類);


            return data;
           
        }

        public 客戶資料 Get單筆客戶資料ById(int id) {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public void 新增客戶資料(客戶資料 客戶資料)
        {
            this.Add(客戶資料);
        }

        public override void Delete(客戶資料 entity)
        {
            entity.Is刪除 = true;
        }

        public List<SelectListItem> Get客戶分類選單()
        {
            List<SelectListItem> selectList = new List<SelectListItem>();

            var 客戶資料 = this.Get全部客戶資料包含搜尋("", "");
            var selectItem = 客戶資料.GroupBy(p => p.客戶分類);

            selectList.Add(new SelectListItem
            {
                Text = "",
                Value = ""
            });

            foreach (var item in selectItem)
            {
                selectList.Add(new SelectListItem
                {
                    Text = item.Key,
                    Value = item.Key
                });
            }

            return selectList;
        }

        public MemoryStream Get匯出Excel檔()
        {
            List<客戶資料> data = this.Get全部客戶資料().ToList();
            List<String> title = new List<string>();

            title.Add("Id");
            title.Add("客戶名稱");
            title.Add("統一編號");
            title.Add("電話");
            title.Add("傳真");
            title.Add("地址");
            title.Add("Email");
            title.Add("Is刪除");
            title.Add("客戶分類");


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
                ws.Cell(i + 2, 2).SetValue(data.ToArray()[i].客戶名稱);
                ws.Cell(i + 2, 3).SetValue(data.ToArray()[i].統一編號);
                ws.Cell(i + 2, 4).SetValue(data.ToArray()[i].電話);
                ws.Cell(i + 2, 5).SetValue(data.ToArray()[i].傳真);
                ws.Cell(i + 2, 6).SetValue(data.ToArray()[i].地址);
                ws.Cell(i + 2, 7).SetValue(data.ToArray()[i].Email);
                ws.Cell(i + 2, 8).SetValue(data.ToArray()[i].Is刪除);
                ws.Cell(i + 2, 9).SetValue(data.ToArray()[i].客戶分類);
            }


            workbook.SaveAs(ms);
            ms.Position = 0;
            ms.Flush();
            return ms;

        }

    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}