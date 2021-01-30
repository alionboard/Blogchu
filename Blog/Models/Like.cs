using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Like
    {
        public int Id { get; set; }

        [Required]
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}