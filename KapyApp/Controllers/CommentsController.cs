using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KapyApp.Models;

namespace KapyApp.Controllers
{
    public class CommentsController : Controller
    {
        private kapymvc1Entities db = new kapymvc1Entities();

        // GET: Comments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.User).Include(c => c.News1);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.Users, "userId", "userId");
            //ViewBag.userId = new SelectList(db.Users, "userId", "userName");
            ViewBag.newsId = new SelectList(db.News1, "newsId", "newsId");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "commentId,userId,newsId,postTime,commentContent,isDisplayed,numOfComentLikes")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.Users, "userId", "userName", comment.userId);
            ViewBag.newsId = new SelectList(db.News1, "newsId", "uniqueTitle", comment.newsId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.Users, "userId", "userName", comment.userId);
            ViewBag.newsId = new SelectList(db.News1, "newsId", "uniqueTitle", comment.newsId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "commentId,userId,newsId,postTime,commentContent,isDisplayed,numOfComentLikes")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.Users, "userId", "userName", comment.userId);
            ViewBag.newsId = new SelectList(db.News1, "newsId", "uniqueTitle", comment.newsId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //This function retrieve all comments for a given news
        // GET: Comments/CommentForNews/5
        public ActionResult CommentForNews(int? mynewsid)
        {


            if (mynewsid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var commentsForNews = (from c in db.Comments where c.newsId == mynewsid select new { c.commentContent, c.userId, c.User, c.isDisplayed, c.numOfComentLikes, c.postTime });
            if (commentsForNews == null)
            {


                return HttpNotFound();
            }
            ViewBag.commentsForNews = commentsForNews;
            return View(commentsForNews);


        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
