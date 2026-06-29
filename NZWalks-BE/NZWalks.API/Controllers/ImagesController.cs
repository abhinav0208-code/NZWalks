using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ImagesController:ControllerBase
    {
        private readonly IImageRepository imageRepository;

        
        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateFileUpload(imageUploadRequestDto);

            if (ModelState.IsValid)
            {

                Image image = new Image()
                {
                    File = imageUploadRequestDto.File,
                    FileDescription = imageUploadRequestDto.FileDescription,
                    FileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName),
                    FileName = imageUploadRequestDto.FileName,
                    FileSizeInBytes = imageUploadRequestDto.File.Length
                };

                await imageRepository.Upload(image);

                return Ok(image);

            }
            return BadRequest(ModelState);


        }

        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            List<string> allowedExtenstion = new List<string>() { ".png", ".jpeg", ".jpg" };

            if (!allowedExtenstion.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName))){

                ModelState.AddModelError("file", "Unsupported file extension");

            }
            if(imageUploadRequestDto.File.Length> 10485760)
            {
                ModelState.AddModelError("file", "File size more that 10 mb, please upload a smaller size file");
            }
        }
    }
}
