using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MVCProject.Models;
using MVCProject.Services;

namespace MVCProject.Controllers
{
    public class HomeController : Controller
    {
        ApiConnection apiConnection = new ApiConnection();
        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult InstallationPage()
        {
            return View();
        }
        public ActionResult SettingsView()
        {
            return View();
        }
        public ActionResult ProcessAlarmInform(string FirstName, string Email)
        {
            try
            {
                string responseText = apiConnection.SendUSerInfo(Email);
                return Json(responseText, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
