using MVCProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class RemoteUtilityController : Controller
    {
        // GET: LED
        ApiConnection apiconnection = new ApiConnection();
        // GET: RemoteUtility
        public ActionResult RemoteUtility()
        {
            return View();
        }
        public JsonResult MotorUp(string value)
        {
            try
            {
                var responseText = apiconnection.MotorUp(value);
                return Json(responseText, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
            
        }
        public ActionResult MotorDown(string value)
        {
            try
            {
                var responseText = apiconnection.MotorDown(value);
                return Json(responseText, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                throw new Exception();
               
            }
            
        }
    }
}