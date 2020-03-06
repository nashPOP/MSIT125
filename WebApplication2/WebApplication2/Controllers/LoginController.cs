using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Data.SqlClient;
using WebApplication2.ModelViews;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace WebApplication2.Controllers
{
    public class LoginController : Controller
    {
        Frog_JumpEntities db = new Frog_JumpEntities();
        AAccount ac = new AAccount();

        // GET: Login
        public ActionResult Index()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Delete(string account1)
        {

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
        public ActionResult EditPassWord(string Account1, string Password, string Password1)
        {
            var q = db.Account.FirstOrDefault(p => p.Account1 == Account1 && p.Password == Password);

            if (Account1 == q.Account1 && Password == q.Password)
            {

                q.Password = Password1;

                db.SaveChanges();
                return RedirectToAction("LoginPage");

            }
            else
            {
                ViewBag.Message("帳號與密碼不相同,請重新輸入!");
                return RedirectToAction("EditPassWord");
            }


        }

        public ActionResult EditPassWord()
        {
            //接受輸入進表單的值,並做修改:
            if (Session["account1"] != null)
            {
                string account = Session["account1"] as string;
                var q = db.Account.FirstOrDefault(p => p.Account1 == account);
                return View(q);
            }
            else
            {
                return RedirectToAction("LoginPage");
            }

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(string wineryname, string wineryphone, string wineryaddress, string wineryemail)
        {

            if (!db.Winery.All(P => P.WineryName == wineryname && P.WineryPhone == wineryphone && P.WineryAddress == wineryaddress && P.WineryEmail == wineryemail))
            {

                db.Winery.Add(new Winery() { WineryName = wineryname, WineryPhone = wineryphone, WineryAddress = wineryaddress, WineryEmail = wineryemail });
                db.SaveChanges();
                //如果split有錯,把值由0改成1:

                string s = db.Account.OrderByDescending(P => P.Account1).Select(P => P.Account1).FirstOrDefault().Substring(1);
                int.TryParse(s, out int a);


                var k = db.Winery.FirstOrDefault(P => P.WineryName == wineryname && P.WineryPhone == wineryphone && P.WineryAddress == wineryaddress && P.WineryEmail == wineryemail).WineryID;
                db.Account.Add(new Account() { Account1 = "A" + (a + 1), IdentityCode = "A", Password = "123456000".Trim(), WineryID = k });
                db.SaveChanges();
                return RedirectToAction("LoginPage");

            }
            else
            {

                return View();

                //}


            }
        }
        //註解可拿掉
        public ActionResult Register()
        {
            if (Session["account1"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoginPage");


            }

        }

        //註解可拿掉

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginPage(string account1, string PassWord)
        {


            var q = db.Account.FirstOrDefault(p => p.Account1 == account1 && p.Password == PassWord);


            //輸入之帳號與db相同,但密碼不同時:
            if (account1 == q.Account1 && PassWord != q.Password)
            {
                ViewBag.Message = "密碼錯誤,請重新輸入!";
                return View("LoginPage");
            }
            //輸入之帳號與密碼和db不同時:
            else if (account1 != q.Account1 && PassWord != q.Password)
            {
                ViewBag.Message = "帳號與密碼皆錯誤,請重新輸入!";
                return View("LoginPage");

            }
            //輸入之帳號和db不同,但密碼相同時:
            else if (account1 != q.Account1 && PassWord == q.Password)
            {
                ViewBag.Message = "帳號錯誤,請重新輸入!";
                return View();

            }
            //輸入之帳號和密碼與db相同,且識別碼為A時:
            if (account1 == q.Account1 && PassWord == q.Password && q.IdentityCode.Trim() == "A")
            {
                ViewBag.Message = "歡迎進入本網站!";
                Session["account1"] = q.Account1.ToString();
                Session["IdentityCode"] = q.IdentityCode.ToString();
                Session["WineryID"] = q.WineryID.ToString();
                return RedirectToAction("EditPassWord", "Login");

            }

            //如果帳號密碼皆與DB相同,但識別碼為B者:
            else if (account1 == q.Account1 && PassWord == q.Password && q.IdentityCode.Trim() == "B")
            {
                ViewBag.Message = "歡迎進入本網站!";
                Session["account1"] = q.Account1.ToString();
                Session["IdentityCode"] = q.IdentityCode.ToString();
                return RedirectToAction("EnterStock", "EnterStock");
            }
            //當帳密為空值或null時:
            else if (account1 == "" && PassWord == "")
            {
                ViewBag.message = "請輸入帳號跟密碼,帳號密碼不可為空值!";
                return RedirectToAction("LoginPage", "Login");
            }
            else if (account1 == q.Account1 && PassWord == "")
            {
                ViewBag.message = "請輸入密碼,密碼不可為空值!";
                return RedirectToAction("LoginPage", "Login");
            }
            else if (account1 == q.Account1 && PassWord == null)
            {
                ViewBag.message = "請輸入帳號跟密碼,帳號密碼不可為空值!";
                return RedirectToAction("LoginPage", "Login");
            }
            else if (account1 != "" && account1 != null && PassWord != "" && PassWord != null)
            {
                if (account1 == q.Account1 && PassWord != q.Password)
                {
                    ViewBag.message = "請輸入正確帳號跟密碼,帳號密碼不可隨便填寫!";
                    return RedirectToAction("LoginPage", "Login");
                }
                else
                {
                    ViewBag.message = "請輸入正確帳號跟密碼,帳號密碼不可隨便填寫!";
                    return RedirectToAction("LoginPage", "Login");
                }

            }
            else
            {
                return View();
            }

        }

        public ActionResult LoginPage(string logout)
        {
            if (logout != null)
            {
                Session.Remove("accout1");
                Session.Remove("IdentityCode");
                Session.Remove("WineryID");

                return View();
            }
            else
            {
                return View();
            }

        }

    }
}




