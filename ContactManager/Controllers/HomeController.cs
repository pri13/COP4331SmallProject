using ContactManager.Models;
using ContactManager.Repository;
using ContactManager.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace ContactManager.Controllers
{
    public class HomeController : Controller
    {
        COP4331Entities6 context = new COP4331Entities6();
        List<Models.ContactViewModel> result = new List<Models.ContactViewModel>();
        public ActionResult Index()
        {

            return View();

        }
        public ActionResult ContactsManager(string id)
        {
            ShowContactList(result,id);
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

        public ActionResult NewUser(string jsonString)
        {

            NewUserTest userObject = Newtonsoft.Json.JsonConvert.DeserializeObject<NewUserTest>(jsonString);

            User newUser = new User {
                FirstName = userObject.FirstName,
                LastName = userObject.LastName,
                Login = userObject.UserName,
                Password = userObject.Password,
                DateCreated = DateTime.Now,
                DateLastLoggedIn = DateTime.Now
            };

            context.Users.Add(newUser);
            context.SaveChanges();




            return null;
        }


        void SendResultInfoAsJson(LoginResponse res)
        {
            string strJson = JsonConvert.SerializeObject(res);
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(strJson);
            Response.End();
        }
        public PartialViewResult ShowContact( List<Models.ContactViewModel> result)
        {
            try
            {
               // ShowContactList(result, id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occured {0}", ex.Message);
            }

            return PartialView(result.OrderByDescending(x => x.FirstName));
        }
        public void ShowContactList(List<Models.ContactViewModel> result, string id)
        {
            int contactID = Convert.ToInt32(id);
            var items = context.People.Where(x=>x.UserID == contactID).ToList();

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
        

        public ActionResult CreateNewContact(string newContact, string id)
        {
            try
            {
                    ContactViewModel contactObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ContactViewModel>(newContact);
                    if(contactObject != null && id !=null)
                    {
                            Person person = new Person();
                            person.FirstName = contactObject.FirstName;
                            person.LastName = contactObject.LastName;
                            person.Birthdate = contactObject.BirthDate;
                            person.CellPhone = contactObject.CellPhone;
                            person.FAX = contactObject.FaxNumber;
                            person.StreetAddress = contactObject.Address;
                            person.City = contactObject.City;
                            person.State = contactObject.State;
                            person.Country = contactObject.Country;
                            person.WorkPhone = contactObject.WorkPhone;
                            person.Email = contactObject.Email;
                            person.DateCreated = DateTime.Now;
                            person.UserID = Convert.ToInt32(id);
                            context.People.Add(person);
                            context.SaveChanges();
                            ShowContactList(result, id);
                            result.OrderByDescending(x => x.FirstName);
                            return PartialView("ShowContact", result);
                    }
                    else
                    {
                             return PartialView("Error");
                    }         
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occureed {0}", ex.Message);
                return PartialView("Error");
            }

        }

        public ActionResult EditContact(string editContactJson, string userId)
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
                    person.Email = contactObject.Email;
                    person.WorkPhone = contactObject.WorkPhone;
                    person.CellPhone = contactObject.CellPhone;
                    person.StreetAddress = contactObject.Address;
                    person.City = contactObject.City;
                    person.State = contactObject.State;


                    break;
                }
            }

            
            context.SaveChanges();

            ShowContactList(result, userId);
            result.OrderByDescending(x => x.FirstName);
            return PartialView("ShowContact", result);



        }

        public ActionResult DeleteContact(string contactID, string userId)
        {

            ContactID contactObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ContactID>(contactID);
            int selectedId = contactObject.contactID;
          

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

            ShowContactList(result, userId);
            result.OrderByDescending(x => x.FirstName);
            return PartialView("ShowContact", result);

        }


    }
}
