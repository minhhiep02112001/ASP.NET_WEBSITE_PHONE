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
    public class bannersController : BaseController
    {
        private ShopDienThoaiDbContext db = new ShopDienThoaiDbContext();

        // GET: admin/banners
        public ActionResult Index()
        {
            return View(db.banners.Where(c=>c.is_remove==false).ToList());
        }

        // GET: admin/banners/Details/5
       
        // GET: admin/banners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/banners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "title,slug,images,url,targets,descriptions,position,is_active")] banner banner)
        {
            if (ModelState.IsValid)
            {
                if (banner.is_active == null)
                {
                    banner.is_active = false;
                }
                banner.create_at = DateTime.Now;
                banner.is_remove = false;
                db.banners.Add(banner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(banner);
        }

        // GET: admin/banners/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            banner banner = db.banners.Find(id);
            if (banner == null || banner.is_remove.Value)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: admin/banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,title,slug,images,url,targets,descriptions,position,is_active")] banner banner)
        {
            if (ModelState.IsValid)
            {
                var old_banner = db.banners.Find(banner.id);
                if (banner.is_active == null)
                {
                    banner.is_active = false;
                }
                old_banner.title = banner.title;
                old_banner.images = banner.images;
                old_banner.is_active = banner.is_active;
                old_banner.url = banner.url;
                old_banner.slug = banner.slug;
                old_banner.targets = banner.targets;
                old_banner.position = banner.position;
                old_banner.descriptions = banner.descriptions;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banner);
        }

        // GET: admin/banners/Delete/5
        [HttpDelete]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            banner banner = db.banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            else
            {
                banner.is_remove = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
