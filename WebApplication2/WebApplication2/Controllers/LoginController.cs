using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Data.SqlClient;

namespace WebApplication2.Controllers
{
    public class LoginController : Controller
    {
        Frog_JumpEntities db = new Frog_JumpEntities();

        // GET: Login
        public ActionResult Index()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Delete(string account1)
        {
            Frog_JumpEntities db = new Frog_JumpEntities();
            var mems = db.Account.Where(p => p.Account1 == account1).FirstOrDefault();
            if (mems != null)
            {
                db.Account.Remove(mems);
                db.SaveChanges();
            }
            return RedirectToAction("LoginPage");

        }

        public ActionResult Delete()
        {

            return View();

        }


        [HttpPost]
        public ActionResult EditPassWord(string account1, string Password)
        {

            Frog_JumpEntities db = new Frog_JumpEntities();
            var mem = db.Account.FirstOrDefault(t => t.Password != Password);
            if (mem == null)
            {

                mem.Password = Password;


                db.SaveChanges();
            }
            return RedirectToAction("");


        }

        public ActionResult EditPassWord()
        {
            //var MEMBERS = (new DELogisticsEntities1()).Accounts.FirstOrDefault(p => p.PassWord == Password);
            //if (MEMBERS == null)
            //    return RedirectToAction("LoginPage");
            return View();


        }

        //        [ValidateAntiForgeryToken]
        //        [HttpPost]
        //        public ActionResult Register(string WineryName, string WineryPhone,string WineryAddress, string WineryEmail)
        //        {
        //            DELogisticsEntities1 db = new DELogisticsEntities1();
        //            if (!db.Wineries.All(P=>P.WineryName==WineryName&& P.WineryPhone ==             if (!db.Wineries.All(P => P.WineryName == WineryName && P.WineryPhone == WineryName && P.WineryName == WineryName && P.WineryName == WineryName,)
        //&& P.WineryName == WineryName &&P.WineryName == WineryName,)
        //            {
        //                db.Accounts.Add(new Account { Account1 = account1, PassWord = password,IdentityCode="A",});
        //                db.SaveChanges();

        //                return RedirectToAction("LoginPage");

        //            }
        //            else
        //            {

        //                return View();

        //            }


        //      }

        //public ActionResult Register()
        //{
        //    return View();
        //}



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LoginPage(string Account1, string PassWord)
        //{

        //    DELogisticsEntities1 db = new DELogisticsEntities1();
        //    var q = db.Accounts.FirstOrDefault(p => p.Account1 == Account1 && p.PassWord == PassWord);
        //    //這邊未來可能要改action位置
        //    if (q != null && q.IdentityCode.Trim() == "A")
        //    {
        //        Session["Account"] = q.Account1.ToString();
        //        Session["IdentityCode"] = q.IdentityCode.ToString();
        //        return RedirectToAction("EditPassWord", "Login");

        //    }
        //    //這邊未來可能要改action位置
        //    else if (q != null && q.IdentityCode.Trim() == "B")
        //    {
        //        Session["Account"] = q.Account1.ToString();
        //        Session["IdentityCode"] = q.IdentityCode.ToString();
        //        return RedirectToAction("EditPassWord", "Login");
        //    }
        //    else if (string.IsNullOrEmpty(Account1) && string.IsNullOrEmpty(PassWord) && q == null)
        //    {
        //        ViewBag.message = "未檢查到帳號,轉至註冊頁面";
        //        return RedirectToAction("Register", "Login");
        //    }
        //    else 
        //    {
        //        return View();
        //    }
        //    //var q = db.Accounts.FirstOrDefault(p => p.Account1 == p.Account1 && p.PassWord == p.PassWord);

        //    //if (q != null)
        //    //{
        //    //    Session["Account"] = q.Account1.ToString();
        //    //    Session["IdentityCode"] = q.IdentityCode.ToString();
        //    //    return RedirectToAction("Index");
        //    //}
        //    //else
        //    //{
        //    //    return View();

        //    //}

        //}

        //public ActionResult LoginPage()
        //{
        //    return View();
        //}
    }
}


