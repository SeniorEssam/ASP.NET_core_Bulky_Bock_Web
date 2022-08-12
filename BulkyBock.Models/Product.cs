using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBock.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(1,10000)]
        public double ListPrice { get; set; }
        [Required]
        //[Display(Name = "Price 1-50")]
        [Range(1,10000)]
        public double Price { get; set; }
        [Required]
        //[Display(Name = "Price 50-100")]
        [Range(1, 10000)]
        public double Price50 { get; set; }
        [Required]
        //[Display(Name = "List Price")]
        [Range(1, 10000)]
        public double Price100 { get; set; }
        [ValidateNever]
        public string ImgUrl { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [Required]
        public int CoverTypeId { get; set; }
        [ValidateNever]
        public CoverType CoverType { get; set; }
    }
}
