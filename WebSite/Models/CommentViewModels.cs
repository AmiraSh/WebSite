using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class DisplayCommentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Text")]
        public string Text { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "Likes")]
        public int Likes { get; set; }

        [Display(Name = "Dislikes")]
        public int DisLikes { get; set; }

        public bool CanDelete { get; set; }
    }

    public class CreateCommentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Text is required")]
        [Display(Name = "Text")]
        public string Text { get; set; }
    }
}