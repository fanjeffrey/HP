using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Models
{
    public class ActivityProduct : Entity
    {
        [Required]
        public Activity Activity { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        [Display(Name = "Bonus Point Price")]
        public double BonusPointPrice { get; set; }

        [Required]
        [Display(Name = "Money Price")]
        public double MoneyPrice { get; set; }
    }
}
