using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viewfinder.Models
{
    public class ShoppingCart
    {
        public Photograph Photograph { get; set; }

        [Range(1, 1, ErrorMessage = "You can buy a photo only 1 time")]
        public int Count { get; set; }
    }
}
