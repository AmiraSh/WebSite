using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.DAL.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
        public string UserId { get; set; }

        public virtual Article Article { get; set; }
    }
}