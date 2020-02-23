using EntityModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Controllers;
using System.Data.SqlClient;

namespace WebApplication2.Controllers
{
    public class LoginController : Controller
    {
        DELogisticsEntities1 db = new DELogisticsEntities1();
        Members Members = new Members();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Delete(string Account, string PassWord)
        {
            DELogisticsEntities1 db = (new DELogisticsEntities1());
            Members MEMBERS = db.Accounts.FirstOrDefault(p => p.Account1 == Account);
            if (MEMBERS!= null)
            {
                db.Accounts.Remove(MEMBERS);
                db.SaveChanges();
            }
            return RedirectToAction("List");

        }

        public ActionResult Delete()
        {

            return View();

        }

        
        [HttpPost]
        public ActionResult EditPassWord(Members p)
        {
            DELogisticsEntities1 db = new DELogisticsEntities1();
            Members MEMBERS = db.Accounts.FirstOrDefault(t => t.PassWord == p.fPassWord);
            if (MEMBERS != null)
            {
                MEMBERS.fAccount = p.fAccount;
                MEMBERS.fPassWord = p.fPassWord;
                MEMBERS.fIdentityCode = p.fIdentityCode;
                MEMBERS.fWineryID = p.fWineryID;
               
                db.SaveChanges();
            }
            return RedirectToAction("List");
           

        }

        public ActionResult EditPassWord(string fPassWord)
        {
            Members MEMBERS = (new DELogisticsEntities1()).Accounts.FirstOrDefault(p => p.PassWord == fPassWord);
            if (MEMBERS == null)
                return RedirectToAction("List");
            return View(MEMBERS);
           

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(string Account,string PassWord)
        {

          DELogisticsEntities1 db = new DELogisticsEntities1();
            db.Accounts.Add(Account,PassWord);
            db.SaveChanges();
            return RedirectToAction("List");
           

        }

        public ActionResult Register(Members P)
        {
            return View(new Account());
        }
           

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginPage(string Account, string PassWord)
        {

            Members member = new Members();
            var q = db.Accounts.FirstOrDefault(p => p.Account1 == Account && p.PassWord == PassWord);


            if (q != null&&q.IdentityCode=="A")
            {


                return RedirectToAction("");

            }
            else if (q!=null&&q.IdentityCode == "B")
            {
                return RedirectToAction("");
            }
            else if(q==null)
            {
                return RedirectToAction("");
            }
            else
            {
                return RedirectToAction("");
            }
        }

        public ActionResult LoginPage(string fAccount)
        {

            return View();

        }

        public ActionResult Logout()
        {

            return View();

        }

        [HttpPost]
        public ActionResult Logout(string Account, string PassWord)
        {

            return View();

        }
    }

  
}


