using Microsoft.AspNetCore.Mvc;
using UploadFiles.Interface;

namespace UploadFiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IFileService _fileService;

        public ImageController(IFileService fileService)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        [HttpPost("uploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty.");

            try
            {
                string result = await _fileService.UploadFileAsync(file, "images", new string[] { ".png", ".jpg", ".jpeg" });
                return Ok(new { message = "Image uploaded successfully!", result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
