namespace InterviewGuide.Infrastructure.Data;

using InterviewGuide.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<QuestionEntity> InterviewQuestions { get; set; }

    public DbSet<CategoryEntity> Categories { get; set; }

    public DbSet<CommentEntity> Comments { get; set; }

    public DbSet<UserEntity> Users { get; set; }

    public DbSet<RoleEntity> Roles { get; set; }

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

            entity
                .HasMany(e => e.Comments)
                .WithOne(c => c.Question)
                .HasForeignKey(c => c.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Navigation(e => e.CategoryEntity).AutoInclude();
            entity.Navigation(e => e.Comments).AutoInclude();
        });

        modelBuilder.Entity<CategoryEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            entity.Property(e => e.CategoryName).IsRequired().HasMaxLength(500);
        });

        modelBuilder.Entity<CommentEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Author).IsRequired();
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.Created).IsRequired();
        });

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Login).IsRequired().HasMaxLength(25);
            entity.Property(e => e.PasswordHash).IsRequired();

            entity.HasMany(e => e.Roles).WithMany();

            entity.Navigation(e => e.Roles).AutoInclude();
        });

        modelBuilder.Entity<RoleEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
        });
    }
}