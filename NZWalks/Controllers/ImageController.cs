using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repository.Interface;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO uploadRequest)
        {
            ValidateFileUpload(uploadRequest);

            if(ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = uploadRequest.File,
                    FileDescription = uploadRequest.FileDescription,
                    FileExtension = Path.GetExtension(uploadRequest.File.FileName),
                    FileSizeInBytes = uploadRequest.File.Length
                };

                _imageRepository.UploadImage(imageDomainModel);

                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDTO uploadRequest)
        {
            var allowedExtension = new string[] {".jpg", ".png", ".jpeg"};
            var extensionFile = Path.GetExtension(uploadRequest.File.FileName);

            if (uploadRequest.File != null && !allowedExtension.Contains(extensionFile))
            {
                ModelState.AddModelError("file", "Extension file are not allowed. Please, upload other one");
            }
            if(uploadRequest.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "Please upload a file smaller than 10MG");
            }
        }
    }
}
