using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Olimp2019.Data.Models {
	public class ApplicationDbContext : IdentityDbContext<User, Role, Guid> {
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Level> Levels { get; set; }

		protected override void OnModelCreating(ModelBuilder builder) {
			base.OnModelCreating(builder);
		}

		public async virtual Task<List<User>> GetUsersAsync() {
			return await Users.ToListAsync();
		}
	}
}
