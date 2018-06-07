using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactManager.Models
{
    public class ContactViewModel
    {
        public int ID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string FaxNumber { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

    }


    public class ContactID
    {
        public int contactID { get; set; }


    }

    public class EditContactTest
    {
        public int contactID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }


    }


}