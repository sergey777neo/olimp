using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Olimp2019.Data.Models;

namespace Olimp2019.Web.Controllers
{
	[Route("[controller]/[action]")]
	public class GameController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly SignInManager<User> _signInManager;

		public GameController(ApplicationDbContext context, SignInManager<User> user)
		{
			_context = context;
			_signInManager = user;
		}

		public IActionResult Index() {
			return View();
		}

		public ActionResult LoadCurrentLevel() {
			var user = _context.Users.First(u => u.UserName == User.Identity.Name);
			var level = _context.Levels.First(l => l.Order == user.CurrentLevel);
			return PartialView(level.Name);
		}

		[HttpGet("{id}")]
		public ActionResult LoadCurrentLevel(int id) {
			var level = _context.Levels.First(l => l.Order == id);
			return PartialView(level.Name, new { level });
		}
	}
}
