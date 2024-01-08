using Microsoft.AspNetCore.Mvc;


namespace PL.Controllers
{
    public class Autor : Controller
    {
        public IActionResult GetAll()
        {
            ML.Autor autor = new ML.Autor();
            //if (result.Correct)
            //{
            //    ML.Autor autor = new ML.Autor();
            //   autor.Autores = result.Objects;

            //    return View(autor);
            //}
            //else
            //{
            //    return View();
            //}

            return View(autor);
        }

        //Simulacion de servicio
        public IActionResult AutorGetAll()
        {
            ML.Result result = BL.Autor.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        //form
        // [Authorize(Roles = "Administrador")]
        //[HttpGet]
        //public ActionResult Form(int? IdAutor)
        //{

        //    //ML.Producto producto = new ML.Producto();
        //    ML.Autor autor = new ML.Autor();

        //    if (IdAutor.HasValue)
        //    {
        //        ViewBag.Accion = "Actualizar";
        //        ML.Result result = new ML.Result();
        //        result = BL.Autor.GetById(IdAutor.Value);
        //        autor = (ML.Autor)result.Object;

        //    }
        //    else
        //    { ViewBag.Accion = "Agregar"; }
        //    return View(autor);
        //}

        // [Authorize(Roles = "Administrador")]
        //[HttpPost]
        //public IActionResult Form(ML.Autor autor, IFormFile fuImagen)
        //{
        //    if (fuImagen != null)
        //    {
        //        autor.Foto = convertFileToByteArray(fuImagen);
        //    }
        //    if (autor.IdAutor == 0)
        //    {
        //        ML.Result result = new ML.Result();
        //            result = BL.Autor.Add(autor);
        //        if (result.Correct)
        //        {
        //            ViewBag.Message = "Autor agregado correctamente";


        //            return PartialView("Modal");
        //        }
        //    }
        //    else
        //    {
        //        ML.Result result = BL.Autor.Update(autor);
        //        if (result.Correct)
        //        {
        //              ViewBag.Message = "Autor actualizado correctamente";

        //            return PartialView("Modal");
        //        }
        //    }

        //    return View();
        //}

        [HttpPost]
        public JsonResult Form(ML.Autor autor, IFormFile fuImagen)
        {
            ML.Result result = new ML.Result();

            if (fuImagen != null)
            {
                autor.Foto = convertFileToByteArray(fuImagen);
            }
            if (autor.IdAutor == 0)
            {
                result = BL.Autor.Add(autor);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Autor agregado correctamente";
                    return Json(result);
                }
                else
                {
                    ViewBag.Mensaje = "Autor no ingresado correctamente";
                    return Json(result);
                }
            }
            else
            {
                result = BL.Autor.Update(autor);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Autor actualizado correctamente";
                    return Json(result);
                }
                else
                {
                    ViewBag.Mensaje = "Autor actualizado correctamente";
                    return Json(result);
                }
            }
        }

        public JsonResult GetById(int IdAutor)
        {
            ML.Result result = BL.Autor.GetById(IdAutor);
            ML.Autor autor = new ML.Autor();

            autor = (ML.Autor)result.Object; //Unboxing
            return Json(result);

        }

        public byte[] convertFileToByteArray(IFormFile Foto)
        {
            //    MemoryStream target = new MemoryStream();
            //    Foto.OpenReadStream.fuImagen.InputStream.CopyTo(target);
            //    byte[] data = target.ToArray();
            //    return data;

            byte[] buffer = new byte[Foto.Length];
            var resultInBytes = ConvertToBytes(Foto);

            Array.Copy(resultInBytes, buffer, resultInBytes.Length);

            return buffer;


        }

        private byte[] ConvertToBytes(IFormFile image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.OpenReadStream().CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        //public IActionResult Delete(int IdAutor)
        //{
        //    ML.Result result = new ML.Result();
        //    result = BL.Autor.Delete(IdAutor);
        //    if (result.Correct) { }
        //    ViewBag.Message = "Autor borrado correctamente";
        //    return PartialView("Modal");

        //}
    }
}
