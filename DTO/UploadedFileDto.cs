namespace UploadFiles.DTO
{
    public class UploadedFileDto
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; }
    }
}
