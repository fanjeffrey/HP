using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Models
{
    public class ActivityProduct : Entity
    {
        [Required]
        public Activity Activity { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        public double PointsPayment { get; set; }

        [Required]
        public double SelfPayment { get; set; }
    }
}
