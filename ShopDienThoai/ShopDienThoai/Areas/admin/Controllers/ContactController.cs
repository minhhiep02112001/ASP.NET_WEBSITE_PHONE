using ShopDienThoai.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopDienThoai.Areas.admin.Controllers
{
    public class ContactController : Controller
    {
        // GET: admin/Contact
        private ShopDienThoaiDbContext db = new ShopDienThoaiDbContext();

        public ActionResult Index()
        {
            return View(db.contacts.Select(a=>a).OrderByDescending(c=>c.id).ToList());
        }
    }
}