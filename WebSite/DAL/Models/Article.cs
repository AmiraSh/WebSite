using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.DAL.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
        public string UserId { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}