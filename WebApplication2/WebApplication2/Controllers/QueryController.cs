using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.ModelViews;

namespace WebApplication2.Controllers
{
    public class QueryController : Controller
    {
        #region Order Query Web

        // GET: OrderQuery
        /// <summary>
        /// Order Query (訂單查詢) 
        /// Qrder List (訂單資料表) 
        /// </summary>
        /// <name>林承緯</name>
        /// <date>2020/02/05</date>
        /// <returns>IEnumerable<FrogJump.Order></returns>
        public ActionResult OrderQuery()
        {
            OrderQueryFastory fas = new OrderQueryFastory();
            var OrderTable = fas.getAllOrder();
            return View(OrderTable);
        }

        [HttpPost]
        public ActionResult OrderQuery(QOrder qOrder)
        {
            OrderQueryFastory fas = new OrderQueryFastory();
            var Query = fas.getOrderQuery(qOrder);
            return View(Query);
        }

        public ActionResult OrderEdit()
        {
            return RedirectToAction("OrderQuery");
        }

        public ActionResult OrderDelete()
        {
            return RedirectToAction("OrderQuery");
        }

        #endregion

        #region Instock Query Web

        public ActionResult InStockQuery()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult InStockQuery(FJInStock fjIS)
        //{
        //    FrogJumpEntities db = new FrogJumpEntities();
        //    var Query = Model.Select(p => p);
        //    if (!string.IsNullOrEmpty(fjIS.id))
        //    {
        //        Query.Where(ID => ID.id == fjIS.id);
        //    }

        //    return View(Query);
        //}
        #endregion

        #region Inventory Query Web

        public ActionResult InventoryQuery()
        {
            return View();
        }

        #endregion

    }
}
