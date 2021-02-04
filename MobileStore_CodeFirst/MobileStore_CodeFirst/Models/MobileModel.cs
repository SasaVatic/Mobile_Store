using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileStore_CodeFirst.Models
{
    public class MobileModel
    {
        public int MobileModelId { get; set; }

        
        [Required]
        [Display(Name = "Model Name")]
        public string ModelName { get; set; }

        [Required]
        [Display(Name = "Brand Name")]
        public int? BrandId { get; set; }

        public decimal? Price { get; set; }

        [Range(0,16)]
        public int? RAM { get; set; }

        [Required]
        [Display(Name = "Storage Capacity")]
        public int StorageCapacity { get; set; }


        [Display(Name = "Operating System")]
        public int? OPSystemId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual OPSystem OPSystem { get; set; }
    }
}