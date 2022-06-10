using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyEshop.Controllers
{
    public class RoleController : Controller
    {
        /* AUTHORIZING MANAGER ROLE*/
        [Authorize(Policy = "RequireManager")]
        public IActionResult Index()
        {
            return View();
        }
        /* AUTHORIZING ADMIN ROLE FOR EMPLOYEE PAGE*/
        [Authorize(Policy = "RequireAdmin")]
        public IActionResult Employee()
        {
            return View();
        }
        /*AUTHORIZING ADMIN USER FOR CUSTOMER'S PRODUCT PAGE*/
        [Authorize(Policy = "RequireUser")]
        public IActionResult ProductsforCustomers()
        {
            return View();
        }
    }
}
