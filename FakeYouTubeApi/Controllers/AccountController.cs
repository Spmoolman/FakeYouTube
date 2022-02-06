using ContactManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FakeYouTubeApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]/[action]")]
public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    private readonly ILogger _logger;

    public AccountController(IServiceProvider services)
    {
        _userManager = services.GetService<UserManager<IdentityUser>>();
        _signInManager = services.GetService<SignInManager<IdentityUser>>();
        _logger = services.GetService<ILogger<AccountController>>();
    }

    // POST: /Account/Login
    [HttpPost(Name = "Login")]
    [AllowAnonymous]
    public async Task<bool> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(model.Email, 
                model.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation(1, "User logged in.");
                return true;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return false;
            }
        }

        // If we got this far, something failed, redisplay form
        return false;
    }

    [HttpPost(Name = "Register")]
    [AllowAnonymous]
    public async Task<bool> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation(3, "User created a new account with password.");
                return true;
            }
        }

        // If we got this far, something failed, redisplay form
        return false;
    }

    // POST: /Account/LogOut
    [HttpPost(Name = "LogOut")]
    public async Task<bool> LogOut()
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation(4, "User logged out.");
        return true;
    }
}
