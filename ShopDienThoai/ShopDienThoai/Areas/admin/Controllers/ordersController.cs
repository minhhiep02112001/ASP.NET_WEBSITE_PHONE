using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopDienThoai.EntityFramework;

namespace ShopDienThoai.Areas.admin.Controllers
{
    public class ordersController : BaseController
    {
        private ShopDienThoaiDbContext db = new ShopDienThoaiDbContext();
        
        // GET: admin/orders
        public ActionResult DatHang()
        {
            return View(db.orders.Where(model=>model.order_Status_id == 0 && model.is_remove ==false).ToList());
        }
        public ActionResult XuLy()
        {
            return View(db.orders.Where(model => model.order_Status_id == 1 && model.is_remove == false).ToList());

        }
        public ActionResult HoanThanh()
        {
            return View(db.orders.Where(model => model.order_Status_id == 2 && model.is_remove == false).ToList());
        }
        public ActionResult Huy()
        {
            return View(db.orders.Where(model => model.order_Status_id == 3 && model.is_remove == false).ToList());
        }

        [ChildActionOnly]
        public ActionResult ShowDetailsProdcutOrder(long id)
        {
            var listDetailsOrder = db.order_details.Where(c => c.order_id == id).ToList();
            ViewBag.count = listDetailsOrder.Count;
            ViewBag.ListProduct = db.products.Select(a => a).ToList();
            return PartialView("ShowDetailsProdcutOrder", listDetailsOrder);
        }

        // GET: admin/orders/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: admin/orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id , FormCollection formCollection)
        {
            var status = int.Parse(formCollection["status"]);
            var order = db.orders.Find(id);
            if (order != null)
            {
                order.order_Status_id = status;
                db.SaveChanges();
            }
            return RedirectToActionPermanent("Edit","orders",new { id = id });
        }

        // GET: admin/orders/Delete/5
        [HttpDelete]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            else
            {
                order.is_remove = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
