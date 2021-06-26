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
    public class categoryController : BaseController
    {
        private ShopDienThoaiDbContext db = new ShopDienThoaiDbContext();

        // GET: admin/category
        public ActionResult Index()
        {
            return View(db.categories.Where(a => a.is_remove== false).ToList());
        }

        private void SetSelectOption(long? selected = null)
        {
            var ListSelectOption = db.categories.Where(a => a.is_remove == false).ToList();
            var lsp = new category();
            lsp.parent_id = 0;
            lsp.name = " --- Không thuộc loại nào ---";
            ListSelectOption.Insert(0, lsp);
            ViewBag.MovieType = new SelectList(ListSelectOption, "id", "name", selected);
        }

        // GET: admin/category/Create
        public ActionResult Create()
        {
            SetSelectOption();
            return View();
        }

        // POST: admin/category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name,slug,icon,parent_id,position,is_active")] category category)
        {
            if (ModelState.IsValid)
            {
                
                if (category.is_active == null)
                {
                    category.is_active = false;
                }
                category.is_remove = false;
                db.categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            SetSelectOption(category.parent_id);
            return View(category);
        }

        // GET: admin/category/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null || category.is_remove.Value)
            {
                return HttpNotFound();
            }
            SetSelectOption(category.parent_id);
            return View(category);
        }

        // POST: admin/category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,slug,icon,parent_id,position,is_active")] category category)
        {
            if (ModelState.IsValid)
            {

                if (category.is_active == null)
                {
                    category.is_active = false;
                }
                var old_cate = db.categories.Find(category.id);
                old_cate.name = category.name ;
                old_cate.slug = category.slug;
                old_cate.icon = category.icon;
                old_cate.parent_id = category.parent_id;
                old_cate.position = category.position;
                old_cate.is_active = category.is_active;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            SetSelectOption(category.parent_id);
            return View(category);
        }

        // GET: admin/category/Delete/5
        [HttpDelete]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            else
            {
                category.is_remove = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
