using David.OpheliaTest.BusinessLayer.Contracts;
using David.OpheliaTest.BusinessLayer.Services;
using David.OpheliaTest.DataAccessLayer;
using David.OpheliaTest.DataAccessLayer.Contracts;
using David.OpheliaTest.DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;
using System.Linq;

namespace David.OpheliaTest.API
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Ophelia API Rest",
                        Version = "v1",
                        Description = "Ophelia API Rest",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Ophelia API Rest",
                            Email = string.Empty,
                            Url = new System.Uri("https://foo.com/"),
                        }
                    });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            Startup.AddRepository(services);
            Startup.AddDbContext(services, this.Configuration.GetConnectionString("DefaultConnection"));
            Startup.AddServices(services);
            //services.AddControllers();

            //Seed database
            services.AddTransient<SeedDb>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:4200");
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("./v1/swagger.json", "API Prueba Ophelia test ");
                c.RoutePrefix = string.Empty;

            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("./v1/swagger.json", "MyAPI V1");
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        public static IServiceCollection AddRepository(IServiceCollection services)
        {
            #region Application
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ISaleRepository, SaleRepository>();
            services.AddTransient<ISaleDetailRepository, SaleDetailRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IWalletRepository, WalletRepository>();
            #endregion

            return services;
        }

        public static IServiceCollection AddDbContext(IServiceCollection services, string DefaultConnection)
        {

            services.AddDbContext<ApplicationDataContext>(options =>
            options.UseSqlServer(DefaultConnection, b => b.MigrationsAssembly("David.OpheliaTest.API")), ServiceLifetime.Transient);

            return services;
        }

        public static IServiceCollection AddServices(IServiceCollection services)
        {
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            #region Application
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<ISaleDetailService, SaleDetailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWalletService, WalletService>();
            #endregion
            return services;
        }
    }
}
