using Microsoft.AspNetCore.Hosting;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Repository.Interface;

namespace NZWalks.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ApplicationDbContext _DbContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor contextAccessor, ApplicationDbContext DbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _contextAccessor = contextAccessor;
            _DbContext = DbContext;
        }

        public async Task<Image> UploadImage(Image image)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}{_contextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            await _DbContext.images.AddAsync(image);
            await _DbContext.SaveChangesAsync();

            return image;
        }
    }
}
