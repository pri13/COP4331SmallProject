//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ContactManager.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public int ID { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateLastLoggedIn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
