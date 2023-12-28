using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class RolController : Controller
    {
        public IActionResult GetAll()
        {
            return View();
        }
    }
}
