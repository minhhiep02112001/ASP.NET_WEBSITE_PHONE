using ShopDienThoai.Areas.admin.Data;
using ShopDienThoai.Common;
using ShopDienThoai.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopDienThoai.Areas.admin.Controllers
{
    public class LoginController : Controller
    {
        private ShopDienThoaiDbContext db = new ShopDienThoaiDbContext();
        // GET: admin/Login
        public ActionResult Index()
        {
            return View();
            var userLogin = new UserLogin();
        }
        [HttpPost]
        public ActionResult Index(UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                var user = db.users.FirstOrDefault(c => c.email == userLogin.Username);
                if(user == null || user.is_remove.Value)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại!");
                    
                }
                else if (user.is_active == false)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa!");
                }
                else if (user.passwords.Trim() ==  Encryptor.GetHash(userLogin.Password))
                {
                    var userSession = new UserSession();
                    userSession.id = user.id;
                    userSession.name = user.name;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");

                }
                else 
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng!");
                }
  
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove(CommonConstants.USER_SESSION);
            return RedirectToAction("Index", "Login");
        }
    }
}