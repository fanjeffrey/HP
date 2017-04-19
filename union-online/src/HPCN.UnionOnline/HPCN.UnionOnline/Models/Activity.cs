using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Models
{
    public class Activity : Entity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Begin Time")]
        public DateTime BeginTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        [Required]
        public ActivityState Status { get; set; } = ActivityState.Pending;

        public ICollection<ActivityProduct> ActivityProducts { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

    public enum ActivityState
    {
        Pending = 0,
        Open = 1,
        Closed = 2
    }
}
