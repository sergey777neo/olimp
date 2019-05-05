using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Olimp2019.Data.Models;
using Olimp2019.Web.Services;

namespace Olimp2019.Web.Pages.Account
{
	public class RegisterModel : PageModel
	{
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;
		private readonly ILogger<LoginModel> _logger;
		private readonly IEmailSender _emailSender;

		public RegisterModel(
			UserManager<User> userManager,
			SignInManager<User> signInManager,
			ILogger<LoginModel> logger,
			IEmailSender emailSender)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_logger = logger;
			_emailSender = emailSender;
		}

		[BindProperty]
		public InputModel Input { get; set; }

		public string ReturnUrl { get; set; }

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

			[Required(ErrorMessage = "Введите полное имя")]
			[DataType(DataType.Text)]
			[Display(Name = "Полное имя")]
			public string FullName { get; set; }
		}

		public void OnGet(string returnUrl = null)
		{
			ReturnUrl = returnUrl;
		}

		public async Task<IActionResult> OnPostAsync(string returnUrl = null)
		{
			ReturnUrl = returnUrl;
			if (ModelState.IsValid)
			{
				var user = new User { UserName = Input.Email, Email = Input.Email, FullName = Input.FullName };
				var result = await _userManager.CreateAsync(user, Input.Password);
				if (result.Succeeded)
				{
					_logger.LogInformation("User created a new account with password.");

                    await _signInManager.UserManager.AddToRoleAsync(user, "User");

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                                "/Account/ConfirmEmail",
                                pageHandler: null,
                                values: new { userId = user.Id, code = code },
                                protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(Url.GetLocalUrl(returnUrl));					
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			// If we got this far, something failed, redisplay form
			return Page();
		}
	}
}
