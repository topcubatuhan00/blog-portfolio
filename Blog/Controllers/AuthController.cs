using Blog.Domain.Entities;
using Blog.Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            ModelState.AddModelError("", "Email or password is wrong");
            return View(model);
        }

        bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.Password);

        if (!isPasswordCorrect)
        {
            ModelState.AddModelError("", "Email or password is wrong");
            return View(model);
        }

        await _signInManager.SignInAsync(user, isPersistent: model.RememberMe);
        return RedirectToAction("Index", "Home");
    }


    public async Task<IActionResult> Register()
    {
        User user = new User { UserName = "topcubatuhan00", FullName = "Batuhan Topcu", Email = "topcubatuhan00@gmail.com", EmailConfirmed = true, PhoneNumber = "05538570750" };
        //var res = await _userManager.CreateAsync(user, "Bursa16**..");
        //await _roleManager.CreateAsync(new IdentityRole("Admin"));
        var res = await _userManager.AddToRoleAsync(user, "Admin");
        if (res.Succeeded)
        {
            return RedirectToAction("Login", "Account");
        }
        else
        {
            foreach (var item in res.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            return View();
        }
    }


    public async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied(string returnUrl)
    {
        return View();
    }

}
