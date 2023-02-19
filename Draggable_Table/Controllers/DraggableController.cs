using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Draggable_Table.Models;


namespace Draggable_Table.Controllers
{
    public class DraggableController : Controller
    {
        dragable_table_exEntities1 db = new dragable_table_exEntities1();
        public ActionResult Index()
        {
            var item = db.dnms.ToList();
            var item2 = item.OrderBy(x=>x.rowNo);
            return View(item2.ToList());
        }

        public ActionResult UpdateItem(string itemids)
        {
            int count = 1;
            List<int> itemidList = new List<int>();
            itemidList = itemids.Split(",".ToCharArray(),StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            foreach(var itemid in itemidList)
            {
                try
                {
                    dnm item = db.dnms.Where(x=>x.id == itemid).FirstOrDefault();
                    item.rowNo = count;
                    db.dnms.AddOrUpdate(item);
                    db.SaveChanges();   
                }
                catch(Exception)
                {
                    continue;
                }
                count++;
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}