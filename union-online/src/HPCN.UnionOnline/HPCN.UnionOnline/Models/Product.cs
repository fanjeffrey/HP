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
        [Display(Name = "Default Bonus Point Price")]
        public double DefaultBonusPointPrice { get; set; }

        [Required]
        [Display(Name = "Default Money Price")]
        public double DefaultMoneyPrice { get; set; }
    }
}
