using PagedList;
using ShopDienThoai.Common;
using ShopDienThoai.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopDienThoai.Controllers
{
    public class ArticlesController : Controller
    {
        private ShopDienThoaiDbContext db = new ShopDienThoaiDbContext();
        // GET: tin-tuc
        public ActionResult Index(int? page)
        {
            var pages = page ?? 1;

            var articles = db.articles.Where(c => c.is_remove == false).OrderBy(x => x.created_at).ToPagedList(pages, 5);

            return View(articles);
        }

        // chi tiết tin tức
        public ActionResult ChiTietTinTuc(string slug)
        {
            var article = db.articles.SingleOrDefault(s => s.slug == slug);
            if (article == null || article.is_remove.Value)
            {
                return HttpNotFound();
            }
            ViewBag.Comments = db.comments.Where(c => c.articles_id == article.id).ToList();
            return View(article);
        }


        // commnet tin tức
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChiTietTinTuc(string slug , FormCollection formCollection)
        {
            var sessionCustomer = (CustomerSession)Session["CUSTOMER_SESSION"];
            if(sessionCustomer == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var noidung = formCollection["noidung"];
                var article = db.articles.SingleOrDefault(s => s.slug == slug);
                var comment = new comment();
                comment.customer_id = sessionCustomer.id;
                comment.meta_title = noidung;
                comment.name = db.customers.Find(sessionCustomer.id).name;
                comment.is_remove = false;
                comment.articles_id = article.id;
                comment.created_at = DateTime.Now;
                db.comments.Add(comment);
                db.SaveChanges();
                return RedirectToRoute("ChiTietTinTuc", new { slug = article.slug });
            }
        }
    }
}