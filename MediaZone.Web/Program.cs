using MediaZone.Data;
using MediaZone.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

using Microsoft.Extensions.Azure;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Core.Extensions;
using Amazon.Rekognition;
using MediaZone.Services.Interfaces;
using MediaZone.Services;

namespace MediaZone.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseDefaultServiceProvider(options => options.ValidateScopes = false);

            // Add services to the container.
            var dbConnectionString = builder.Configuration.GetConnectionString("Database") ?? throw new InvalidOperationException("Connection string 'Database' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(dbConnectionString).UseLazyLoadingProxies());
               
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                if (builder.Environment.IsDevelopment())
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredLength = 4;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredUniqueChars = 1;
                }
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            string blobStorageConnectionString = builder.Configuration.GetConnectionString("BlobStorage") ?? throw new InvalidOperationException("Connection string 'BlobStorage' not found.");
            builder.Services.AddAzureClients(config =>
            {
                config.UseCredential(new DefaultAzureCredential());
                config.AddBlobServiceClient(blobStorageConnectionString);
            });

            builder.Services.AddScoped<IAmazonRekognition, AmazonRekognitionClient>();

            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IIdentityService, IdentityService>();
            builder.Services.AddScoped<IFolderService, FolderService>();


            builder.Services.AddControllersWithViews();

            
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            
            app.Run();
        }
    }
}