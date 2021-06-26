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
    public class customersController : BaseController
    {
        private ShopDienThoaiDbContext db = new ShopDienThoaiDbContext();

        // GET: admin/customers
        public ActionResult Index()
        {
            return View(db.customers.Where(c=>c.is_remove==false).ToList());
        }

        // GET: admin/customers/Details/5


        // GET: admin/customers/Delete/5
        [HttpDelete]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                customer.is_remove = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
