using Microsoft.EntityFrameworkCore;
using UploadFiles.Data;
using UploadFiles.DTO;
using UploadFiles.Interface;
using UploadFiles.Models;

namespace UploadFiles.Services
{
    public class FileService : IFileService
    {
        private readonly AppDbContext _context;

        public FileService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folderName, string[] allowedExtensions)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty.");

            var ext = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(ext))
                throw new ArgumentException($"Invalid file type. Allowed: {string.Join(", ", allowedExtensions)}");

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var uploadedFile = new UploadedFile
            {
                FileName = file.FileName,
                FileType = ext,
                Content = ms.ToArray(),
                UploadedAt = DateTime.UtcNow
            };

            _context.UploadedFiles.Add(uploadedFile);
            await _context.SaveChangesAsync();

            return $"File saved with ID: {uploadedFile.Id}";
        }

        public async Task<List<UploadedFileDto>> GetAllFilesAsync()
        {
            return await _context.UploadedFiles
                .Select(f => new UploadedFileDto
                {
                    Id = f.Id,
                    FileName = f.FileName,
                    FileType = f.FileType,
                    UploadedAt = f.UploadedAt
                })
                .ToListAsync();
        }

        public async Task<UploadedFile> GetFileByIdAsync(int id)
        {
            return await _context.UploadedFiles.FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
