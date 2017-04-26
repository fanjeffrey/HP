using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HPCN.UnionOnline.Services
{
    public interface IProductPictureService
    {
        bool ValidateContentType(string contentType);
        Task<string> Save(IFormFile picture, ProductPictureOptions pictureOptions, Guid productId);
    }

    public class ProductPictureOptions
    {
        public string StoreFolder { get; set; }
        public string NoPictureFileName { get; set; }
    }
}
