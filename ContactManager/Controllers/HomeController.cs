
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
        COP4331Entities1 context = new COP4331Entities1();
        public ActionResult Index()
        {
           
            return View();
           
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
           
            return PartialView(result.OrderByDescending(x=>x.FirstName));
        }
        public void GetContact(List<Models.ContactViewModel> result)
        {
          
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


    }
}
