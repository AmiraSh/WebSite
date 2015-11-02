using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Mvc;
using WebSite.DAL;
using WebSite.DAL.Models;
using WebSite.Mappers;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        public ActionResult Index()
        {
            var userName = User.Identity.GetUserName();
            List<DisplayArticleInListViewModel> list = new List<DisplayArticleInListViewModel>();
            foreach (Article item in db.Articles.ToList())
            {
                list.Add(Mapper.MapToDisplayArticleInList(item, userName));
            }
            return View(list);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        
        public ActionResult Details(int id)
        {
            var userName = User.Identity.GetUserName();
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            DisplayArticleViewModel articleVM = Mapper.MapToDisplayArticle(article, userName);
            return View(articleVM);
        }
    }
}