using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class AboutController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
