using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viewfinder.Models
{
    public class Photograph
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Author { get; set; }
        
        [ValidateNever]
        public string ImageUrl { get; set; }
        
        [Range(1,10000)]
        public double Price { get; set; }
        
        [Required]
        [ForeignKey("CategoryId")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ValidateNever]

        public Category Category { get; set; }
        
        [Required]
        [ForeignKey("CompositionId")]
        [Display(Name ="Composition")]
        public int CompositionId { get; set; }
        [ValidateNever]

        public Composition Composition { get; set; }


    }
}
