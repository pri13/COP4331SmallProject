
using ContactManager.Models;
using ContactManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ContactManager.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
           
            return View();
           
        }
        public PartialViewResult ShowContact()
        {
            var result = new List<Models.ContactViewModel>();
            GetContact(result);
            return PartialView(result.OrderByDescending(x=>x.FirstName));
        }
        public void GetContact(List<Models.ContactViewModel> result)
        {
            COP4331Entities1 context = new COP4331Entities1();
            var items = context.People.ToList();
            
            foreach(var i in items)
            {
                var ba = new Models.ContactViewModel
                {
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Address = i.StreetAddress,
                    City = i.City,
                    State= i.State,
                    Country = i.Country,
                    WorkPhone = i.CellPhone,
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
            COP4331Entities1 context = new COP4331Entities1();
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
            return Json("Ok", JsonRequestBehavior.AllowGet);
        }


    }
}
