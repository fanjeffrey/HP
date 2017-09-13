using HPCN.UnionOnline.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Site.ViewModels
{
    public class EnrollmentCreateViewModel
    {
        [Key]
        [Required]
        [StringLength(200)]
        [Display(Name = "活动名称", Prompt = "12月份旅游报名")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "报名开始时间")]
        public DateTime BeginTime { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "报名结束时间")]
        public DateTime EndTime { get; set; } = DateTime.Now.AddDays(14);

        [StringLength(2000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "详情")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "最大可参与人数")]
        public int MaxCountOfEnrollees { get; set; } = 50;

        [Required]
        [Display(Name = "不允许代报名？")]
        public bool SelfEnrollmentOnly { get; set; } = true;
    }

    public class EnrollmentSearchViewModel
    {
        [Display(Prompt = "type keyword here")]
        public string Keyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int Count { get; set; }
        public IList<Enrollment> Enrollments { get; set; }

        public bool HasPrevPage { get { return PageIndex > 1; } }
        public bool HasNextPage { get { return PageIndex < TotalPages; } }
        public int TotalPages { get { return (int)Math.Ceiling(Count / (double)PageSize); } }
    }

    public class EnrollmentAddFieldViewModel
    {
        public Enrollment Enrollment { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "字段名")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "页面显示名称")]
        public string DisplayName { get; set; }

        [StringLength(200)]
        [Display(Name = "字段说明")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "是否必填字段")]
        public bool IsRequired { get; set; }

        [StringLength(200)]
        [Display(Name = "必填字段提示消息")]
        public string RequiredMessage { get; set; }

        [Required]
        [Display(Name = "页面显示顺序", Description = "数字越小，页面排位越靠上。")]
        public int DisplayOrder { get; set; }

        [Required]
        [Display(Name = "字段值类型")]
        public FieldValueType TypeOfValue { get; set; }

        [Required]
        [Display(Name = "字段值选项模式", Description = "None 是指此字段不提供选项输入。Single 是指用户只能从选项中选择一个。Multi 是指用户可以从选项中选择多个。")]
        public FieldValueChoiceMode ChoiceMode { get; set; }

        public List<FieldValueChoice> ValueChoices { get; set; }
    }

    public class EnrollViewModel
    {
        public Enrollment Enrollment { get; set; }
        public Enrollee Enrollee { get; set; }
    }
}
