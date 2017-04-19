using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Models
{
    public class OrderDetail : Entity
    {
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Order Order { get; set; }
    }
}
