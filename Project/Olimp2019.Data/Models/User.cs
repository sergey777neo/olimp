using Microsoft.AspNetCore.Identity;
using System;

namespace Olimp2019.Data.Models
{
	public class User : IdentityUser<Guid>
	{
		public string FullName { get; set; }
	}
}
