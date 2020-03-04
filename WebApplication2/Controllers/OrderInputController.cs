using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.ModelViews;
using WebApplication2.Models;
using WebApplication4.ViewModel;

namespace WebApplication4.Controllers
{
    public class OrderInputController : Controller
    {
        // GET: OrderInput 
        public ActionResult InputPage()
        {
            dbSimulator db = new dbSimulator();
            //set datas
            OrderInputViewModel model = new OrderInputViewModel();
            model.Winerys = db.GetWinerys();
            model.Categoerys = db.GetCategoerys();
            model.ProductsDatas = db.GetProductsDatas();
            return View(model);
        }

        //POST: ORDERIMPUT
        [HttpPost]
        public ActionResult InputPagePost(OrderInputPostModel[] input)
        {
            dbSimulator db = new dbSimulator();
            db.AddToDb(input);
            return Json(input);
        }


        //public ActionResult InputSuccess()
        //{
        //    return View();
        //}
    }
}