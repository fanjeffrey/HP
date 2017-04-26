using HPCN.UnionOnline.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Site.ViewModels
{
    public class ActivityCreateViewModel
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BeginTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
    }

    public class ActivityEditViewModel : ActivityCreateViewModel
    {
        public Guid Id { get; set; }
    }

    public class ActivityProductsViewModel
    {
        public Activity Activity { get; set; }
        public IEnumerable<Product> ProdcutCatalog { get; set; }
    }

    public class ActivitySearchViewModel
    {
        [Display(Prompt = "type keyword here")]
        public string Keyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int Count { get; set; }
        public IList<Activity> Activities { get; set; }

        public bool HasPrevPage { get { return PageIndex > 1; } }
        public bool HasNextPage { get { return PageIndex < TotalPages; } }
        public int TotalPages { get { return (int)Math.Ceiling(Count / (double)PageSize); } }
    }
}
