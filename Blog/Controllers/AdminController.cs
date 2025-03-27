using Blog.Application.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class AdminController : Controller
{
    private readonly IBlogService _blogService;

    public AdminController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        var model = await _blogService.GetAdminHomeData();
        return View(model);
    }
}
