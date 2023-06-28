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
            if(!ValidateBySignature(uploadRequest.File))
            {
                ModelState.AddModelError("file", "Extension file are not allowed. Please, upload other one");
                return BadRequest(ModelState);
            }

            if(uploadRequest.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "Please upload a file smaller than 10MG");
                return BadRequest(ModelState);
            }

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

        private bool ValidateBySignature(IFormFile file)
        {
            Dictionary<string, List<byte[]>> _fileSignature = new Dictionary<string, List<byte[]>>
            {
                {".jpg", new List<byte[]>
                {
                     new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                     new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                     new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 }
                }
                },

                {".png", new List<byte[]>
                {
                    new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }
                }
                },

                {".jpeg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 }
                }
                }
            };

            foreach(string exts in _fileSignature.Keys)
            {
                using (var reader = new BinaryReader(file.OpenReadStream()))
                {
                    var signatures = _fileSignature[exts];
                    var readerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

                    if(signatures.Any(signature => readerBytes.Take(signature.Length).SequenceEqual(signature)))
                    {
                        return true;
                    }     
                }
            }
            return false;
        }
    }
}
