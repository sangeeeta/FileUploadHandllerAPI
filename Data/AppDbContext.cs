using Microsoft.EntityFrameworkCore;
using UploadFiles.Models;

namespace UploadFiles.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UploadedFile> UploadedFiles { get; set; }
    }
}
