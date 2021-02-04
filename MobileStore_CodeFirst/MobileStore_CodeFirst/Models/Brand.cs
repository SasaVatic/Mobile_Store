using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MobileStore_CodeFirst.Models
{
    public class Brand
    {
        public int BrandId { get; set; }

        
        [Required]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        public virtual ICollection<MobileModel> Models { get; set; }
    }
}