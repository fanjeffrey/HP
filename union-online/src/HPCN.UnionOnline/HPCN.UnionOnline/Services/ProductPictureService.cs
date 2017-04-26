using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public class ProductPictureService : IProductPictureService
    {
        public bool ValidateContentType(string contentType)
        {
            return
                "image/png".Equals(contentType, StringComparison.OrdinalIgnoreCase) ||
                "image/jpeg".Equals(contentType, StringComparison.OrdinalIgnoreCase);
        }

        public async Task<string> Save(IFormFile picture, ProductPictureOptions pictureOptions, Guid productId)
        {
            if (picture == null || picture.Length == 0)
            {
                return pictureOptions.NoPictureFileName;
            }

            var pictureFileName = GenerateFileName(picture.ContentType, productId);
            var targetPath = Path.Combine(pictureOptions.StoreFolder, pictureFileName);

            using (var stream = new FileStream(targetPath, FileMode.Create))
            {
                await picture.CopyToAsync(stream);
                return pictureFileName;
            }
        }


        private static string GenerateFileName(string fileType, Guid productId)
        {
            fileType = fileType ?? string.Empty;
            fileType = fileType.ToLower();

            if (fileType.Contains("png"))
            {
                return $"{productId}.png";
            }
            else if (fileType.Contains("jpeg"))
            {
                return $"{productId}.jpg";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
