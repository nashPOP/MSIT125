using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.ModelViews
{
    [ValidateAntiForgeryToken]
    public class AAccount
    {   [Display(Name ="帳號")]
        [Required]
        public string Account { get; set; }
        [Display(Name = "密碼")]
        [Required]
        [Editable(true)]
        public string Password { get; set; }

        [Display(Name = "識別碼")]
        [Required]
        public string IdentityCode { get; set; }
        [Display(Name = "酒莊序號")]
        [Required]
        public string WineryID { get; set; }
        [Display(Name = "序號")]
        [Required]
        public string AccountID { get; set; }
        [Display(Name = "酒莊名稱")]
        [Required]
        public string WineryName { get; set; }
        [Display(Name = "酒莊電話")]
        [Required]
        [Phone]
        public string WineryPhone { get; set; }
        [Display(Name = "酒莊地址")]
        [Required]
        public string WineryAddress { get; set; }
        [Display(Name = "酒莊郵件")]
        [Required]
        [EmailAddress]
        public string WineryEmail { get; set; }
    }
}