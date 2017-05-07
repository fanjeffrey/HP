using HPCN.UnionOnline.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Site.ViewModels
{
    public class EmployeeSearchViewModel
    {
        [Display(Prompt = "type keyword here")]
        public string Keyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int Count { get; set; }
        public IList<Employee> Employees { get; set; }

        public bool HasPrevPage { get { return PageIndex > 1; } }
        public bool HasNextPage { get { return PageIndex < TotalPages; } }
        public int TotalPages { get { return (int)Math.Ceiling(Count / (double)PageSize); } }
    }

    public class EmployeeAddViewModel
    {
        [Required]
        [StringLength(50)]
        public string No { get; set; }

        [Required]
        [StringLength(200)]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string ChineseName { get; set; }

        [Required]
        [StringLength(50)]
        public string DisplayName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OnboardDate { get; set; }

        [Required]
        [StringLength(18)]
        [MinLength(15)]
        public string IdCardNo { get; set; }

        [Required]
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string BaseCity { get; set; }

        [Required]
        [StringLength(50)]
        public string WorkCity { get; set; }

        [Required]
        [StringLength(50)]
        public string CostCenter { get; set; }

        [Required]
        public EmployeeType EmployeeType { get; set; }
    }

    public class EmployeeEditViewModel : EmployeeAddViewModel
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
