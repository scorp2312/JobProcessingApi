namespace InterviewGuide.Infrastructure.Data;

using InterviewGuide.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<QuestionEntity> InterviewQuestions { get; set; }

    public DbSet<CategoryEntity> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuestionEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property<int>("CategoryId");
            entity.Property(e => e.QuestionText).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Answer).HasMaxLength(10000);

            entity
                .HasOne(e => e.CategoryEntity)
                .WithMany()
                .HasForeignKey("CategoryId")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            entity.Navigation(e => e.CategoryEntity).AutoInclude();
        });

        modelBuilder.Entity<CategoryEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            entity.Property(e => e.CategoryName).IsRequired().HasMaxLength(500);
        });
    }
}