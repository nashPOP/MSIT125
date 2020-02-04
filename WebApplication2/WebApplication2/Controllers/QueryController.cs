using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class QueryController : Controller
    {
        // GET: OrderQuery
        public ActionResult OrderQuery()
        {
            return View();
        }

        public ActionResult InStockQuery()
        {
            return View();
        }

        public ActionResult InventoryQuery()
        {
            return View();
        }

        public ActionResult Query()
        {
            return View();
        }
    }
}
