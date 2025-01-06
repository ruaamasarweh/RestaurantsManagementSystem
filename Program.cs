using Gp.Data;
using Microsoft.EntityFrameworkCore;

namespace Gp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDistributedMemoryCache();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSession(options => { 
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true; // Security: make the cookie HttpOnly
                options.Cookie.IsEssential = true;
            });


            builder.Services.AddDbContext<SystemDbContext>(
            item => item.UseSqlServer(builder.Configuration.GetConnectionString("conn")) 
             );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=Login}/{id?}");

      



            app.Run();
        }
    }
}
