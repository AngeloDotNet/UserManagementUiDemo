using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using UserManagementUiDemo.Data;
using UserManagementUiDemo.Models.Entities;

namespace UserManagementUiDemo;

public class Startup(IConfiguration configuration)
{
	public IConfiguration Configuration { get; } = configuration;

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddDbContext<ApplicationDbContext>(options
			=> options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

		services.AddDatabaseDeveloperPageExceptionFilter();
		services.AddDefaultIdentity<ApplicationUser>(options =>
		{
			options.SignIn.RequireConfirmedAccount = true;
			options.Lockout.MaxFailedAccessAttempts = 5;
			options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
			options.Lockout.AllowedForNewUsers = true;
		}).AddEntityFrameworkStores<ApplicationDbContext>();

		services.AddControllersWithViews();
		services.AddRazorPages();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			app.UseMigrationsEndPoint();
		}
		else
		{
			app.UseExceptionHandler("/Home/Error");
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
			endpoints.MapRazorPages();
		});
	}
}