using ShopDienThoai.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopDienThoai.Controllers
{
    public class HomeController : Controller
    {
        private ShopDienThoaiDbContext db = new ShopDienThoaiDbContext();

        public ActionResult Index()
        {
            ViewBag.Banners = db.banners.Where(a=>a.is_active == true && a.is_remove == false).OrderByDescending(b=>b.position).ToList();
            ViewBag.AriclesHot = db.articles.Where(c => c.is_hot == true && c.is_active == true && c.is_remove == false).Take(5).ToList();
            ViewBag.ProductNew = db.products.Where(prod => prod.is_remove == false && prod.is_active == true).OrderByDescending(a => a.create_at).Take(8).ToList();
            ViewBag.ProductSelling = db.products.SqlQuery("select top(8) * from product where product.id in (select top(8) product_id  from order_details group by product_id , qty order by SUM(qty) desc)").ToList();
            ViewBag.ProductHot = db.products.Where(prod => prod.is_remove == false && prod.is_hot == true && prod.is_active == true).OrderByDescending(a => a.create_at).Take(8).ToList();
            return View();
        }
        [ChildActionOnly]
        public ActionResult LoadMenu()
        {
            var categorys = db.categories.Where(c=>c.parent_id == 0 && c.is_remove==false).ToList();
            return PartialView("LoadMenu", categorys);
        }
    }
}