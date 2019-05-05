using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;

namespace Olimp2019.Data.Models
{
	public class User : IdentityUser<Guid>
	{
		public string FullName { get; set; }

		public int Score { get; set; }

		[DefaultValue(1)]
		public int CurrentLevel { get; set; }

		public int CurrentStep { get; set; }
	}
}
