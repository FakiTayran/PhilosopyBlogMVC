using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMVC.Models
{
    [Table("Articles")]
    public class Article
    {
        public Article()
        {
            Categories = new HashSet<Category>();
            Comments = new HashSet<Comment>();
        }
        public int Id { get; set; }


        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public string PhotoPath { get; set; }

        [Required]
        [MinLength(100)]
        [MaxLength(300)]
        public string Summary { get; set; }

        [NotMapped]
        public HttpPostedFileBase File { get; set; }

        [Required]
        [StringLength(500000,MinimumLength = 500, ErrorMessage = "{0} must have min length of {2} and max Length of {1}")]
        [AllowHtml]
        public string Content { get; set; }

        public DateTime? ArticleTime { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Report> Reports { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

    }
}