using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;


namespace WebApp
{
    public class Program
    {
        public const string UrlAPI = "http://localhost:5045/api";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapBlazorHub();
                endpoint.MapFallbackToPage("/_Host");
                endpoint.MapFallbackToPage("/auth", "/_Host");
                endpoint.MapFallbackToPage("/index", "/_Host");
                endpoint.MapFallbackToPage("/profile", "/_Host");
                endpoint.MapFallbackToPage("/schedule", "/_Host");
                endpoint.MapFallbackToPage("/teacher", "/_Host");
                endpoint.MapFallbackToPage("/group/{id:int}", "/_Host");
                endpoint.MapFallbackToPage("/admin/user/{id:int}", "/_Host");
            });


            app.Run();
        }
    }
}