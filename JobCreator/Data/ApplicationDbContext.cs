namespace JobCreator.Data;

using JobCreator.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Job> Jobs { get; set; }

    public DbSet<Question> InterviewQuestions { get; set; }

    public DbSet<Category> Categories { get; set; }

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

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.QuestionText).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Answer).HasMaxLength(10000);

            entity
                .HasOne(e => e.Category)
                .WithMany()
                .HasForeignKey("Id")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CategoryName).IsRequired().HasMaxLength(500);
        });
    }
}