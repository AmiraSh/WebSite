using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.DAL;
using WebSite.DAL.Models;
using WebSite.Models;

namespace WebSite.Mappers
{
    public static class Mapper
    {
        public static DeleteArticleViewModel MapToDeleteArticle(Article article)
        {
            DeleteArticleViewModel articleVM = new DeleteArticleViewModel();
            articleVM.Name = article.Name;
            articleVM.Category = article.Category.Name;
            articleVM.Id = article.Id;
            return articleVM;
        }
        public static Article MapToArticle(CreateArticleViewModel articleVM)
        {
            Article article = new Article();
            article.Name = articleVM.Name;
            article.Text = articleVM.Text;
            return article;
        }
        public static CreateArticleViewModel MapToEditAticle(Article article, string tags)
        {
            CreateArticleViewModel articleVM = new CreateArticleViewModel();
            articleVM.Id = article.Id;
            articleVM.Name = article.Name;
            articleVM.Text = article.Text;
            EnumCategory choice;
            if (Enum.TryParse(article.Category.Name, out choice))
                articleVM.CategoryName = choice;
            articleVM.Tags = tags;
            return articleVM;
        }
        public static DisplayArticleInListViewModel MapToDisplayArticleInList(Article article, string userName)
        {
            DisplayArticleInListViewModel articleVM = new DisplayArticleInListViewModel();
            articleVM.Id = article.Id;
            articleVM.Name = article.Name;
            articleVM.Likes = article.Likes;
            articleVM.DisLikes = article.DisLikes;
            articleVM.Category = article.Category.Name;
            articleVM.CanEdit = false;
            articleVM.UserName = userName;
            return articleVM;
        }
        public static DisplayArticleInListViewModel MapToDisplayArticleInList(Article article, string userName, bool canEdit)
        {
            DisplayArticleInListViewModel articleVM = MapToDisplayArticleInList(article, userName);
            articleVM.CanEdit = canEdit;
            return articleVM;
        }
        public static DisplayArticleViewModel MapToDisplayArticle(Article article, string userName)
        {
            DisplayArticleViewModel articleVM = new DisplayArticleViewModel();
            articleVM.Id = article.Id;
            articleVM.Name = article.Name;
            articleVM.Text = article.Text;
            articleVM.Likes = article.Likes;
            articleVM.DisLikes = article.DisLikes;
            articleVM.Category = article.Category.Name;
            articleVM.TimeCreated = article.TimeCreated;
            List<DisplayCommentViewModel> list = new List<DisplayCommentViewModel>();
            foreach (Comment item in article.Comments.ToList())
            {
                list.Add(MapToDisplayComment(item, userName));
            }
            articleVM.Comments = list;
            articleVM.CanEdit = false;
            articleVM.UserName = userName;
            return articleVM;
        }        
        public static DisplayArticleViewModel MapToDisplayArticle(Article article, string userName, bool canEdit)
        {
            DisplayArticleViewModel articleVM = MapToDisplayArticle(article, userName);
            articleVM.CanEdit = canEdit;
            return articleVM;
        }
        public static DisplayCommentViewModel MapToDisplayComment(Comment comment, string userName)
        {
            DisplayCommentViewModel commentVM = new DisplayCommentViewModel();
            commentVM.Id = comment.Id;
            commentVM.Text = comment.Text;
            commentVM.Likes = comment.Likes;
            commentVM.DisLikes = comment.DisLikes;
            commentVM.CanDelete = false;
            commentVM.UserName = userName;
            return commentVM;
        }
        public static DisplayCommentViewModel MapToDisplayComment(Comment comment, string userName, bool canDelete)
        {
            DisplayCommentViewModel commentVM = MapToDisplayComment(comment, userName);
            commentVM.CanDelete = canDelete;
            return commentVM;
        }

        // ---- users ----
        public static ProfileViewModel MapToProfile(ApplicationUser user)
        {
            ProfileViewModel profile = new ProfileViewModel();
            profile.Id = user.Id;
            profile.FirstName = user.FirstName;
            profile.LastName = user.LastName;
            profile.Country = user.Country;
            profile.Position = user.Position;
            profile.University = user.University;
            profile.Country = user.Country;
            return profile;
        }
    }
}
