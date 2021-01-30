using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    [Table("Categories")]
    public class Category
    {
        public Category()
        {
            this.Articles = new HashSet<Article>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string CategoryName { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}