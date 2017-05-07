using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Models
{
    public class Order : AbstractEntity
    {
        [Required]
        public double PointsAmount { get; set; }

        [Required]
        public double MoneyAmount { get; set; }

        [Required]
        public OrderState Status { get; set; } = OrderState.Created;

        [Required]
        public User User { get; set; }

        public ICollection<OrderDetail> Details { get; set; }
    }

    public enum OrderState
    {
        Created = 0,
        Cancelled = 1, // cancelled by buyer
        Aborted = 2, // aborted by seller
        Completed = 9
    }
}
