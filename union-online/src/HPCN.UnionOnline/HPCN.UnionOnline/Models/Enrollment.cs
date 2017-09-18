using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Models
{
    public class Enrollment : AbstractEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public DateTime BeginTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public ActivityState Status { get; set; } = ActivityState.Pending;

        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        public int MaxCountOfEnrolles { get; set; }

        [Required]
        public bool SelfEnrollmentOnly { get; set; }

        public List<FieldEntry> ExtraFormFields { get; set; }
    }

    public class Enrolling : AbstractEntity
    {
        [Required]
        public Enrollment Enrollment { get; set; }

        [Required]
        public Enrollee Enrollee { get; set; }

        [Required]
        public User User { get; set; }

        public List<FieldInput> FieldInputs { get; set; }
    }

    public class Enrollee : AbstractEntity
    {
        [Required]
        [StringLength(50)]
        public string EmployeeNo { get; set; }

        [Required]
        [StringLength(200)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }
    }

    public class FieldInput : AbstractEntity
    {
        [Required]
        public Guid FieldEntryId { get; set; }

        [Required]
        [StringLength(4000)]
        public string Input { get; set; }

        [Required]
        public Enrolling Enrolling { get; set; }
    }

    public class FieldEntry : AbstractEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string DisplayName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public bool IsRequired { get; set; }

        [StringLength(200)]
        public string RequiredMessage { get; set; }

        [Required]
        public int DisplayOrder { get; set; }

        [Required]
        public FieldValueType TypeOfValue { get; set; }

        [Required]
        public FieldValueChoiceMode ChoiceMode { get; set; }

        public List<FieldValueChoice> ValueChoices { get; set; }

        [Required]
        public Enrollment Enrollment { get; set; }
    }

    public class FieldValueChoice : AbstractEntity
    {
        [Required]
        [StringLength(4000)]
        public string Value { get; set; }

        [Required]
        [StringLength(200)]
        public string DisplayText { get; set; }

        [Required]
        public int DisplayOrder { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public FieldEntry Field { get; set; }
    }

    public enum FieldValueType
    {
        String = 0,
        Boolean = 1,
        Char = 2,
        Guid = 3,
        DateTime = 4,

        Int = 10,
        LongInt = 11,

        Float = 20,
        Double = 21,

        Decimal = 30,
    }

    public enum FieldValueChoiceMode
    {
        None = 0,
        Single = 1,
        Multi = 2
    }
}
