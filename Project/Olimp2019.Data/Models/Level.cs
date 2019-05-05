using System;

namespace Olimp2019.Data.Models
{
	public class Level
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int Order { get; set; }
		public int StepCount { get; set; }
	}
}
