using PagedList;
using ShopDienThoai.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopDienThoai.Controllers
{
    public class ProductController : Controller
    {
        private ShopDienThoaiDbContext db = new ShopDienThoaiDbContext();
        // GET: danh-muc
        public ActionResult Index(string slug , int? page)
        {
            var pages = page ?? 1;
            var _category = db.categories.SingleOrDefault(model=>model.slug==slug);
            if(_category == null || _category.is_remove.Value)
            {
                return HttpNotFound();
            }
           
            var categoryChilder = db.categories.Where(cate => cate.parent_id == _category.id && cate.is_remove==false && cate.is_active == true).ToList();
            categoryChilder.Insert(0, _category);
            ViewBag.Categorys = categoryChilder;
            ViewBag.Selected = _category;
            var listID = categoryChilder.Select(c => c.id).ToList();

            var arrProduct = db.products.Where(m => listID.Contains(m.category_id) && m.is_remove == false && m.is_active == true).OrderBy(x => x.create_at).ToPagedList(pages, 12);
            
            return View(arrProduct);
        }


        //GET : san-pham/
        public ActionResult ProductDetails(string slug)
        {
            var product = db.products.SingleOrDefault(s => s.slug == slug);
            if (product == null || product.is_remove.Value || product.is_active.Value == false)
            {
                return HttpNotFound();
            }
            ViewBag.RelatedProduct= db.products.Where(m => m.category_id == product.category_id && m.id != product.id).Take(4).ToList();
            return View(product);
        }
    }
}