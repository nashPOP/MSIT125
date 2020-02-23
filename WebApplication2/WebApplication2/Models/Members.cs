using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using EntityModels;

namespace EntityModels
{
    public class Members
    {
        
        public int AccountID { get; set; }

        [Display(Name="帳號")]
        [Required]
        public string fAccount { get; set; }

        [Display(Name = "密碼")]
        [Required]
        public string fPassWord { get; set; }

        [Display(Name = "身分識別")]
        [Required]
        public string fIdentityCode { get; set; }
       
        public int fWineryID { get; set; }
    }
}