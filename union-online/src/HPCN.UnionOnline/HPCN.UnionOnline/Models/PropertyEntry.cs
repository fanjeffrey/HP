using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Models
{
    public class PropertyEntry : AbstractEntity
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
        public PropertyValueType TypeOfValue { get; set; }
        
        public PropertyValueChoiceMode? ChoiceMode { get; set; }

        public ICollection<PropertyValueChoice> ValueChoices { get; set; }
    }

    public class PropertyValueChoice : AbstractEntity
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
        public PropertyEntry Property { get; set; }
    }

    public enum PropertyValueType
    {
        String = 0,
        Boolean = 1,
        Char = 2,
        Guid = 3,
        DateTime = 4,

        Int = 10,
        Long = 11,

        Float = 20,
        Double = 21,

        Decimal = 30,
    }

    public enum PropertyValueChoiceMode
    {
        Single = 1,
        Multi = 2
    }
}
