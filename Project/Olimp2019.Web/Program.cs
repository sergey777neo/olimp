using Olimp2019.Data.DataSeed;
using Olimp2019.Data.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Olimp2019.Web
{
	public class Program
	{
		static async Task Main(string[] args)
		{
			var host = BuildWebHost(args);

			using (var scope = host.Services.CreateScope()) {
				var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
				await DataSeedProvider.Initialize(context, roleManager);
			}

			host.Run();
		}

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.Build();
	}
}