using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.Model
{
    public class Carts
    {
        [Required]
        [Key]
        public int ItemID { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        [MinLength(6)]
        public string PhoneNumber { get; set; }
    }
}
