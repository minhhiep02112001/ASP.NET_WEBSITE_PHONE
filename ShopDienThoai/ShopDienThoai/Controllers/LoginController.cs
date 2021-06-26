using ShopDienThoai.Common;
using ShopDienThoai.EntityFramework;
using ShopDienThoai.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopDienThoai.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private ShopDienThoaiDbContext db = new ShopDienThoaiDbContext();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult PostLogin(CustomerLogin customer)
        {
            if (ModelState.IsValid)
            {
                var user = db.customers.FirstOrDefault(c => c.email == customer.email);
                if (user == null || user.is_remove.Value)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại!");

                }
                else if (user.is_active == false)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa!");
                }
                else if (user.pass_word.Trim() == Encryptor.GetHash(customer.password))
                {
                    var userSession = new CustomerSession();
                    userSession.id = user.id;
                    userSession.name = user.name;
                    Session.Add(CommonConstants.CUSTOMER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng!");
                }

            }
            return View();
        }

        [Route("Register")]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [Route("Register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.is_active = true;
                customer.is_remove = false;
                customer.pass_word = Encryptor.GetHash(customer.pass_word);
                db.customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToRoute("Register");
        }

        public ActionResult Logout()
        {
            Session.Remove(CommonConstants.CUSTOMER_SESSION);
            return RedirectToAction("Index", "Home");
        }

    }
}