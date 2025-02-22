using Blog.Application.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAboutService _aboutService;

        public HomeController(ILogger<HomeController> logger, IAboutService aboutService)
        {
            _logger = logger;
            _aboutService = aboutService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _aboutService.GetAbouts();
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
