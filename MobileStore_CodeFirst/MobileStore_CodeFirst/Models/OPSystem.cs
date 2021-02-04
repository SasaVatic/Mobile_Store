using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MobileStore_CodeFirst.Models
{
    public class OPSystem
    {
        public int OPSystemId { get; set; }

        
        [Required]
        [Display(Name = "Operating System")]
        public string OpSystemName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<MobileModel> Models { get; set; }
    }
}