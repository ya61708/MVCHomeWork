using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;

namespace WebApplication2.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public IQueryable<客戶銀行資訊> Get全部客戶銀行資料()
        {
         return this.All();
        
        }

        public IQueryable<客戶銀行資訊> Get全部客戶銀行資料包含搜尋(String searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return this.All().Where(p => p.Is刪除 == false);
            }
            else
            {
                return this.All().Where(p => p.Is刪除 == false && p.銀行名稱.Contains(searchString));
            }

        }

        public 客戶銀行資訊 Get單筆客戶銀行資料ById(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public void 新增客戶銀行資訊資料(客戶銀行資訊 客戶銀行資訊)
        {
            this.Add(客戶銀行資訊);
        }

        public override void Delete(客戶銀行資訊 entity)
        {
            entity.Is刪除 = true;
        }


        public MemoryStream Get匯出Excel檔()
        {
            List<客戶銀行資訊> data = this.Get全部客戶銀行資料().ToList();
            List<String> title = new List<string>();

            title.Add("Id");
            title.Add("客戶Id");
            title.Add("銀行名稱");
            title.Add("銀行代碼");
            title.Add("分行代碼");
            title.Add("帳戶名稱");
            title.Add("帳戶號碼");
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
                ws.Cell(i + 2, 3).SetValue(data.ToArray()[i].銀行名稱);
                ws.Cell(i + 2, 4).SetValue(data.ToArray()[i].銀行代碼);
                ws.Cell(i + 2, 5).SetValue(data.ToArray()[i].分行代碼);
                ws.Cell(i + 2, 6).SetValue(data.ToArray()[i].帳戶名稱);
                ws.Cell(i + 2, 7).SetValue(data.ToArray()[i].帳戶號碼);
                ws.Cell(i + 2, 8).SetValue(data.ToArray()[i].Is刪除);
                
            }


            workbook.SaveAs(ms);
            ms.Position = 0;
            ms.Flush();
            return ms;

        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}