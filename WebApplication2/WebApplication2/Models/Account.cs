using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Account
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string IdentityCode { get; set; }
        public string WineryID { get; set; }
    }
}