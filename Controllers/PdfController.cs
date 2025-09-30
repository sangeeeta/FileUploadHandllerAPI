using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UploadFiles.Interface;

namespace UploadFiles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PdfController : ControllerBase
    {
        private readonly IFileService _fileService;
        public PdfController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPdf(IFormFile file)
        {
            try
            {
                var fileUrl = await _fileService.UploadFileAsync(
                    file,
                    "pdf",
                    new[] { ".pdf" });

                return Ok(new { message = "PDF uploaded successfully!", fileUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
