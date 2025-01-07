using Microsoft.EntityFrameworkCore;
using AMLSystem.DAL.Models;

namespace AMLSystem.DAL;

public class AmlContext : DbContext
{
    public AmlContext(DbContextOptions<AmlContext> options) : base(options) { }

    public DbSet<MediaItem> MediaItems { get; set; }
    public DbSet<BorrowedMediaItem> BorrowedMediaItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MediaItem>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Name).IsRequired().HasMaxLength(255);
            entity.Property(m => m.Author).IsRequired().HasMaxLength(255);
            entity.Property(m => m.BookCode).IsRequired().HasMaxLength(50);
            entity.Property(m => m.Genre).HasConversion<int>();
            entity.Property(m => m.IsBorrowed).IsRequired();
            entity.Property(m => m.Description).HasMaxLength(1000);
            entity.Property(m => m.ImageUrl).HasMaxLength(500);
            entity.Property(m => m.IssueDate).HasColumnType("datetime");
            entity.Property(m => m.ReturnDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<BorrowedMediaItem>(entity =>
        {
            entity.HasOne(b => b.MediaItem)
                .WithOne()
                .HasForeignKey<BorrowedMediaItem>(b => b.MediaItemId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(b => b.BorrowedDate).HasColumnType("datetime");
            entity.Property(b => b.ReturnedDate).HasColumnType("datetime");
        });
    }
}