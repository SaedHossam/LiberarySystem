using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
//using System.Web.Mvc;

namespace LiberarySystem.Attributes
{
    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
        }

        //public override void OnActionExecuting(ActionExecutingContext actionContext)
        //{
        //    
        //}
    }
}