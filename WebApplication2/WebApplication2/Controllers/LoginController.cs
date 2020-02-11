using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Delete(string ACCOUNT, string PASSWORD)
        {

            return View();

        }

        public ActionResult Delete()
        {

            return View();

        }

        [HttpPost]
        public ActionResult EditAccount(string ACCOUNT, string PASSWORD)
        {

            return View();

        }

        public ActionResult EditAccount()
        {

            return View();

        }

        [HttpPost]
        public ActionResult Register(string ACCOUNT, string PASSWORD)
        {

            return View();

        }

        public ActionResult Register()
        {

            return View();

        }

        [HttpPost]
        public ActionResult Login(string ACCOUNT, string PASSWORD)
        {

            return View();

        }

        public ActionResult Login()
        {

            return View();

        }
    }
}