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
    public class productsController : BaseController
    {
        private ShopDienThoaiDbContext db = new ShopDienThoaiDbContext();
      
         
        // GET: admin/products
        public ActionResult Index()
        {
            ViewBag.categorys = db.categories.Where(C => C.is_remove == false).ToList();
           
            return View(db.products.Where(C=>C.is_remove==false).ToList());
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

        // GET: admin/products/Create
        public ActionResult Create()
        {
            SetSelectOption();
            return View();
        }

        // POST: admin/products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "name,slug,quantity,images,price,sale,is_active,is_hot,category_id,sku,url,color,memory,summary,descriptions,meta_title,meta_description")] product product)
        {
            if (ModelState.IsValid)
            {
                if(product.is_hot == null)
                {
                    product.is_hot = false;
                }
                if (product.is_active == null)
                {
                    product.is_active = false;
                }
                product.is_remove = false;
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            SetSelectOption(product.category_id);
            return View(product);
        }

        // GET: admin/products/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null || product.is_remove.Value)
            {
                return HttpNotFound();
            }
            
            SetSelectOption(product.category_id);
            return View(product);
        }

        // POST: admin/products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,name,slug,images,price,sale,is_active,quantity,is_hot,category_id,sku,url,color,memory,summary,descriptions,meta_title,meta_description")] product product)
        {
            if (ModelState.IsValid)
            {
                if (product.is_hot == null)
                {  
                    product.is_hot = false;
                }
                if (product.is_active == null)
                {
                    product.is_active = false;
                }
                var old_product = db.products.Find(product.id);
                old_product.name = product.name;
                old_product.slug = product.slug;
                old_product.images = product.images;
                old_product.price = product.price;
                old_product.sale = product.sale;
                old_product.is_active = product.is_active;
                old_product.is_hot = product.is_hot;
                old_product.category_id = product.category_id;
                old_product.sku = product.sku;
                old_product.quantity = product.quantity;
                old_product.url = product.url;
                old_product.color = product.color;
                old_product.memory = product.memory;
                old_product.summary = product.summary;
                old_product.descriptions = product.descriptions;
                old_product.meta_title =product.meta_title;
                old_product.meta_description = product.meta_description;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            SetSelectOption(product.category_id);
            return View(product);
        }

        // GET: admin/products/Delete/5
        [HttpDelete]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                product.is_remove = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
