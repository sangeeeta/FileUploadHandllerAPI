using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UploadFiles.Interface;

namespace UploadFiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        // GET: api/Files
        [HttpGet]
        public async Task<IActionResult> GetAllFiles()
        {
            var files = await _fileService.GetAllFilesAsync();
            return Ok(files);
        }

        // GET: api/Files/Download/5
        [HttpGet("Download/{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var file = await _fileService.GetFileByIdAsync(id);
            if (file == null) return NotFound();

            return File(file.Content, "application/octet-stream", file.FileName);
        }
    }
}
