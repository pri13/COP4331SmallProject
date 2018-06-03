
using ContactManager.Models;
using ContactManager.Repository;
using ContactManager.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace ContactManager.Controllers
{
    public class HomeController : Controller
    {
        COP4331Entities context = new COP4331Entities();
        public ActionResult Index()
        {

            return View();

        }
        public ActionResult ContactsManager()
        {
            var result = new List<Models.ContactViewModel>();
            GetContact(result);
            return View(result.OrderByDescending(x => x.FirstName));

        }
        [HttpPost]
        public ActionResult LogOff()
        {
            return View();
        }
        public ActionResult Error()
        {

            return View();

        }
        public void Login(string username, string password)
        {
            Login login = new Login();
            var response = login.GetAccess(username, password);
            SendResultInfoAsJson(response);

        }
        void SendResultInfoAsJson(LoginResponse res)
        {
            string strJson = JsonConvert.SerializeObject(res);
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(strJson);
            Response.End();
        }
        public PartialViewResult ShowContact()
        {
            var result = new List<Models.ContactViewModel>();
            try
            {
                GetContact(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occured {0}", ex.Message);
            }

            return PartialView(result.OrderByDescending(x => x.FirstName));
        }
        public void GetContact(List<Models.ContactViewModel> result)
        {

            var items = context.People.ToList();

            foreach (var i in items)
            {
                var ba = new Models.ContactViewModel
                {
                    ID = i.ID,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Address = i.StreetAddress,
                    City = i.City,
                    State = i.State,
                    Country = i.Country,
                    CellPhone = i.CellPhone,
                    Email = i.Email,
                };
                if (ba != null)
                {
                    result.Add(ba);
                }
            }
        }

        public ActionResult CreateNewContact(ContactViewModel model)
        {
            try
            {
                Person person = new Person();
                person.FirstName = model.FirstName;
                person.LastName = model.LastName;
                person.Birthdate = model.BirthDate;
                person.CellPhone = model.CellPhone;
                person.StreetAddress = model.Address;
                person.City = model.City;
                person.State = model.State;
                person.Country = model.Country;
                person.WorkPhone = model.WorkPhone;
                person.Email = model.Email;
                person.DateCreated = DateTime.Now;
                context.People.Add(person);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occureed {0}", ex.Message);
            }
            return View("Index");

        }

        public ActionResult EditContact(string editContactJson)
        {
            EditContactTest contactObject = Newtonsoft.Json.JsonConvert.DeserializeObject<EditContactTest>(editContactJson);
            int selectedId = contactObject.contactID;
            var result = new List<Models.ContactViewModel>();

            Person contact = null;
            foreach (var person in context.People)
            {
                if (person.ID == selectedId)
                {
                    contact = person;

                    person.FirstName = contactObject.FirstName;
                    person.LastName = contactObject.LastName;
                    person.WorkPhone = contactObject.WorkPhone;
                    person.CellPhone = contactObject.CellPhone;
                    person.StreetAddress = contactObject.Address;
                    person.City = contactObject.City;
                    person.State = contactObject.State;


                    break;
                }
            }


            context.SaveChanges();

            GetContact(result);
            result.OrderByDescending(x => x.FirstName);
            return PartialView("ShowContact", result);

        }

        public ActionResult DeleteContact(string contactID)
        {

            ContactID contactObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ContactID>(contactID);
            int selectedId = contactObject.contactID;
            var result = new List<Models.ContactViewModel>();

            Person contact = null;
            foreach (var person in context.People)
            {
                if (person.ID == selectedId)
                {
                    contact = person;
                    break;
                }
            }


            context.People.Remove(contact);
            context.SaveChanges();

            GetContact(result);
            result.OrderByDescending(x => x.FirstName);


            return PartialView("ShowContact", result);

        }


    }
}
