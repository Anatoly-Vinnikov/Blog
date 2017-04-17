using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog2.Models;
using Microsoft.AspNet.Identity;

namespace Blog2.Controllers
{
    public class ArticlesController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Articles
        public ActionResult Index()
        {
            return View(db.Articles.ToList());
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleDetails artDet = new ArticleDetails();
            artDet.Article = db.Articles.Find(id);
            List<Comment> comments = new List<Comment>();
            if (artDet.Article == null)
            {
                return HttpNotFound();
            }
            foreach (var comment in db.Comments)
            {
                if (comment.ArticleId == id)
                    comments.Add(comment);
            }
            artDet.Comments = comments;
            return View(artDet);
        }

        [HttpPost]
        public ActionResult Details([Bind(Include = "Id,ArticleId,Creator,Text,CreationDate")] Article article)
        {
            Comment comment = new Comment();
            comment.ArticleId = Int32.Parse(Request.Form["ArticleId"]);
            comment.CreationDate = DateTime.Now;
            comment.Creator = User.Identity.GetUserName();
            comment.Text = Request.Form["CommentText"];
            db.Comments.Add(comment);
            db.SaveChanges();
            return Redirect(Request.RawUrl);
        }

        // GET: Articles/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Creator,Title,Text,CreationDate,RatingGood,RatingBad")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.Creator = User.Identity.GetUserName();
                article.CreationDate = DateTime.Now;
                article.RatingGood = 0;
                article.RatingBad = 0;
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Creator,Title,Text,CreationDate,RatingGood,RatingBad")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Articles/Like/5
        [Authorize]
        public ActionResult Like(int id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            foreach (var rating in db.Ratings)
            {
                if (rating.Creator == User.Identity.GetUserName() && rating.ArticleId == id)
                {
                    if (rating.Like)
                    {
                        db.Ratings.Remove(rating);
                        article.RatingGood--;
                    }
                    else//dislike
                    {
                        rating.Dislike = false;
                        article.RatingBad--;
                        rating.Like = true;
                        article.RatingGood++;
                    }
                }
            }
            if (db.Entry(article).State == EntityState.Modified)
            {
                db.SaveChanges();
                return Redirect("/Articles/Details/" + id);
            }
            Rating newRating = new Rating();
            newRating.Like = true;
            newRating.Dislike = false;
            newRating.ArticleId = id;
            newRating.Creator = User.Identity.GetUserName();
            article.RatingGood++;
            db.Ratings.Add(newRating);
            db.Entry(article).State = EntityState.Modified;
            db.SaveChanges();
            return Redirect("/Articles/Details/" + id);
        }

        // GET: Articles/Dislike/5
        [Authorize]
        public ActionResult Dislike(int id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            foreach (var rating in db.Ratings)
            {
                if (rating.Creator == User.Identity.GetUserName() && rating.ArticleId == id)
                {
                    if (rating.Dislike)
                    {
                        db.Ratings.Remove(rating);
                        article.RatingBad--;
                    }
                    else//like
                    {
                        rating.Dislike = true;
                        article.RatingGood--;
                        rating.Like = false;
                        article.RatingBad++;
                    }
                }
            }
            if (db.Entry(article).State == EntityState.Modified)
            {
                db.SaveChanges();
                return Redirect("/Articles/Details/" + id);
            }
            Rating newRating = new Rating();
            newRating.Like = false;
            newRating.Dislike = true;
            newRating.ArticleId = id;
            newRating.Creator = User.Identity.GetUserName();
            article.RatingBad++;
            db.Ratings.Add(newRating);
            db.Entry(article).State = EntityState.Modified;
            db.SaveChanges();
            return Redirect("/Articles/Details/" + id);
        }

        // GET: Articles/Ratings
        public ActionResult Ratings()
        {
            var articles = db.Articles.ToList();
            articles.Sort((a, b) => (a.RatingGood-a.RatingBad).CompareTo(b.RatingGood - b.RatingBad));
            articles.Reverse();
            return View(articles);
        }
    }
}