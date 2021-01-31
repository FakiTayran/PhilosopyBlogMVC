using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMVC.Models
{
    public class AddCommentViewModel
    {
        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        public int ArticleId { get; set; }

    }
}