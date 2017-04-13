using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog2.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Creator { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public int RatingGood { get; set; }
        public int RatingBad { get; set; }
    }
}