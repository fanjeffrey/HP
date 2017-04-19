using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Models
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Display(Name = "When created")]
        public DateTime CreatedTime { get; set; }

        [Required]
        [Display(Name = "When updated")]
        public DateTime UpdatedTime { get; set; }
    }
}
