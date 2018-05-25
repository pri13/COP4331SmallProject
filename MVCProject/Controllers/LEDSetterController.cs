
using MVCProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{

    public class LEDSetterController : Controller
    {
        ApiConnection apiconnection = new ApiConnection();
        public ActionResult LEDView()
        {
            return View();
        }
        public ActionResult SendColor(string color)
        {
            try
            {
                var repsonse = apiconnection.SendColor(color);
                return Json(repsonse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
          
        }
    
        
        public ActionResult GetBrightnessLevel(string level)
        {
            try
            {
                var repsonse = apiconnection.GetBrightnessLevel(level);
                return Json(repsonse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
           
         }

        public ActionResult SendONOFFSignal(string status)
        {
            try
            {
                var repsonse = apiconnection.SendONOFFSignal(status);
                return Json(repsonse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
          
        }
    }
}
