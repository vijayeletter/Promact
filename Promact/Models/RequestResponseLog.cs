using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
namespace Promact.Models
{
    public class RequestResponseLog : ActionFilterAttribute

    {

        //public override void OnActionExecuting(HttpActionContext actionContext)
        //{
        //    if (System.IO.File.Exists("D:\\Promact.Log"))
        //    {
        //        using (StreamWriter sw = File.AppendText("D:\\Promact.Log"))
        //        {                    
        //            sw.WriteLine("-----------Log Details on " + " " + DateTime.Now.ToString() + "-----------------");

        //            sw.WriteLine(actionContext.ControllerContext.ControllerDescriptor.ControllerName);                    
        //            sw.Flush();
        //            sw.Close();
        //        }               

        //    }         

        //    base.OnActionExecuting(actionContext);
        //}
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("OnActionExecuting", filterContext.RouteData);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting", filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExecuted", filterContext.RouteData);
        }


        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0} controller:{1} action:{2}" + Environment.NewLine, methodName, controllerName, actionName);
            //Debug.WriteLine(message, "Action Filter Log");

            System.IO.File.AppendAllText("D:\\Promact.Log", message);
        }


    }

}