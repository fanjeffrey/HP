using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Models
{
    public class Product : Entity
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public double Credit { get; set; }

        [Required]
        public double Money { get; set; } = 0d;

        public Activity Activity { get; set; }
    }
}
