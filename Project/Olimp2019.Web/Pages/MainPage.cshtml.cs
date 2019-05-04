using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Olimp2019.Data.Models;
using System.Threading.Tasks;

namespace Olimp2019.Web.Pages
{
	[Authorize(Roles = "User")]
	public class MainPageModel : PageModel
	{
		private readonly SignInManager<User> _signInManager;
		public string CurrentUserFullName { get; set; }
		public int CurrentScore { get; set; }

		public MainPageModel(SignInManager<User> signInManager)
		{
			_signInManager = signInManager;
		}

		public async Task OnGetAsync()
		{
			var user = await _signInManager.UserManager.FindByNameAsync(User.Identity.Name);

			CurrentUserFullName = user.FullName;
		}
	}
}