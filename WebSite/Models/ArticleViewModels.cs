using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Models
{
    public class CreateArticleViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "Length should be between 1 - 100 words.", MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Text is required.")]
        [Display(Name = "Text")]
        public string Text { get; set; }
        
        [Required(ErrorMessage = "Category is required.")]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
                
        [Display(Name = "Tags (separate with space)")]
        public string Tags { get; set; }
    }

    public class DisplayArticleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Text")]
        public string Text { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Likes")]
        public int Likes { get; set; }

        [Display(Name = "Diskes")]
        public int DisLikes { get; set; }

        [Display(Name = "Comments")]
        public IList<DisplayCommentViewModel> Comments { get; set; }

        public bool CanEdit { get; set; }
    }

    public class DisplayArticleInListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Likes")]
        public int Likes { get; set; }

        [Display(Name = "Diskes")]
        public int DisLikes { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        public bool CanEdit { get; set; }
    }

    public class DeleteArticleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }
    }
}