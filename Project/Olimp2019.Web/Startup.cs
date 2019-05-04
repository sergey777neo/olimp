using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Olimp2019.Web.Services;
using Olimp2019.Data.Models;
using Olimp2019.Repositories.Implementations;
using Olimp2019.Repositories;

namespace Olimp2019
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
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddAuthorization(options =>
			{
				options.AddPolicy("RequireAdminRole",
					 policy => policy.RequireRole("Administrator"));
			});

			services
				.AddIdentity<User, Role>(options =>
				{
					options.Password.RequireNonAlphanumeric = false;
				})
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			services.AddMvc()
				.AddRazorPagesOptions(options =>
				{
					options.Conventions.AuthorizeFolder("/Account/Manage");
					options.Conventions.AuthorizePage("/Account/Logout");
					options.Conventions.AuthorizeFolder("/Reports", "RequireAdminRole");
				});

			services.AddAuthentication()
				.AddFacebook(facebookOptions =>
				{
					facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
					facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
				})
				.AddGoogle(options =>
				{
					options.ClientId = Configuration["Authentication:Google:ClientId"];
					options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
				});

			// Register no-op EmailSender used by account confirmation and password reset during development
			// For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
			services.AddSingleton<IEmailSender, EmailSender>();

			services.AddScoped<IReportRepository, ReportRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseMvc();
		}
	}
}
