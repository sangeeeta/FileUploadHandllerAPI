using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UploadFiles.Interface;

namespace UploadFiles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExcelController : ControllerBase
    {
        private readonly IFileService _fileService;
        private static readonly string[] allowedExtensions = new string[] { ".xls", ".xlsx" };

        public ExcelController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("uploadExcel")]
        public async Task<IActionResult> UploadPdf(IFormFile file)
        {
            try
            {
                var fileUrl = await _fileService.UploadFileAsync(
                    file,
                    "excel",
                    allowedExtensions);

                return Ok(new { message = "PDF uploaded successfully!", fileUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

