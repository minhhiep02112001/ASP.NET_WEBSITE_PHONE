using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopDienThoai.Common;
using ShopDienThoai.EntityFramework;

namespace ShopDienThoai.Areas.admin.Controllers
{
    public class usersController : BaseController
    {
        private ShopDienThoaiDbContext db = new ShopDienThoaiDbContext();

        // GET: admin/users
        public ActionResult Index()
        {
            return View(db.users.Where(c=>c.is_remove==false).ToList());
        }

        
  
        // GET: admin/users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name,email,passwords,avatar,is_active")] user user)
        {
            if (ModelState.IsValid)
            {
                var checkemail = db.users.SingleOrDefault(model => model.email == user.email);
                if(checkemail == null)
                {
                    user.created_at = DateTime.Now;
                    user.passwords = Encryptor.GetHash(user.passwords);
                    user.is_remove = false;
                    db.users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Email đã tồn tại!");
                }
            }

            return View(user);
        }

        // GET: admin/users/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null || user.is_remove.Value)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: admin/users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,email,passwords,avatar,is_active")] user user)
        {
            if (ModelState.IsValid)
            {
                var checkemail = db.users.SingleOrDefault(model => model.email == user.email && model.id != user.id);
                if (checkemail == null)
                {
                    var old_user = db.users.Find(user.id);
                    old_user.name = user.name;
                    old_user.email = user.email;
                    old_user.avatar = user.avatar;
                    old_user.is_active = user.is_active;
                    if (user.passwords != null)
                    {
                        old_user.passwords = Encryptor.GetHash(user.passwords);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Email đã tồn tại!");
                }
            }
            return View(user);
        }

        // GET: admin/users/Delete/5
        [HttpDelete]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                user.is_remove = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
