using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Models
{
    /// <summary>
    /// This class is for points exchange activity.
    /// </summary>
    public class Activity : AbstractEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public DateTime BeginTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public ActivityState Status { get; set; } = ActivityState.Pending;

        [StringLength(1000)]
        public string Description { get; set; }

        public ICollection<ActivityProduct> ActivityProducts { get; set; }
    }

    public enum ActivityState
    {
        Pending = 0,
        Active = 1,
        Closed = 2
    }
}
