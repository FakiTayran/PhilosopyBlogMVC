using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlogMVC.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        public int Rate { get; set; }

        public DateTime CommentTime { get; set; }
        public virtual Article Article { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}