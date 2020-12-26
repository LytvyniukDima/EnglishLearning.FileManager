using System.Collections.Generic;
using System.Text.Json;
using EnglishLearning.FileManager.Persistence.Configuration;
using EnglishLearning.FileManager.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.FileManager.Persistence
{
    internal class FileManagerContext : DbContext
    {
        private readonly DatabaseConfiguration _databaseConfiguration;
        
        public FileManagerContext(DbContextOptions options, DatabaseConfiguration databaseConfiguration) 
            : base(options)
        {
            _databaseConfiguration = databaseConfiguration;
        }
        
        public DbSet<FileEntity> Files { get; set; }
        
        public DbSet<FolderEntity> Folders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileEntity>()
                .ToTable("Files");

            modelBuilder.Entity<FileEntity>()
                .Property(x => x.Metadata)
                .HasConversion(
                    j => JsonSerializer.Serialize(j, new JsonSerializerOptions() {IgnoreNullValues = true }),
                    j => JsonSerializer.Deserialize<Dictionary<string, string>>(j, new JsonSerializerOptions() { IgnoreNullValues = true }));

            modelBuilder.Entity<FolderEntity>()
                .ToTable("Folders");
        }
    }
}