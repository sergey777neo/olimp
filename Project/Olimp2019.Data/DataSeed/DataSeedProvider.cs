using Olimp2019.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Olimp2019.Data.DataSeed
{
	public class DataSeedProvider
	{
		public static async Task Initialize(ApplicationDbContext context, RoleManager<Role> roleManager)
		{
			await context.Database.EnsureCreatedAsync();

			await SeedUserRoles(context, roleManager);
		}

		public static async Task SeedUserRoles(ApplicationDbContext context, RoleManager<Role> roleManager)
		{
			if (!context.Roles.Any())
			{
				await roleManager.CreateAsync(new Role("Administrator"));
				await roleManager.CreateAsync(new Role("User"));

				await context.SaveChangesAsync();
			}
		}
	}
}
