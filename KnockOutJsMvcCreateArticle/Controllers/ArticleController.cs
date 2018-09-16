using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnockOutJsMvcCreateArticle.Models;

namespace KnockOutJsMvcCreateArticle.Controllers
{
    public class ArticleController : Controller
    {
        private ArticleDBContex db = new ArticleDBContex();

        //
        // GET: /Article/
        public ActionResult PartialIndex()
        {
            return View(db.ArticleDB.ToList());
        }

        public ActionResult Mapping()
        {
            return View();
        }
        public ActionResult Mappingajax()
        {
            return View();
        }
        
        public ActionResult PartialMappingIndex()
        {
            return View(db.ArticleDB.ToList());
        }
        public ActionResult Index()
        {
            return View(db.ArticleDB.ToList());
        }

        //
        // GET: /Article/Details/5

        public ActionResult IndexRazor()
        {
            return View(db.ArticleDB.ToList());
        }

        public ActionResult EditGrid()
        {
            return View();
        }


        public ActionResult JsonModelView(int id = 0)
        {
            Article article = db.ArticleDB.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }
        public ActionResult Details(int id=0)
        {
            Article article = db.ArticleDB.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        //
        // GET: /Article/Create

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Example2()
        {
            return View();
        }
        public ActionResult Example3()
        {
            return View();
        }
        public ActionResult Example4()
        {
            return View();
        }
        public ActionResult Load(int id=0)
        {
            Article article = db.ArticleDB.Find(id);
            return Json(article, JsonRequestBehavior.AllowGet);
            //return Json(new Article { Title = "Rambo", Excerpts = "dddd", Content="dfdfd", id=111 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NestedVM()
        {
            return View();
        }
        //
        // POST: /Article/Create

      /*  [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                db.ArticleDB.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }
        */
       [HttpPost]
       public String Create(Article article)
        {
            db.ArticleDB.Add(article);
            db.SaveChanges();
            return ("success") ;
        }
       
        public JsonResult FillIndex()
        {
            return Json(db.ArticleDB.ToList(), JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Article/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Article article = db.ArticleDB.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        public ActionResult EditJSONMapping(int id = 0)
        {
            Article article = db.ArticleDB.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }
        //
        // POST: /Article/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        //
        // GET: /Article/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Article article = db.ArticleDB.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        //
        // POST: /Article/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.ArticleDB.Find(id);
            db.ArticleDB.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}