using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlogMVC.Models
{
    [Table("Reports")]
    public class Report
    {
        public int Id { get; set; }

        public int ReportCategoryId { get; set; }
        public int ArticleId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        public bool? IsInvestigated { get; set; }

        public DateTime? ReportTime { get; set; }

        public virtual Article Article { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ReportCategory ReportCategory { get; set; }


    }
}