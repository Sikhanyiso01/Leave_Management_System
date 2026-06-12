using Leave_Management_System.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Leave_Management_System.web.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            var data = new TestViewModel
            {
                Name = "Hanyani Shabalala AKA (Miss Kitty)",
                DateOfBirth = new DateTime(2004, 06, 03)
            };
            return View(data);
        }
    }
}
