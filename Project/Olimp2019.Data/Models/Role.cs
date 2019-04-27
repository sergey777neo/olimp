using Microsoft.AspNetCore.Identity;
using System;

namespace Olimp2019.Data.Models
{
	public sealed class Role : IdentityRole<Guid>
	{
		public Role() : base() { }

		public Role(string roleName) : base(roleName) { }
	}
}
