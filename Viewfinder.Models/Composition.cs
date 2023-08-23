using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viewfinder.Models
{
    public class Composition
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Composition Name")]
        public string Name { get; set; }
    }
}
