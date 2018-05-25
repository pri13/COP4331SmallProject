using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace MVCProject.Services
{
    public class ApiConnection
    {
        public string GetBrightnessLevel(string value)
        {
            var endpoint = "ledBrightness"; 
            return ProcessRequest(endpoint, value);
        }
        public string SendUSerInfo(string value)
        {
            var endpoint = "setInfo";
            var chrs = new[] { '@', ':',  ';', '\'' };
            var stringarry = value.Split('@');
            return ProcessRequest(endpoint, stringarry[0], stringarry[1]);
        }
        public string Automation(string WakeTime  ,string BedTime)
        {
           
            var chrs = new[] { '@', ':', '.', ';', '\'' };
            var wakeTime = string.Concat(WakeTime.Where(c => !chrs.Contains(c)));
            var bedTime = string.Concat(BedTime.Where(c => !chrs.Contains(c)));
            var endpoint = "alarmClock";
            return ProcessRequest(endpoint, wakeTime, bedTime);
        }
        public string SendONOFFSignal(string value)
        {
            var endpoint = "ledStatus";
            return ProcessRequest(endpoint, value);
        }
        public string SendColor(string value)
        {
           var endpoint =  "ledColor";
            return ProcessRequest(endpoint, value);
        }
        public string GetBattery()
        {
            var endpoint = "getBattery";
            return ProcessRequest(endpoint , "");
        }
        public string MotorUp(string value)
        {
            var endpoint = "motorUp";
            return ProcessRequest(endpoint, value);
        }
        public string MotorDown(string value)
        {
            var endpoint = "motorDown";
            return ProcessRequest(endpoint, value);
        }
        public string EnableAlarm(string value)
        {
            var endpoint = "alarmEnable";
            return ProcessRequest(endpoint, value);
        }
        public string NightLightAlarm(string value)
        {
            var endpoint = "nightLightEnable";
            return ProcessRequest(endpoint, value);
        }
        public string MotionDection(string value)
        {
            var endpoint = "motorMotionEnable";
            return ProcessRequest(endpoint, value);
        }
        public string ProcessAlarm(string value)
        {
            var endpoint = "nightLightEnable";
            return ProcessRequest(endpoint, value);
        }
        public string ProcessRequest(string endpoint , string value)
        {
            string responseText = string.Empty;
            var baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            var path = ConfigurationManager.AppSettings["path"];
            try
            {
                string QueryTag = "value=";
                UriBuilder builder = new UriBuilder(baseUrl);
                builder.Query = path + endpoint+ "?" + QueryTag + value;
                if (string.IsNullOrEmpty(value))
                {
                    builder.Query = path + endpoint;
                }
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(builder.Uri.ToString());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
                HttpResponseMessage response = client.GetAsync(builder.Uri).Result;
                if (response.StatusCode.ToString().Equals("OK"))
                {
                    if (endpoint.Contains("getBattery"))
                    {
                        System.Threading.Thread.Sleep(4000);
                        var batteryValue = GetBatteryResponse();
                        if (batteryValue != null)
                        {
                            responseText = batteryValue;
                        }
                    }
                    else
                    {
                        responseText = "Success";
                    }
                    
                }
                else
                {
                    responseText = "Error Occured Please Try Again";
                }
            }
            catch(Exception ex)
            {
                responseText = "Error Occured Please Try Again";
                throw new Exception("Error occured"+ ex.Message);
            }      
            return responseText;
        }
        public string ProcessRequest(string endpoint, string BedTime, string WakeTime)
        {
            string responseText = string.Empty;
            var baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            var path = ConfigurationManager.AppSettings["path"];
            try
            {
                string QueryTag = "WakeTime=";
                UriBuilder builder = new UriBuilder(baseUrl);
                builder.Query = path + endpoint + "?" + QueryTag + BedTime + "%26" + QueryTag + WakeTime;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(builder.Uri.ToString());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
                HttpResponseMessage response = client.GetAsync(builder.Uri).Result;
                if (response.StatusCode.ToString().Equals("OK"))
                {
                    if (endpoint.Contains("alarmClock"))
                    {
                        responseText = "Success";
                    }
                }
                else
                {
                    responseText = "Error Occured Please Try Again";
                }
            }
            catch (Exception ex)
            {
                responseText = "Error Occured Please Try Again";
                throw new Exception("Error occured" + ex.Message);
            }
            return responseText;
        }
        public string GetBatteryResponse()
        {
            var endpoint = ConfigurationManager.AppSettings["firebaseEndpoint"];
            UriBuilder builder = new UriBuilder(endpoint);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(builder.Uri.ToString());
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            HttpResponseMessage response = client.GetAsync(builder.Uri).Result;
            return response.Content.ReadAsStringAsync().Result;

        }

    }
}