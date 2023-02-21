using Draggable_Table.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Draggable_Table.Controllers
{
    public class DraggableController : Controller
    {
        dragable_table_exEntities db = new dragable_table_exEntities();
        public ActionResult Index()
        {
            var item = db.dnm.ToList();
            var item2 = item.OrderBy(x => x.rowNo);
            return View(item2.ToList());
        }
        public ActionResult UpdateItem(string itemIds)
        {
            int count = 1;
            List<int> itemIdList = new List<int>();
            itemIdList = itemIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            foreach(var itemId in itemIdList)
            {
                try
                {
                    dnm item = db.dnm.Where(x => x.id == itemId).FirstOrDefault();
                    item.rowNo = count;
                    db.dnm.AddOrUpdate(item);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    continue;
                }
                count++;
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}