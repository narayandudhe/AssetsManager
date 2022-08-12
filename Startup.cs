using AssetsManager.Models;
using AssetsManager.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace AssetsManager
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
            //inform application to this is dbcontext class
            services.AddDbContext<AssetManagerDBContext>(Options=>Options.UseSqlServer(Configuration.GetConnectionString("AssetManagerDB")));
            //add our application services
            services.AddTransient<IEmployeeDetailsRepository, EmployeeDetailsRepository>();
            services.AddTransient<IAssetDetilsRepository, AssetDetilsRepository>();
            services.AddTransient<IAssetAssignedRepository, AssetAssignedRepository>();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            }); 
            services.AddCors(option=> 
            {
                option.AddDefaultPolicy(builder=> 
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            
          
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
               Path.Combine(Directory.GetCurrentDirectory(), @"Pictures/AssetPic")),
                RequestPath = "/Aimage",
                EnableDefaultFiles = true
            });
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
              Path.Combine(Directory.GetCurrentDirectory(), @"Pictures/EmployeePic")),
                RequestPath = "/Eimage",
                EnableDefaultFiles = true
            });
            app.UseStaticFiles(); // For the wwwroot folder


            app.UseRouting();

            app.UseCors();


                app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
