using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Models
{
    public class ActivityProduct : AbstractEntity
    {
        [Required]
        public Product Product { get; set; }

        [Required]
        public double PointsPayment { get; set; }

        [Required]
        public double SelfPayment { get; set; }

        [Required]
        public double Stock { get; set; } = -1; // -1 means infinite

        [Required]
        public Activity Activity { get; set; }

        public ICollection<CartProduct> CartProducts { get; set; }
    }
}
