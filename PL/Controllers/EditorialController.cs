﻿using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EditorialController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Editorial editorial = new ML.Editorial();

            editorial.Direccion = new ML.Direccion();
            editorial.Direccion.Colonia = new ML.Colonia();
            editorial.Direccion.Colonia.Municipio = new ML.Municipio();
            editorial.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            editorial.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            ML.Result resultPaises = BL.Pais.GetAll();
            editorial.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
            //ML.Result resultEstados = BL.Estado.GetAll();
            //editorial.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;
            //ML.Result resultMunicipios = BL.Municipio.GetAll();
            //editorial.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;
            //ML.Result resultColonias = BL.Colonia.GetAll();
            //editorial.Direccion.Colonia.Colonias = resultColonias.Objects;


            //ML.Result result = BL.Editorial.GetAll();

            //if (result.Correct)
            //{
            //    editorial.Editoriales = result.Objects;

            //}
            //else
            //{
            //    ViewBag.Mensaje = "No hay editoriales aún registradas";
            //}

            return View(editorial);
        }

        //Simulacion de servicio
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

        public IActionResult PaisGetAll()
        {
            ML.Result result = BL.Pais.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        public JsonResult GetById(int IdEditorial)
        {
            ML.Result result = BL.Editorial.GetById(IdEditorial);
            if (result.Correct)
            {
                //Inicializar variables de navegacion
                ML.Editorial editorial = new ML.Editorial();
                editorial.Direccion = new ML.Direccion();
                editorial.Direccion.Colonia = new ML.Colonia();
                editorial.Direccion.Colonia.Municipio = new ML.Municipio();
                editorial.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                editorial.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                editorial = (ML.Editorial)result.Object; //Unboxing

                ML.Result resultPaises = BL.Pais.GetAll();
                ML.Result resultEstados = BL.Estado.GetByIdPais(editorial.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                ML.Result resultMunicipios = BL.Municipio.GetByIdEstado(editorial.Direccion.Colonia.Municipio.Estado.IdEstado);
                ML.Result resultColonias = BL.Colonia.GetByIdMunicipio(editorial.Direccion.Colonia.Municipio.IdMunicipio);

                editorial.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
                editorial.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;
                editorial.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;
                editorial.Direccion.Colonia.Colonias = resultColonias.Objects;

                return Json(result);
            }
            else
            {
                return Json(result);    
            }

        }

        //[HttpGet]
        //public IActionResult Form(int? IdEditorial)
        //{
        //    ML.Editorial editorial = new ML.Editorial();

        //    editorial.Direccion = new ML.Direccion();
        //    editorial.Direccion.Colonia = new ML.Colonia();
        //    editorial.Direccion.Colonia.Municipio = new ML.Municipio();
        //    editorial.Direccion.Colonia.Municipio.Estado = new ML.Estado();
        //    editorial.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

        //    ML.Result resultPaises = BL.Pais.GetAll();
        //    editorial.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;

        //    if (IdEditorial == null)
        //    {
        //        ViewBag.Accion = "Agregar";
        //    }
        //    else
        //    {
        //        ML.Result result = BL.Editorial.GetById(IdEditorial.Value);

        //        if (result.Correct)
        //        {
        //            //Inicializar variables de navegacion
        //            editorial.Direccion = new ML.Direccion();
        //            editorial.Direccion.Colonia = new ML.Colonia();
        //            editorial.Direccion.Colonia.Municipio = new ML.Municipio();
        //            editorial.Direccion.Colonia.Municipio.Estado = new ML.Estado();
        //            editorial.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

        //            editorial = (ML.Editorial)result.Object; //Unboxing

        //            ML.Result resultEstados = BL.Estado.GetByIdPais(editorial.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
        //            ML.Result resultMunicipios = BL.Municipio.GetByIdEstado(editorial.Direccion.Colonia.Municipio.Estado.IdEstado);
        //            ML.Result resultColonias = BL.Colonia.GetByIdMunicipio(editorial.Direccion.Colonia.Municipio.IdMunicipio);

        //            editorial.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
        //            editorial.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;
        //            editorial.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;
        //            editorial.Direccion.Colonia.Colonias = resultColonias.Objects;
        //        }
        //        ViewBag.Accion = "Actualizar";
        //    }
        //    return View(editorial);
        //}

        [HttpPost]
        public IActionResult Form(ML.Editorial editorial)
        {

            if(editorial.IdEditorial == null)
            {
                ML.Result result = BL.Editorial.Add(editorial);
                if (result.Correct)
                {
                    //Enviamos datos del controlador hacia la vista
                    ViewBag.Mensaje = "Se ha ingresado correctamente la editorial";
                    return Json(result);
                }
                else
                {
                    ViewBag.Mensaje = "No se ha ingresado correctamente la editorial" + result.ErrorMessage;
                    return Json(result);
                }
            }
            else
            {
                ML.Result result = BL.Editorial.Update(editorial);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se ha actualizado correctamente la editorial";
                    return Json(result);
                }
                else
                {
                    ViewBag.Mensaje = "No se ha actualizado correctamente la editorial" + result.ErrorMessage;
                    return Json(result);
                }
            }
        }

        //public IActionResult Delete (int IdEditorial)
        //{
        //    ML.Result result = BL.Editorial.Delete(IdEditorial);

        //    if (result.Correct)
        //    {
        //        ViewBag.Mensaje = "Se ha eliminado correctamente la editorial";
        //    }
        //    else
        //    {
        //        ViewBag.Mensaje = "No se eliminado correctamente la editorial";
        //    }
        //    return View("Modal");
        //}


        public JsonResult EstadoGetByIdPais(int IdPais)
        {
            ML.Result result = BL.Estado.GetByIdPais(IdPais);

            return Json(result.Objects);
        }


        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = BL.Municipio.GetByIdEstado(IdEstado);

            return Json(result.Objects);
        }


        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.GetByIdMunicipio(IdMunicipio);

            return Json(result.Objects);
        }

    }
}
