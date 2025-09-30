using Microsoft.AspNetCore.Http;
using UploadFiles.Data;
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
                UploadedAt = DateTime.UtcNow  // ✅ Use UTC
            };

            _context.UploadedFiles.Add(uploadedFile); // DbSet matches class
            await _context.SaveChangesAsync();

            return $"File saved with ID: {uploadedFile.Id}";
        }
    }
}
