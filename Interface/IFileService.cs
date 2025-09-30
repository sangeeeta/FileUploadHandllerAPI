using UploadFiles.DTO;
using UploadFiles.Models;

namespace UploadFiles.Interface
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName, string[] allowedExtensions);
        Task<List<UploadedFileDto>> GetAllFilesAsync();  // For UI listing
        Task<UploadedFile> GetFileByIdAsync(int id);      // For downloading
    }
}
