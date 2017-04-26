using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Site.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "员工号")]
        public string EmployeeNo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "员工号")]
        public string EmployeeNo { get; set; }
    }
}
