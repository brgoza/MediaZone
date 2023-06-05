using MediaZone.Data.Entities.Identity;
using MediaZone.Services.Interfaces;
using MediaZone.Web.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MediaZone.Web.Controllers;

public class AccountController : Controller
{
    private readonly IIdentityService _identityService;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ILogger<AccountController> _logger;
    
    public AccountController(IIdentityService identityService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILogger<AccountController> logger)
    {
        _identityService = identityService;
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult Login() => View();
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        if (result.Succeeded)
        {
            _logger.LogInformation("user {userName} logged in", User.Identity?.Name ?? "??");
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }
    }
    [HttpGet]
    public IActionResult Register() => View();
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        AppUser newUser = new()
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };
        IdentityResult? result = await _identityService.CreateUser(newUser,model.Password, false);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(newUser, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    } 
}
