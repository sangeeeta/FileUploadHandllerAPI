using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UploadFiles.Models
{
    [Table("uploaded_files")]
    public class UploadedFile
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("file_name")]
        public string FileName { get; set; } = string.Empty;

        [Required]
        [Column("file_type")]
        public string FileType { get; set; } = string.Empty;

        [Required]
        [Column("content")]  // ✅ lowercase matches table
        public byte[] Content { get; set; } = Array.Empty<byte>();

        [Column("uploaded_at")]
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
