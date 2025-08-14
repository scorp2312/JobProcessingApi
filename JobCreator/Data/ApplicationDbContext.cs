namespace JobCreator.Data;

using JobCreator.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Job> Jobs { get; set; }

    public DbSet<InterviewQuestion> InterviewQuestions { get; set; }

    public DbSet<InterviewQuestionCategory> Categories { get; set; }

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

        modelBuilder.Entity<InterviewQuestion>(entity =>
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

        modelBuilder.Entity<InterviewQuestionCategory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Category).IsRequired().HasMaxLength(500);
        });
    }
}