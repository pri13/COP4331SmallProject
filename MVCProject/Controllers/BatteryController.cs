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
    public class BatteryController : Controller
    {
        ApiConnection apiConnection = new ApiConnection();
        // GET: Battery
        public ActionResult BatteryView()
        {
            return View();
        }
        public JsonResult GetBattery()
        {
            var response = apiConnection.GetBattery();
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}