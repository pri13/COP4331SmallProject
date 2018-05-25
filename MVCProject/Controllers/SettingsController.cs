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
    public class SettingsController : Controller
    {
        ApiConnection apiConnection = new ApiConnection();
        // GET: Settings
        public ActionResult SettingsView()
        {
            return View();
        }
        public ActionResult EnableAlarm(string value)
        {
            try
            {
                var responseText = apiConnection.EnableAlarm(value);
                return Json(responseText, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
          
        }
        public ActionResult NightLightAlarm(string value)
        {
            try
            {
                var responseText = apiConnection.NightLightAlarm(value);
                return Json(responseText, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
          
        }
        public ActionResult MotionDection(string value)
        {
            try
            {
                var responseText = apiConnection.MotionDection(value);
                return Json(responseText, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
           
        }
        public ActionResult ProcessAlarm(string startTime, string endTime)
        {
            try
            {
                var responseText = apiConnection.Automation(startTime, endTime);
                return Json(responseText, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
           
        }

    }
}
