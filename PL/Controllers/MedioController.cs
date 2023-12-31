using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PL.Controllers
{
    public class MedioController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Medio medio = new ML.Medio();

            ML.Result result = BL.Medio.GetAll();

            if (result.Correct)
            {
                medio.Medios = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = "Sin medios registrados aún";
            }
            
            return View(medio);
        }

        [HttpGet]
        public IActionResult Form(int? IdMedio)
        {
            ML.Medio medio = new ML.Medio();

            //Drop Down Lists

            ML.Result resultTipoMedios = BL.TipoMedio.GetAll();
            medio.TipoMedio = new ML.TipoMedio();
            medio.TipoMedio.TipoMedios = resultTipoMedios.Objects;

            ML.Result resultEditoriales = BL.Editorial.GetAll();
            medio.Editorial = new ML.Editorial();
            medio.Editorial.Editoriales = resultEditoriales.Objects;

            ML.Result resultIdiomas = BL.Idioma.GetAll();
            medio.Idioma = new ML.Idioma();
            medio.Idioma.Idiomas = resultIdiomas.Objects;

            ML.Result resultAutores = BL.Autor.GetAllDropDownList();
            medio.Autor = new ML.Autor();
            medio.Autor.Autores = resultAutores.Objects;

            if(IdMedio == null)
            {
                ViewBag.Accion = "Agregar";
            }
            else
            {
                ML.Result result = BL.Medio.GetById(IdMedio.Value);

                if (result.Correct)
                {
                    medio = (ML.Medio)result.Object;
                }
                medio.TipoMedio.TipoMedios = resultTipoMedios.Objects;
                medio.Editorial.Editoriales = resultEditoriales.Objects;
                medio.Idioma.Idiomas = resultIdiomas.Objects;
                medio.Autor.Autores = resultAutores.Objects;
                ViewBag.Accion = "Actualizar";
            }
            return View(medio);
        }
    }
}
