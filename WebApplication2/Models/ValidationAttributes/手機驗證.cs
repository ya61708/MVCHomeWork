using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApplication2.Models.ValidationAttributes
{
    public class 手機驗證 : DataTypeAttribute
    {
        public 手機驗證() : base(DataType.PhoneNumber)
        {
           ErrorMessage = "請輸入正確的手機格式";
        }

        public override bool IsValid(Object value)
        {
            if (value == null) {
                return true;
            }

            var phone = (string)value;
            return Regex.IsMatch(phone, "[0-9]{4}-[0-9]{6}");

        }

    }
}