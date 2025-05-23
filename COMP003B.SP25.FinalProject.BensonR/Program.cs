// Author: Riley Benson
// Course: COMP-003B: ASP.NET Core
// Instructor: Jonathan Cruz
// Purpose: Final project synthesizing MVC, Web API, EF Core, and middleware
using COMP003B.SP25.FinalProject.BensonR.Data;
using COMP003B.SP25.FinalProject.BensonR.Middleware;
using Microsoft.EntityFrameworkCore;
namespace COMP003B.SP25.FinalProject.BensonR
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<ComputerContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			builder.Services.AddLogging();
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

			app.UseMiddleware<RequestTimingMiddleware>();
			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}