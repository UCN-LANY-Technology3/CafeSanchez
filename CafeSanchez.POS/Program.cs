using CafeSanchez.POS.Services.Auth;
using CafeSanchez.POS.Services.OrderManagement;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CafeSanchez.POS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string userDataConnectionString = builder.Configuration.GetConnectionString("UserData") ?? throw new Exception("No connectionstring for authentication service");

            // Add services to the container.
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/";         
            });
            builder.Services.AddScoped(_ => new LoginService(userDataConnectionString));
            builder.Services.AddScoped(_ => new OrderManagementService(builder.Configuration));
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
