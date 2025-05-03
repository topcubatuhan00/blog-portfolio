using Blog.Application.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class AboutController : Controller
{
    private readonly IAboutService  _aboutService;

    public AboutController(IAboutService  aboutService)
    {
        _aboutService = aboutService;
    }
    public async Task<IActionResult> Index()
    {
        var res = await _aboutService.GetAbouts();
        return View(res);
    }
}
