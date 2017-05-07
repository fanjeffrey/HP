using System;
using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Models
{
    public class OrderDetail : AbstractEntity
    {
        [Required]
        public ActivityProduct ActivityProduct { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double PointsPayment { get; set; } // snapshot of when the order is created

        [Required]
        public double SelfPayment { get; set; } // snapshot of when the order is created

        [Required]
        public double PointsPaymentAmount { get; set; }

        [Required]
        public double SelfPaymentAmount { get; set; }

        #region keep these redundant fields as memo
        // when the activity or the product get changed in their original table, for example, the Name changed.
        // information kept here will be a snapshot of when they get added into this order.
        public Guid ActivityId { get; set; }
        public string AcitivityName { get; set; }
        public Guid ActivityProductId { get; set; }
        public string ProductName { get; set; }
        #endregion

        [Required]
        public Order Order { get; set; }
    }
}
