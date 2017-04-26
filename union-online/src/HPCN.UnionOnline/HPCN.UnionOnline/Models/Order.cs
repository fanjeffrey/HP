using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Models
{
    public class Order : Entity
    {
        [Required]
        public Employee Employee { get; set; }

        [Required]
        public Activity Activity { get; set; }

        [Required]
        public double PointsAmount { get; set; }

        [Required]
        public double MoneyAmount { get; set; }

        public ICollection<OrderDetail> Details { get; set; }
    }
}
