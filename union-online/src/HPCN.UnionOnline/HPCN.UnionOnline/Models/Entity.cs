using System;
using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Models
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime? CreatedTime { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedTime { get; set; }

        [StringLength(200)]
        public string UpdatedBy { get; set; }

        public DateTime? ConcurrencyTimestamp { get; set; } = DateTime.Now;
    }
}
