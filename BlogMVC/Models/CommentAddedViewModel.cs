using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMVC.Models
{
    public class CommentAddedViewModel
    {
        public string CommentSuccess { get; set; }

        public int CommentId { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public string CommentTime { get; set; }
    }
}