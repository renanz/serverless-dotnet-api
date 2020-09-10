using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.API.DbContexts;
using Todo.API.Repositories;

namespace Todo.Core
{
    public static class Configurations
    {
        public static void ConfigureDependencies(IServiceCollection services)
        {
            services.AddScoped<ICourseLibraryRepository, CourseLibraryRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<CourseLibraryContext>(options =>
            {
                options.UseNpgsql("Host=localhost;Database=my_db;Username=postgres;Password=password");
            });   
        }
    }
}