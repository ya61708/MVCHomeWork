using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ValidationAttributes
{
    public class Email驗證 : DataTypeAttribute
    {

        public Email驗證() : base(DataType.Text)
        {
            ErrorMessage = "請輸入正確的Email格式";
        }

        public override bool IsValid(Object value)
        {
            var email = (string)value;
            var emailAttribute = new EmailAddressAttribute();

            return emailAttribute.IsValid(email);

        }


    }
}