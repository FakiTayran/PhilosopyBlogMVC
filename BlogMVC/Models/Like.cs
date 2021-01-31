using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlogMVC.Models
{
    public class Like
    {
        public int Id { get; set; }

        [Required]
        public int ArticleId { get; set; }

        public bool? Islike { get; set; }

        public virtual Article Article { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}