using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyEshop.Controllers
{
    public class RoleController : Controller
    {
        [Authorize(Policy = "RequireManager")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "RequireAdmin")]
        public IActionResult Employee()
        {
            return View();
        }

        [Authorize(Policy = "RequireUser")]
        public IActionResult ProductsforCustomers()
        {
            return View();
        }
    }
}
