using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Models
{
    public class Product : Entity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public double PointsPayment { get; set; }

        [Required]
        public double SelfPayment { get; set; }

        [StringLength(200)]
        public string PictureFileName { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public ICollection<ActivityProduct> InvolvedActivities { get; set; }
        public ICollection<OrderDetail> InvolvedOrders { get; set; }
    }
}
