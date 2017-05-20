using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.ActionFilter
{
    public class ActionDurationAttribute : ActionFilterAttribute
    {
        DateTime startDateTime;
        DateTime endDateTime;

        public string MyProperty { get; set; }

       

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.startDateTime = DateTime.Now;
            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            this.endDateTime = DateTime.Now;

            int duration = (endDateTime.Minute - startDateTime.Minute) * 60 * 1000 + (endDateTime.Second - startDateTime.Second) * 1000 + (endDateTime.Millisecond - startDateTime.Millisecond);


            Debug.WriteLine(">>>>"+ duration+ "毫秒");

            base.OnResultExecuted(filterContext);
        }
    }
}