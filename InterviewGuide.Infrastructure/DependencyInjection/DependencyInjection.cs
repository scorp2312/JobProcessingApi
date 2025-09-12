namespace InterviewGuide.Infrastructure.DependencyInjection
{
    using InterviewGuide.Domain.Entities;
    using InterviewGuide.Domain.Interfaces;
    using InterviewGuide.Infrastructure.Data;
    using InterviewGuide.Infrastructure.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IRepository<CategoryEntity, int>, RepositoryBase<CategoryEntity, int>>();
            services.AddScoped<IRepository<CommentEntity, Guid>, RepositoryBase<CommentEntity, Guid>>();
            services.AddScoped<IRepository<RoleEntity, int>, RepositoryBase<RoleEntity, int>>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}