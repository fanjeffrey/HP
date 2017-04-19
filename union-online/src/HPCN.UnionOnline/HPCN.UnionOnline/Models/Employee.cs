using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Models
{
    public class Employee : Entity
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(20)]
        [Display(Name = "Employee No")]
        public string EmployeeNo { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        public double Credit { get; set; }

        public ApplicationUser User { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
