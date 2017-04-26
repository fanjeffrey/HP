using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Models
{
    public class OrderDetail : Entity
    {
        [Required]
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double PointsPaymentAmount { get; set; }

        [Required]
        public double SelfPaymentAmount { get; set; }

        [Required]
        public Order Order { get; set; }
    }
}
