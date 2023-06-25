using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Repository.Interface;

namespace NZWalks.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _WebHostenvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _applicationDbContext;

        public ImageRepository(IWebHostEnvironment webHostenvironment, IHttpContextAccessor httpContextAccessor, ApplicationDbContext applicationDbContext)
        {
            _WebHostenvironment = webHostenvironment;
            _httpContextAccessor = httpContextAccessor;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(_WebHostenvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            await _applicationDbContext.images.AddAsync(image);
            await _applicationDbContext.SaveChangesAsync();

            return image;
        }
    }
}
