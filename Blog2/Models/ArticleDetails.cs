using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog2.Models
{
    public class ArticleDetails
    {
        public Article Article { get; set; }
        public List<Comment> Comments { get; set; }
    }
}