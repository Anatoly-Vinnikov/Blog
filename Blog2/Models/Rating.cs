using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog2.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string Creator { get; set; }
        public bool Like { get; set; }
        public bool Dislike { get; set; }
    }
}