using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebApi.Helpers;
using WebApi.Services;
using AutoMapper;

using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System;
using System.IO;
using DAL.Entities;
using FluentValidation.AspNetCore;

namespace WebApi
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
            services.AddCors();
            //services.AddDbContext<DataContext>(x => x.UseInMemoryDatabase("TestDb"));
            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(ValidatorActionFilter));
            }).AddFluentValidation(fvc => {
                fvc.RegisterValidatorsFromAssemblyContaining<Startup>();
                fvc.ImplicitlyValidateChildProperties = true;
            });

            services.AddAutoMapper();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            JWTAuthentication.Configure(services, appSettingsSection.Get<AppSettings>().Secret);

            // Register the Swagger generator, defining 1 or more Swagger documents
            AddSwaggerGen(services);

            //Register DB Context
            var dbConnectionString = appSettingsSection.Get<AppSettings>().DBConnectionString;
            services.AddDbContext<DataContext>(options => options.UseSqlServer(dbConnectionString));

            // configure DI for application services
            services.AddScoped<OperatingUser, OperatingUser>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserTypeService, UserTypeService>();
            services.AddScoped<IContentService, ContentService>();
            services.AddScoped<IUserContentService, UserContentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IMapper mapper)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //must be added before app.UseMvc();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            //Throws exception incase automapper configuration have unmapped properties bewteen source type and destination type
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            app.UseAuthentication();

            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty; //to serve the Swagger UI at the app's root i.e. http://localhost:<port>/
            });

            app.UseMvc();
        }

        /// <summary>
        /// Register the Swagger generator, defining 1 or more Swagger documents
        /// </summary>
        private void AddSwaggerGen(IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Project Energy API"
                    //,
                    //Description = "Provides methods for data access",
                    //TermsOfService = "None",
                    //Contact = new Contact
                    //{
                    //    Name = "Param",
                    //    Email = string.Empty,
                    //    Url = "https://twinflamescoach.com"
                    //},
                    //License = new License
                    //{
                    //    Name = "Use under LICX",
                    //    Url = "https://example.com/license"
                    //}
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
