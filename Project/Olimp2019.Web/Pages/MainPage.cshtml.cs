using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Olimp2019.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Olimp2019.Web.Pages
{
	[Authorize(Roles = "User")]
	public class MainPageModel : PageModel
	{
		private readonly SignInManager<User> _signInManager;
		private readonly ApplicationDbContext _context;
		public string CurrentUserFullName { get; set; }
		public int CurrentScore { get; set; }
		public int CurrentLevel { get; set; }
		public int CurrentLevelStep { get; set; }
		public int CurrentLevelStepsCount { get; set; }

		public MainPageModel(SignInManager<User> signInManager, ApplicationDbContext context)
		{
			_signInManager = signInManager;
			_context = context;
		}

		public async Task OnGetAsync()
		{
			var user = await _signInManager.UserManager.FindByNameAsync(User.Identity.Name);
			var level = _context.Levels.First(l => l.Order == user.CurrentLevel);

			CurrentUserFullName = user.FullName;
			CurrentScore = user.Score;
			CurrentLevel = user.CurrentLevel;
			CurrentLevelStepsCount = level.StepCount;
			CurrentLevelStep = user.CurrentStep;
		}
	}
}