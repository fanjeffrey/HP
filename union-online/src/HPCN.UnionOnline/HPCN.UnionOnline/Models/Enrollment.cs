using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPCN.UnionOnline.Models
{
    public class Enrollment : AbstractEntity
    {
        [Required]
        public EnrollmentActivity EnrollmentActivity { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public ICollection<Enrollee> Enrollee { get; set; }

        public ICollection<EnrollmentInput> PropertyInputs { get; set; }
    }

    public class Enrollee : AbstractEntity
    {
        [Required]
        [StringLength(200)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }
    }

    public class EnrollmentInput : AbstractEntity
    {
        [Required]
        public Enrollment Enrollment { get; set; }

        [Required]
        public PropertyEntry Property { get; set; }

        [Required]
        [StringLength(4000)]
        public string Input { get; set; }
    }

    public class EnrollmentActivity : AbstractEntity
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
    }

    public class EnrollmentActivityProperty : LoggableEntity
    {
        [Key, ForeignKey("PropertyEntry")]
        public Guid PropertyEntryId { get; set; }

        [Required]
        public EnrollmentActivity EnrollmentActivity { get; set; }
    }
}
