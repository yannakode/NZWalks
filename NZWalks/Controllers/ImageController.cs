using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repository.Interface;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly IImageRepository _imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<ActionResult> Upload([FromForm] ImageRequestUploadDTO requestImage)
        {
            ValidateFileUpload(requestImage);

            if(ModelState.IsValid)
            {
                var ImageDomainModel = new Image
                {
                    File = requestImage.File,
                    FileExtension = Path.GetExtension(requestImage.File.FileName),
                    FileSizeInBytes = requestImage.File.Length,
                    FileName = requestImage.FileName,
                    FileDescription = requestImage.FileDescription

                };

                await _imageRepository.Upload(ImageDomainModel);

                return Ok(ImageDomainModel);
            }
            return BadRequest();
        }

        private void ValidateFileUpload(ImageRequestUploadDTO requestImage)
        {
            var allowedExtensions = new string[]{".jpg", ".png", ".jpeg" };

            if(!allowedExtensions.Contains(requestImage.File.FileName))
            {
                ModelState.AddModelError("file", "unsuported file extension");
            }

            if(requestImage.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MG, upload a smaller one");
            }

        }
    }
}
