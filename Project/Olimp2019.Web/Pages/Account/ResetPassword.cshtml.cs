using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Olimp2019.Data.Models;

namespace Olimp2019.Web.Pages.Account
{
	public class ResetPasswordModel : PageModel
	{
		private readonly UserManager<User> _userManager;

		public ResetPasswordModel(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		[BindProperty]
		public InputModel Input { get; set; }

		public class InputModel
		{
			[Required(ErrorMessage = "Введите e-mail")]
			[EmailAddress]
			[Display(Name = "E-mail")]
			public string Email { get; set; }

			[Required(ErrorMessage = "Введите пароль")]
			[DataType(DataType.Password)]
			[Display(Name = "Пароль")]
			public string Password { get; set; }

			[DataType(DataType.Password)]
			[Display(Name = "Подтвердите пароль")]
			[Compare("Password", ErrorMessage = "Пароли не совпадают")]
			public string ConfirmPassword { get; set; }

			public string Code { get; set; }
		}

		public IActionResult OnGet(string code = null)
		{
			if (code == null)
			{
				throw new ApplicationException("A code must be supplied for password reset.");
			}
			else
			{
				Input = new InputModel
				{
					Code = code
				};
				return Page();
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var user = await _userManager.FindByEmailAsync(Input.Email);
			if (user == null)
			{
				// Don't reveal that the user does not exist
				return RedirectToPage("./ResetPasswordConfirmation");
			}

			var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
			if (result.Succeeded)
			{
				return RedirectToPage("./ResetPasswordConfirmation");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
			return Page();
		}
	}
}
