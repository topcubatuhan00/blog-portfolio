using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "Admin2")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
