using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Models
{
    public class Order : Entity
    {
        [Required]
        public Employee Employee { get; set; }

        [Required]
        public Activity Activity { get; set; }

        [Required]
        [Display(Name = "Bonus Point Amount")]
        public double BonusPointAmount { get; set; }

        [Required]
        [Display(Name = "Money Amount")]
        public double MoneyAmount { get; set; }

        public ICollection<OrderDetail> Details { get; set; }
    }
}
