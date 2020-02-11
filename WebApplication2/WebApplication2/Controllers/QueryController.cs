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

        DDLFastory ddl = new DDLFastory();

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
            QueryFastory fas = new QueryFastory();
            QueryModelByView qv = new QueryModelByView()
            {
                Orderlist = fas.getAllOrder(),
                Wineryddl = ddl.getWinery(),
                Categoryddl = ddl.getCategory(),
                Productddl = ddl.getProduct()
            };
            
            return View(qv);
        }

        [HttpPost]
        public ActionResult OrderQuery(QOrder qOrder)
        {
            QueryFastory fas = new QueryFastory();
            QueryModelByView qv = new QueryModelByView()
            {
                Orderlist = fas.getOrderQuery(qOrder),
                Wineryddl = ddl.getWinery(),
                Categoryddl = ddl.getCategory(),
                Productddl = ddl.getProduct()
            };
            return View(qv);
        }

        public ActionResult OrderEdit()
        {
            return View();
        }

        public ActionResult OrderDelete()
        {
            return RedirectToAction("OrderQuery");
        }

        #endregion

        #region Instock Query Web

        public ActionResult InStockQuery()
        {
            QueryFastory fas = new QueryFastory();
            QueryModelByView qv = new QueryModelByView()
            {
                //StockEnterlist = fas.getAllStockEnter(),
                Wineryddl = ddl.getWinery(),
                Categoryddl = ddl.getCategory(),
                Productddl = ddl.getProduct()
            };
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
