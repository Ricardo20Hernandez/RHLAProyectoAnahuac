using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Abstractions;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

using System.Text.Encodings.Web;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using NuGet.Protocol;
using ML;
using Microsoft.AspNetCore.Authorization;

namespace PL.Controllers
{
    public class PrestamoController : Controller
    {//

        //para obtener el id en prestar
        [Authorize(Roles = "Administrador")]
        public IActionResult GetAll2(int IdMedio)

        {
            ML.Result result1 = BL.IdentityUser.GetAll();
            ML.UserIdentity userIdentity1 = new ML.UserIdentity();
            //userIdentity1.IdentityUsers = result1.Objects;
            ML.MediosYUsuarios mediosYUsuarios = new ML.MediosYUsuarios();
            mediosYUsuarios.Medio = new ML.Medio();
            mediosYUsuarios.Medio.IdMedio = IdMedio;
            mediosYUsuarios.UserIdentity = userIdentity1;
            mediosYUsuarios.UserIdentity.IdentityUsers = result1.Objects;
            return View(mediosYUsuarios);
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult GetAll3()

        {
            ML.Result result1 = BL.IdentityUser.GetAll();
            ML.UserIdentity userIdentity1 = new ML.UserIdentity();
            //userIdentity1.IdentityUsers = result1.Objects;
            ML.MediosYUsuarios mediosYUsuarios = new ML.MediosYUsuarios();
            mediosYUsuarios.Medio = new ML.Medio();
            //mediosYUsuarios.Medio.IdMedio = IdMedio;
            mediosYUsuarios.UserIdentity = userIdentity1;
            mediosYUsuarios.UserIdentity.IdentityUsers = result1.Objects;
            return View(mediosYUsuarios);
        }

        public IActionResult PrestamoGetAll3()

        {
            ML.Result result1 = BL.IdentityUser.GetAll();
            ML.UserIdentity userIdentity1 = new ML.UserIdentity();
            //userIdentity1.IdentityUsers = result1.Objects;
            ML.MediosYUsuarios mediosYUsuarios = new ML.MediosYUsuarios();
            mediosYUsuarios.Medio = new ML.Medio();
            //mediosYUsuarios.Medio.IdMedio = IdMedio;
            mediosYUsuarios.UserIdentity = userIdentity1;
            mediosYUsuarios.UserIdentity.IdentityUsers = result1.Objects;
            return Ok(mediosYUsuarios);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult GetAll4(int IdMedio)

        {
            ML.Result result1 = BL.IdentityUser.GetAll();
            ML.UserIdentity userIdentity1 = new ML.UserIdentity();
            //userIdentity1.IdentityUsers = result1.Objects;
            ML.MediosYUsuarios mediosYUsuarios = new ML.MediosYUsuarios();
            mediosYUsuarios.Medio = new ML.Medio();
            mediosYUsuarios.Medio.IdMedio = IdMedio;
            mediosYUsuarios.UserIdentity = userIdentity1;
            mediosYUsuarios.UserIdentity.IdentityUsers = result1.Objects;
            return View(mediosYUsuarios);
        }

        public IActionResult PrestamoGetAll4()

        {
            ML.Result result1 = BL.IdentityUser.GetAll();
            ML.UserIdentity userIdentity1 = new ML.UserIdentity();
            //userIdentity1.IdentityUsers = result1.Objects;
            ML.MediosYUsuarios mediosYUsuarios = new ML.MediosYUsuarios();
            mediosYUsuarios.Medio = new ML.Medio();
            //mediosYUsuarios.Medio.IdMedio = IdMedio;
            mediosYUsuarios.UserIdentity = userIdentity1;
            mediosYUsuarios.UserIdentity.IdentityUsers = result1.Objects;
            return Ok(mediosYUsuarios);
        }




        [Authorize(Roles = "Administrador")]
        public IActionResult MediosPrestados(ML.MediosYUsuarios mediosYUsuarios)
        {

            ML.DetallePrestamo medio = new ML.DetallePrestamo();
            //ClaimsPrincipal principal = new ClaimsPrincipal();
            //  string id = principal.FindFirstValue.ToString();
            //  prestamo.Id = User.getUserId();

            // ClaimsPrincipal principal2 = new ClaimsPrincipal();
            // ;
            //va a ser con el id que reciba 

            // ML.Result result = BL.Prestamo.LibrosPrestados(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ML.Result result = BL.Prestamo.LibrosPrestados(mediosYUsuarios.UserIdentity.IdUsuario);
            if (result.Correct)
            {
                medio.DetallePrestamos = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = "Sin medios registrados aún";
            }

            return View("SacarEnPrestamo",medio);

        }

        public IActionResult MediosPrestadosAjax([FromBody] ML.PrestamoEntrada prestamoEntrada)
        {

            ML.DetallePrestamo medios = new ML.DetallePrestamo();
            //ClaimsPrincipal principal = new ClaimsPrincipal();
            //  string id = principal.FindFirstValue.ToString();
            //  prestamo.Id = User.getUserId();

            // ClaimsPrincipal principal2 = new ClaimsPrincipal();
            // ;
            //va a ser con el id que reciba 

            // ML.Result result = BL.Prestamo.LibrosPrestados(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ML.Result result = BL.Prestamo.LibrosPrestados(prestamoEntrada.idUsuario);
            if (result.Correct)
            {
                medios.DetallePrestamos = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = "Sin medios registrados aún";
            }
            // aqui tiene que ser ok 
            return Ok(medios);

        }

        public IActionResult MediosPrestadosDevolverAjax([FromBody] ML.PrestamoEntrada prestamoEntrada)
        {

            ML.DetallePrestamo medios = new ML.DetallePrestamo();
            //ClaimsPrincipal principal = new ClaimsPrincipal();
            //  string id = principal.FindFirstValue.ToString();
            //  prestamo.Id = User.getUserId();

            // ClaimsPrincipal principal2 = new ClaimsPrincipal();
            // ;
            //va a ser con el id que reciba 

            // ML.Result result = BL.Prestamo.LibrosPrestados(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ML.Result result = BL.Prestamo.LibrosPrestados(prestamoEntrada.idUsuario);
            if (result.Correct)
            {
                medios.DetallePrestamos = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = "Sin medios registrados aún";
            }
            // aqui tiene que ser ok 
            return Ok(medios);

        }



        [Authorize(Roles = "Usuario")]
        public IActionResult MisMediosPrestados()
        {

            ML.DetallePrestamo medio = new ML.DetallePrestamo();
            ClaimsPrincipal principal = new ClaimsPrincipal();
            //  string id = principal.FindFirstValue.ToString();
            //  prestamo.Id = User.getUserId();

            // ClaimsPrincipal principal2 = new ClaimsPrincipal();
            // ;
            //va a ser con el id que reciba 

             ML.Result result = BL.Prestamo.LibrosPrestados(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //ML.Result result = BL.Prestamo.LibrosPrestados(mediosYUsuarios.UserIdentity.IdUsuario);
            if (result.Correct)
            {
                medio.DetallePrestamos = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = "Sin medios registrados aún";
            }

            return View("SacarEnPrestamo", medio);

        }
        [Authorize(Roles = "Administrador")]
        public IActionResult MediosPrestadosDevolver(ML.MediosYUsuarios mediosYUsuarios)
        {

            ML.DetallePrestamo medio = new ML.DetallePrestamo();
            medio.Prestamo = new ML.Prestamo();
            medio.Prestamo.Id = mediosYUsuarios.UserIdentity.IdUsuario;
            //ClaimsPrincipal principal = new ClaimsPrincipal();
            //  string id = principal.FindFirstValue.ToString();
            //  prestamo.Id = User.getUserId();

            // ClaimsPrincipal principal2 = new ClaimsPrincipal();
            // ;
            //va a ser con el id que reciba 

            // ML.Result result = BL.Prestamo.LibrosPrestados(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ML.Result result = BL.Prestamo.LibrosPrestados(mediosYUsuarios.UserIdentity.IdUsuario);
            if (result.Correct)
            {
                medio.DetallePrestamos = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = "Sin medios registrados aún";
            }

            return View("MediosPrestados", medio);

        }
        public IActionResult GetAll()
        {

            ML.Medio medio = new ML.Medio();
           // ML.UserIdentity userIdentity = new ML.UserIdentity();
            ML.Result result = BL.Medio.GetAll();
            //ML.Result result1 = BL.IdentityUser.GetAll();   
            ML.MediosYUsuarios mediosYUsuarios = new ML.MediosYUsuarios();
            mediosYUsuarios.UserIdentity = new ML.UserIdentity();   
            mediosYUsuarios.Medio = new ML.Medio(); 

           // ML.Result result = BL.Prestamo.LibrosDisponibles(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (result.Correct)
            {
                medio.Medios = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = "Sin medios registrados aún";
            }
            //if (result1.Correct)
            //{
            //   userIdentity.IdentityUsers = result1.Objects;
            //}
            //else
            //{
            //    ViewBag.Mensaje = "Sin medios registrados aún";
            //}
            //mediosYUsuarios.UserIdentity = userIdentity;
            mediosYUsuarios.Medio = medio;

           // HttpContext.Session.SetString("ID", mediosYUsuarios.UserIdentity.IdUsuario);
            return View(mediosYUsuarios);
           
        }







        [Authorize(Roles = "Administrador")]
        public IActionResult SacarEnPrestamo(ML.MediosYUsuarios mediosYUsuarios)
        {
            
            ML.DetallePrestamo detallePrestamo = new ML.DetallePrestamo();
            ML.Result result = BL.Medio.GetById(mediosYUsuarios.Medio.IdMedio);
            ML.Medio medio = new ML.Medio();
            if (result.Correct)
            {
                medio = (ML.Medio)result.Object;
            }
            ML.Prestamo prestamo = new ML.Prestamo();
            // se obtiene el id del usuario4
           // ClaimsPrincipal principal = new ClaimsPrincipal();
            //  string id = principal.FindFirstValue.ToString();
            //  prestamo.Id = User.getUserId();

            // ClaimsPrincipal principal2 = new ClaimsPrincipal();
            if (medio.CantidadDisponible > 0)
            {
                prestamo.Id = mediosYUsuarios.UserIdentity.IdUsuario;
              // prestamo.Id = HttpContext.Session.GetString("ID");
                // prestamo.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                prestamo.Medio = new ML.Medio();
            prestamo.Medio = medio;
            prestamo.FechaSalida = DateTime.Today.ToString();
            prestamo.FechaEntregaEsperada = DateTime.Today.AddDays(7).ToString();

            ML.Result resultPrestamo = new ML.Result();
           resultPrestamo = BL.Prestamo.NuevoPrestamo(prestamo);
         //   ML.Result resultRestarInventario = new ML.Result();
        // resultRestarInventario = BL.Prestamo.RestarInventario(IdMedio,prestamo.Id);
            
            detallePrestamo.Prestamo = new ML.Prestamo();
            detallePrestamo.Prestamo = prestamo;
           ML.Result librosPrestados = new ML.Result();
            librosPrestados = BL.Prestamo.LibrosPrestados(prestamo.Id);
            detallePrestamo.DetallePrestamos = librosPrestados.Objects;
            //medio.TipoMedio.TipoMedios = resultTipoMedios.Objects;
            //medio.Editorial.Editoriales = resultEditoriales.Objects;
            //medio.Idioma.Idiomas = resultIdiomas.Objects;
            //medio.Autor.Autores = resultAutores.Objects;
            if(!resultPrestamo.Correct)
            {
                ViewBag.Accion = resultPrestamo.ErrorMessage;
            }
            ML.Result tipoMedio = new ML.Result();}
            
           // tipoMedio = BL.TipoMedio.GetAll();
           // detallePrestamo.Prestamo.Medio.TipoMedio.TipoMedios = tipoMedio.Objects;
            //detallePrestamo.Prestamo.Medio.TipoMedio
           // mediosYUsuarios.
            return View(detallePrestamo);

           

        }

        //para pasar un objeto atravez de una peticion ajax se tiene que
        //crear el modelo exacto para la peticion 
        //ademas de untilizar la etiqueta [FromBody]
        // ML.PrestamoEntrada prestamoEntrada
        public IActionResult SacarEnPrestamoAjax([FromBody] ML.PrestamoEntrada prestamoEntrada)
        {

            ML.DetallePrestamo detallePrestamo = new ML.DetallePrestamo();
            ML.Result result = BL.Medio.GetById(prestamoEntrada.idMedio);
            ML.Medio medio = new ML.Medio();
            if (result.Correct)
            {
                medio = (ML.Medio)result.Object;
            }
            ML.Prestamo prestamo = new ML.Prestamo();
            // se obtiene el id del usuario4
            // ClaimsPrincipal principal = new ClaimsPrincipal();
            //  string id = principal.FindFirstValue.ToString();
            //  prestamo.Id = User.getUserId();

            // ClaimsPrincipal principal2 = new ClaimsPrincipal();
            if (medio.CantidadDisponible > 0)
            {
                prestamo.Id = prestamoEntrada.idUsuario;
                // prestamo.Id = HttpContext.Session.GetString("ID");
                // prestamo.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                prestamo.Medio = new ML.Medio();
                prestamo.Medio = medio;
                prestamo.FechaSalida = DateTime.Today.ToString();
                prestamo.FechaEntregaEsperada = DateTime.Today.AddDays(7).ToString();

                ML.Result resultPrestamo = new ML.Result();
                resultPrestamo = BL.Prestamo.NuevoPrestamo(prestamo);
                //   ML.Result resultRestarInventario = new ML.Result();
                // resultRestarInventario = BL.Prestamo.RestarInventario(IdMedio,prestamo.Id);

                detallePrestamo.Prestamo = new ML.Prestamo();
                detallePrestamo.Prestamo = prestamo;
                ML.Result librosPrestados = new ML.Result();
                librosPrestados = BL.Prestamo.LibrosPrestados(prestamo.Id);
                detallePrestamo.DetallePrestamos = librosPrestados.Objects;
                //medio.TipoMedio.TipoMedios = resultTipoMedios.Objects;
                //medio.Editorial.Editoriales = resultEditoriales.Objects;
                //medio.Idioma.Idiomas = resultIdiomas.Objects;
                //medio.Autor.Autores = resultAutores.Objects;
                if (!resultPrestamo.Correct)
                {
                    ViewBag.Accion = resultPrestamo.ErrorMessage;
                }
                ML.Result tipoMedio = new ML.Result();
            }

            // tipoMedio = BL.TipoMedio.GetAll();
            // detallePrestamo.Prestamo.Medio.TipoMedio.TipoMedios = tipoMedio.Objects;
            //detallePrestamo.Prestamo.Medio.TipoMedio
            // mediosYUsuarios.
            //aqui se tiene que reecostruir la vista del detalle prestamo con ajax
            return Ok(detallePrestamo);



        }



        [Authorize(Roles = "Administrador")]
        public IActionResult Devolver(int IdMedio,string Id)
        {// ClaimsPrincipal principal = new ClaimsPrincipal();
            
            //  string id = principal.FindFirstValue.ToString();
            //  prestamo.Id = User.getUserId();

            // ClaimsPrincipal principal2 = new ClaimsPrincipal();
            //prestamo.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ML.DetallePrestamo detallePrestamo = new ML.DetallePrestamo();
            detallePrestamo.Prestamo = new ML.Prestamo();
            detallePrestamo.Prestamo.Medio = new ML.Medio();

            detallePrestamo.FechaEntregaReal = DateTime.Today.ToString();
            detallePrestamo.Entregado = true;
            detallePrestamo.Prestamo.Medio.IdMedio = IdMedio;
            detallePrestamo.Prestamo.Id = Id;
            ML.Result result = new ML.Result();
           
            result = BL.DetallePrestamo.DevolverMedio(detallePrestamo);
            if(result.Correct)
            {
                ViewBag.Mensaje = "Medio devuelto Correctamente el dia de" + detallePrestamo.FechaEntregaReal;
            }
       

            return View("Modal");


        }

        public IActionResult DevolverElPrestamoAjax([FromBody] ML.PrestamoEntrada prestamoEntrada)
        {// ClaimsPrincipal principal = new ClaimsPrincipal();

            //  string id = principal.FindFirstValue.ToString();
            //  prestamo.Id = User.getUserId();

            // ClaimsPrincipal principal2 = new ClaimsPrincipal();
            //prestamo.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ML.DetallePrestamo detallePrestamo = new ML.DetallePrestamo();
            detallePrestamo.Prestamo = new ML.Prestamo();
            detallePrestamo.Prestamo.Medio = new ML.Medio();

            detallePrestamo.FechaEntregaReal = DateTime.Today.ToString();
            detallePrestamo.Entregado = true;
            detallePrestamo.Prestamo.Medio.IdMedio = prestamoEntrada.idMedio;
            detallePrestamo.Prestamo.Id = prestamoEntrada.idUsuario;
            ML.Result result = new ML.Result();

            result = BL.DetallePrestamo.DevolverMedio(detallePrestamo);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Medio devuelto Correctamente el dia de" + detallePrestamo.FechaEntregaReal;
            }


            return View("Modal");


        }





        //simulacion de servicio desde el controlador 

        public IActionResult PrestamoGetAll()
        {

            ML.Medio medio = new ML.Medio();
            // ML.UserIdentity userIdentity = new ML.UserIdentity();
            ML.Result result = BL.Medio.GetAll();
            //ML.Result result1 = BL.IdentityUser.GetAll();   
            ML.MediosYUsuarios mediosYUsuarios = new ML.MediosYUsuarios();
            mediosYUsuarios.UserIdentity = new ML.UserIdentity();
            mediosYUsuarios.Medio = new ML.Medio();

            // ML.Result result = BL.Prestamo.LibrosDisponibles(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (result.Correct)
            {
                medio.Medios = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = "Sin medios registrados aún";
            }
            //if (result1.Correct)
            //{
            //   userIdentity.IdentityUsers = result1.Objects;
            //}
            //else
            //{
            //    ViewBag.Mensaje = "Sin medios registrados aún";
            //}
            //mediosYUsuarios.UserIdentity = userIdentity;
            mediosYUsuarios.Medio = medio;

            // HttpContext.Session.SetString("ID", mediosYUsuarios.UserIdentity.IdUsuario);
            return Ok(mediosYUsuarios);

            //ML.Result result = BL.Editorial.GetAll();

            //if (result.Correct)
            //{
            //    return Ok(result);
            //}
            //else
            //{
            //    return BadRequest(result);
            //}
        }


        public IActionResult PrestamoGetAll2(int idMedio)

        {
            ML.Result result1 = BL.IdentityUser.GetAll();
            ML.UserIdentity userIdentity1 = new ML.UserIdentity();
            //userIdentity1.IdentityUsers = result1.Objects;
            ML.MediosYUsuarios mediosYUsuarios = new ML.MediosYUsuarios();
            mediosYUsuarios.Medio = new ML.Medio();
            mediosYUsuarios.Medio.IdMedio = idMedio;
            mediosYUsuarios.UserIdentity = userIdentity1;
            mediosYUsuarios.UserIdentity.IdentityUsers = result1.Objects;
            return Ok(mediosYUsuarios);
        }

        public IActionResult UsuariosGetAll()
        {
            ML.Result result = BL.IdentityUser.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        //devolver el prestamo 


    }
}
