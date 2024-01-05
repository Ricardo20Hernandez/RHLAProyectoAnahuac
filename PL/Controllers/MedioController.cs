using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PL.Controllers
{
    public class MedioController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Medio medio = new ML.Medio();

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

            //ML.Result result = BL.Medio.GetAll();

            //if (result.Correct)
            //{
            //    medio.Medios = result.Objects;
            //}
            //else
            //{
            //    ViewBag.Mensaje = "Sin medios registrados aún";
            //}

            return View(medio);
        }

        public IActionResult MedioGetAll()
        {
            ML.Result result = BL.Medio.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        public IActionResult TipoMedioGetAll()
        {
            ML.Result result = BL.TipoMedio.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        public IActionResult EditorialGetAll()
        {
            ML.Result result = BL.Editorial.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        public IActionResult IdiomaGetAll()
        {
            ML.Result result = BL.Idioma.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        public IActionResult AutorGetAll()
        {
            ML.Result result = BL.Autor.GetAllDropDownList();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        public JsonResult GetById(int IdMedio)
        {
            ML.Result result = BL.Medio.GetById(IdMedio);
            ML.Medio medio = new ML.Medio();

            medio = (ML.Medio)result.Object; //Unboxing
            return Json(result);         
        }

        //[HttpGet]
        //public IActionResult Form(int? IdMedio)
        //{
        //    ML.Medio medio = new ML.Medio();

        //    //Drop Down Lists

        //    ML.Result resultTipoMedios = BL.TipoMedio.GetAll();
        //    medio.TipoMedio = new ML.TipoMedio();
        //    medio.TipoMedio.TipoMedios = resultTipoMedios.Objects;

        //    ML.Result resultEditoriales = BL.Editorial.GetAll();
        //    medio.Editorial = new ML.Editorial();
        //    medio.Editorial.Editoriales = resultEditoriales.Objects;

        //    ML.Result resultIdiomas = BL.Idioma.GetAll();
        //    medio.Idioma = new ML.Idioma();
        //    medio.Idioma.Idiomas = resultIdiomas.Objects;

        //    ML.Result resultAutores = BL.Autor.GetAllDropDownList();
        //    medio.Autor = new ML.Autor();
        //    medio.Autor.Autores = resultAutores.Objects;

        //    if(IdMedio == null)
        //    {
        //        ViewBag.Accion = "Agregar";
        //    }
        //    else
        //    {
        //        ML.Result result = BL.Medio.GetById(IdMedio.Value);

        //        if (result.Correct)
        //        {
        //            medio = (ML.Medio)result.Object;
        //        }
        //        medio.TipoMedio.TipoMedios = resultTipoMedios.Objects;
        //        medio.Editorial.Editoriales = resultEditoriales.Objects;
        //        medio.Idioma.Idiomas = resultIdiomas.Objects;
        //        medio.Autor.Autores = resultAutores.Objects;
        //        ViewBag.Accion = "Actualizar";
        //    }
        //    return View(medio);
        //}


        //[HttpPost]
        //public IActionResult Form(ML.Medio medio, IFormFile fuImagen)
        //{
        //    if (fuImagen != null)
        //    {
        //        medio.Imagen = convertFileToByteArray(fuImagen);

        //    }

        //    if(medio.IdMedio == 0)
        //    {
        //        ML.Result result = BL.Medio.Add(medio);
        //        if (result.Correct)
        //        {
        //            ViewBag.Mensaje = "Medio añadido correctamente";
        //        }
        //        else
        //        {
        //            ViewBag.Mensaje = "Error al añadir el medio";
        //        }
        //    }
        //    else
        //    {
        //        ML.Result result = BL.Medio.Update(medio);
        //        if (result.Correct)
        //        {
        //            ViewBag.Mensaje = "Medio actualizado correctamente";
        //        }
        //        else
        //        {
        //            ViewBag.Mensaje = "Error al actualizar el medio";
        //        }
        //    }
        //    return View("Modal");

        //}


        [HttpPost]
        public JsonResult Form(ML.Medio medio, IFormFile fuImagen)
        {
            ML.Result result = new ML.Result(); 

            if(fuImagen != null)
            {
                medio.Imagen = convertFileToByteArray(fuImagen);
            }
            
            if(medio.IdMedio == 0)
            {
                result = BL.Medio.Add(medio);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Medio agregado correctamente";
                    return Json(result);
                }
                else
                {
                    ViewBag.Mensaje = "Medio no ingresado correctamente";
                    return Json(result);
                }
               
            }
            else
            {
                result = BL.Medio.Update(medio);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Medio actualizado correctamente";
                    return Json(result);
                }
                else
                {
                    ViewBag.Mensaje = "Medio no actualizado correctamente";
                    return Json(result);
                }
            }
        }

        public JsonResult Delete(int IdMedio)
        {
            ML.Result result = BL.Medio.Delete(IdMedio);

            if (result.Correct)
            {
                ViewBag.Mensaje = "Medio eliminado correctamente";
                return Json(result);
            }
            else
            {
                ViewBag.Mensaje = "Error al eliminar el medio";
                return Json(result);
            }
        }

        //public IActionResult Delete(int IdMedio)
        //{
        //    ML.Result result = BL.Medio.Delete(IdMedio);

        //    if (result.Correct)
        //    {
        //        ViewBag.Mensaje = "Medio eliminado correctamente";
        //    }
        //    else
        //    {
        //        ViewBag.Mensaje = "Error al eliminar el medio";
        //    }
        //    return View("Modal");
        //}

        public byte[] convertFileToByteArray(IFormFile fuImagen)
        {
            MemoryStream target = new MemoryStream();
            fuImagen.OpenReadStream().CopyTo(target);
            byte[] data = target.ToArray();
            return data;
        }


    }
}
