using HPCN.UnionOnline.Models;
using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Site.ViewModels
{
    public class EnrollingViewModel
    {
        public Enrollment Enrollment { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "员工号")]
        public string EmployeeNo { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "电邮地址")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "手机")]
        public string PhoneNumber { get; set; }
    }
}
