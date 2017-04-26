using HPCN.UnionOnline.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Site.ViewModels
{
    public class ProductAddViewModel
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "商品名称")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "积分")]
        public double PointsPayment { get; set; }

        [Required]
        [Display(Name = "自费金额")]
        public double SelfPayment { get; set; }

        [StringLength(500)]
        [Display(Name = "描述")]
        public string Description { get; set; }
        
        [Display(Name = "商品图片")]
        public IFormFile Picture { get; set; }
    }

    public class ProductEditViewModel : ProductAddViewModel
    {
        [Required]
        public Guid Id { get; set; }

        public string PictureFileName { get; set; }
    }

    public class ProductSearchViewModel
    {
        [Display(Prompt = "type keyword here")]
        public string Keyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int Count { get; set; }
        public IList<Product> Products { get; set; }

        public bool HasPrevPage { get { return PageIndex > 1; } }
        public bool HasNextPage { get { return PageIndex < TotalPages; } }
        public int TotalPages { get { return (int)Math.Ceiling(Count / (double)PageSize); } }
    }
}
