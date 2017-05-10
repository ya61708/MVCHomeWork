namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using ValidationAttributes;

    [MetadataType(typeof(客戶聯絡人MetaData))]



    public partial class 客戶聯絡人 :IValidatableObject
    {
        客戶資料Entities db = new 客戶資料Entities();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            var all = db.客戶聯絡人.AsQueryable();
            var data = all.Where(p => p.客戶Id == 客戶Id);
                
            foreach (var item in data)
            {
                if (this.Email.Equals(item.Email) && item.Is刪除 == false && this.Id != item.Id) {
                    yield return new ValidationResult("信箱已有重複", new[] { "Email"});
               }
            }

            yield break;
        }

    }
    
    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Email驗證]
        [Required]
        public string Email { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [手機驗證]
        public string 手機 { get; set; }

        [Required]
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
