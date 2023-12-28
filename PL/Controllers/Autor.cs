using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Autor : Controller
    {
        public IActionResult GetAll()
        {
            ML.Result result = BL.Autor.GetAll();
            if (result.Correct)
            {
                ML.Autor autor = new ML.Autor();
               autor.Autores = result.Objects;

                return View(autor);
            }
            else
            {
                return View();
            }
        }
    }
}
