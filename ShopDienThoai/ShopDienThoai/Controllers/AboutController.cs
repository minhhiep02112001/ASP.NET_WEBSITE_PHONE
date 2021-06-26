using ShopDienThoai.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopDienThoai.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        private ShopDienThoaiDbContext db = new ShopDienThoaiDbContext();
        public ActionResult Index()
        {
            
            return View();
        }
        [ValidateInput(false)]
        public ActionResult postContact(FormCollection formCollection)
        {
            var contact = new contact();
            contact.name = formCollection["name"];
            contact.phone = formCollection["phone"];
            contact.email = formCollection["email"];
            contact.content = formCollection["content"];
            contact.create_at = DateTime.Now;
            db.contacts.Add(contact);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}