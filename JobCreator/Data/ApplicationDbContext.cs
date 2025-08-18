namespace JobCreator.Data;

using JobCreator.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Job> Jobs { get; set; }

    public DbSet<InQuestion> InterviewQuestions { get; set; }

    public DbSet<InQCategory> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Result).HasMaxLength(1000);
            entity.HasIndex(e => e.Status);
        });

        modelBuilder.Entity<InQuestion>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Question).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Answer).HasMaxLength(10000);

            entity
                .HasOne(e => e.Category)
                .WithMany()
                .HasForeignKey("CategoryId")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<InQCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);
            entity.Property(e => e.CategoryName).IsRequired().HasMaxLength(500);
        });
    }
}