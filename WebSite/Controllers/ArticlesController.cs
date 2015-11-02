using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebSite.DAL;
using WebSite.DAL.Models;
using WebSite.Mappers;
using WebSite.Models;

namespace WebSite.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        // GET: Articles
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var userName = User.Identity.GetUserName();
            List<DisplayArticleInListViewModel> list = new List<DisplayArticleInListViewModel>();
            foreach (Article item in db.Articles.ToList())
            {
                bool canEdit = (userId == item.UserId);
                list.Add(Mapper.MapToDisplayArticleInList(item, userName, canEdit));
            }
            return View(list);
        }

        // GET: Articles/MyArticles
        public ActionResult MyArticles()
        {
            var userId = User.Identity.GetUserId();
            var userName = User.Identity.GetUserName();
            List<DisplayArticleInListViewModel> list = new List<DisplayArticleInListViewModel>();
            foreach (Article item in db.Articles.Where(x => x.UserId == userId).ToList())
            {
                list.Add(Mapper.MapToDisplayArticleInList(item, userName, true));
            }
            return View(list);
        }

        // GET: Articles/MyComments
        public ActionResult MyComments()
        {
            var userId = User.Identity.GetUserId();
            var userName = User.Identity.GetUserName();
            List<DisplayCommentViewModel> list = new List<DisplayCommentViewModel>();
            foreach (Comment item in db.Comments.Where(x => x.UserId == userId).ToList())
            {
                list.Add(Mapper.MapToDisplayComment(item, userName, true));
            }
            return View(list);
        }

        // GET: Articles/Details/5
        public ActionResult Details(int id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            var userId = User.Identity.GetUserId();
            var userName = User.Identity.GetUserName();
            bool canEdit = (userId == article.UserId);
            DisplayArticleViewModel articleVM = Mapper.MapToDisplayArticle(article, userName, canEdit);
            return View(articleVM);
        }


        // GET: Articles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateArticleViewModel articleVM)
        {
            if (ModelState.IsValid)
            {
                Article article = Mapper.MapToArticle(articleVM);
                db.Articles.Add(article);
                var userId = User.Identity.GetUserId();
                article.UserId = userId;
                article.TimeCreated = DateTime.Now;

                //add category
                Category category = db.Categories.FirstOrDefault(x => x.Name == articleVM.CategoryName.ToString());
                if (category != null)
                {
                    article.Category = category;
                    category.Articles.Add(article);
                }
                else
                {
                    ModelState.AddModelError("", "This category does not exist.");
                    return View(articleVM);
                }

                // add tags
                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex(@"[ ]{2,}", options);
                articleVM.Tags = regex.Replace(articleVM.Tags, @" ");
                string[] tags = Regex.Split(articleVM.Tags, " ");
                article.Tags = new List<Tag>();
                foreach (string item in tags)
                {
                    Tag tag = db.Tags.FirstOrDefault(x => x.Name == item);
                    if (tag != null)
                    {
                        if (!article.Tags.Contains(tag))
                        {
                            article.Tags.Add(tag);
                            tag.Articles.Add(article);
                        }
                    }
                    else
                    {
                        Tag newTag = new Tag();
                        newTag.Name = item;
                        db.Tags.Add(newTag);
                        article.Tags.Add(newTag);
                        newTag.Articles = new List<Article>();
                        newTag.Articles.Add(article);
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Check your input.");
            }

            return View(articleVM);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            var userId = User.Identity.GetUserId();
            if (userId == article.UserId)
            {
                return HttpNotFound();
            }
            StringBuilder tags = new StringBuilder();
            foreach (Tag item in article.Tags.ToList())
            {
                tags.Append(item.Name + " ");
            }
            CreateArticleViewModel articleViewModel = Mapper.MapToEditAticle(article,tags.ToString());
            return View(articleViewModel);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateArticleViewModel articleVM)
        {
            if (ModelState.IsValid)
            {
                Article article = db.Articles.Find(articleVM.Id);
                // set new category
                if (article.Category.Name != articleVM.CategoryName.ToString())
                {
                    Category category = db.Categories.FirstOrDefault(x => x.Name == articleVM.CategoryName.ToString());
                    if (category != null)
                    {
                        article.Category = category;
                        category.Articles.Add(article);
                    }
                    else
                    {
                        ModelState.AddModelError("", "This category does not exist.");
                        return View(articleVM);
                    }
                }

                // set new tags
                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex(@"[ ]{2,}", options);
                articleVM.Tags = regex.Replace(articleVM.Tags, @" ");
                string[] tags = Regex.Split(articleVM.Tags, " ");
                foreach (string item in tags)
                {
                    if (article.Tags.FirstOrDefault(x => x.Name == item) == null)
                    {
                        Tag tag = db.Tags.FirstOrDefault(x => x.Name == item);
                        if (tag != null)
                        {
                            article.Tags.Add(tag);
                            tag.Articles.Add(article);
                        }
                        else
                        {
                            Tag newTag = new Tag();
                            newTag.Name = item;
                            db.Tags.Add(newTag);
                            newTag.Articles = new List<Article>();
                            newTag.Articles.Add(article);
                            article.Tags.Add(newTag);
                        }
                    }
                }
                
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(articleVM);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            var userId = User.Identity.GetUserId();
            if (userId == article.UserId)
            {
                return HttpNotFound();
            }
            DeleteArticleViewModel articleVM = Mapper.MapToDeleteArticle(article);
            return View(articleVM);
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
    }
}
