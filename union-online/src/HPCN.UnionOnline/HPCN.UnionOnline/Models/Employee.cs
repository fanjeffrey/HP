using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPCN.UnionOnline.Models
{
    public class Employee : LoggableEntity
    {
        [Key, ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string No { get; set; }

        [Required]
        [StringLength(200)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string ChineseName { get; set; }

        [Required]
        [StringLength(50)]
        public string DisplayName { get; set; }

        [Required]
        public DateTime OnboardDate { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [StringLength(200)]
        public string ManagerEmail { get; set; }

        [StringLength(200)]
        public string TeamAdminAssistant { get; set; }

        [StringLength(18)]
        public string IdCardNo { get; set; }

        [StringLength(50)]
        public string BaseCity { get; set; }

        [StringLength(50)]
        public string WorkCity { get; set; }

        [StringLength(50)]
        public string CostCenter { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public EmployeeType EmployeeType { get; set; }

        [Required]
        public EmployeeState EmployeeStatus { get; set; }

        [Required]
        public User User { get; set; }
    }

    public enum Gender
    {
        F = 0,
        M = 1
    }

    public enum EmployeeType
    {
        ETW = 0,
        RE = 1,
    }

    public enum EmployeeState
    {
        Inactive = 0,
        Active = 1
    }
}
