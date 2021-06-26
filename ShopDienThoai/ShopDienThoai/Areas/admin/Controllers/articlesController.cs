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
    public class articlesController : BaseController
    {
        private ShopDienThoaiDbContext db = new ShopDienThoaiDbContext();

        // GET: admin/articles
        public ActionResult Index()
        {
            return View(db.articles.Where(a => a.is_remove == false).ToList());
        }

        public ActionResult Comment()
        {
            ViewBag.ListArticles = db.articles.Select(a => a).ToList();
            return View(db.comments.Where(a => a.is_remove == false).ToList());
        }
        // GET: admin/articles/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            article article = db.articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: admin/articles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "title,slug,images,summary,descriptions,position,is_hot,url,is_active,meta_title,meta_description")] article article)
        {
            if (ModelState.IsValid)
            {
                if (article.is_hot == null)
                {
                    article.is_hot = false;
                }
                if (article.is_active == null)
                {
                    article.is_active = false;
                }
                article.created_at = DateTime.Now;
                article.is_remove = false;
                db.articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: admin/articles/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            article article = db.articles.Find(id);
            if (article == null || article.is_remove.Value)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: admin/articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,title,slug,images,summary,descriptions,position,is_hot,url,is_active,meta_title,meta_description")] article article)
        {
            if (ModelState.IsValid)
            {
                if (article.is_hot == null)
                {
                    article.is_hot = false;
                }
                if (article.is_active == null)
                {
                    article.is_active = false;
                }
                var old_articles = db.articles.Find(article.id);
                old_articles.title = article.title;
                old_articles.slug = article.slug;
                old_articles.images = article.images;
                old_articles.summary = article.summary;
                old_articles.descriptions = article.descriptions;
              
                old_articles.is_hot = article.is_hot;
                old_articles.url = article.url;
                old_articles.is_active = article.is_active;
                old_articles.meta_title = article.meta_description;
                old_articles.meta_description = article.meta_description;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: admin/articles/Delete/5
        [HttpDelete]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            article article = db.articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            else
            {
                article.is_remove = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpDelete]
        public ActionResult DeleteComment(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var commnet = db.comments.Find(id);
            if (commnet == null)
            {
                return HttpNotFound();
            }
            else
            {
                commnet.is_remove = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
