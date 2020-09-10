using Todo.API.DbContexts;
using Todo.API.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace Todo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["DB_CONNECTION"];
            services.AddControllers(setupAction => { setupAction.ReturnHttpNotAcceptable = true; })
                .AddNewtonsoftJson(setupAction =>
                {
                    setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddXmlDataContractSerializerFormatters()
                .ConfigureApiBehaviorOptions(setupAction =>
                {
                    setupAction.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetailsFactory = context.HttpContext.RequestServices
                            .GetRequiredService<ProblemDetailsFactory>();
                        var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                            context.HttpContext, context.ModelState);

                        problemDetails.Detail = "See the errors field for details.";
                        problemDetails.Instance = context.HttpContext.Request.Path;

                        var actionExecutingContext = context as ActionExecutingContext;

                        if (context.ModelState.ErrorCount > 0 && actionExecutingContext?.ActionArguments.Count ==
                            context.ActionDescriptor.Parameters.Count)
                        {
                            problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                            problemDetails.Title = "One or more validation errors occurred";

                            return new UnprocessableEntityObjectResult(problemDetails)
                            {
                                ContentTypes = {"application/problem+json"}
                            };
                        }

                        problemDetails.Status = StatusCodes.Status400BadRequest;
                        problemDetails.Title = "One or more errors on input occurred.";

                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = {"application/problem+json"}
                        };
                    };
                });

            services.AddScoped<ICourseLibraryRepository, CourseLibraryRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<CourseLibraryContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });   
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try Again");
                    });
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}