namespace UploadFiles.Interface
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName, string[] allowedExtensions);
    }
}
