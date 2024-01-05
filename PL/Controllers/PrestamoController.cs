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

namespace PL.Controllers
{
    public class PrestamoController : Controller
    {//

        public IActionResult MediosPrestados(string Id)
        {

            ML.DetallePrestamo medio = new ML.DetallePrestamo();
            ClaimsPrincipal principal = new ClaimsPrincipal();
            //  string id = principal.FindFirstValue.ToString();
            //  prestamo.Id = User.getUserId();

            // ClaimsPrincipal principal2 = new ClaimsPrincipal();
            // ;
            //va a ser con el id que reciba 

            // ML.Result result = BL.Prestamo.LibrosPrestados(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ML.Result result = BL.Prestamo.LibrosPrestados(Id);
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
        public IActionResult GetAll()
        {

            ML.Medio medio = new ML.Medio();
            ML.UserIdentity userIdentity = new ML.UserIdentity();
            ML.Result result = BL.Medio.GetAll();
            ML.Result result1 = BL.IdentityUser.GetAll();   
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
            if (result1.Correct)
            {
               userIdentity.IdentityUsers = result1.Objects;
            }
            else
            {
                ViewBag.Mensaje = "Sin medios registrados aún";
            }
            mediosYUsuarios.UserIdentity = userIdentity;
            mediosYUsuarios.Medio = medio;


            return View(mediosYUsuarios);
           
        }

      
       [HttpPost]

        public IActionResult SacarEnPrestamo(int IdMedio,ML.MediosYUsuarios mediosYUsuarios)
        {
            ML.DetallePrestamo detallePrestamo = new ML.DetallePrestamo();
            ML.Result result = BL.Medio.GetById(IdMedio);
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
            return View(detallePrestamo);

           

        }

        public IActionResult Devolver(int IdMedio)
        { ClaimsPrincipal principal = new ClaimsPrincipal();
            
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
            detallePrestamo.Prestamo.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ML.Result result = new ML.Result();
           
            result = BL.DetallePrestamo.DevolverMedio(detallePrestamo);
            if(result.Correct)
            {
                ViewBag.Mensaje = "Medio devuelto Correctamente el dia de" + detallePrestamo.FechaEntregaReal;
            }
       

            return View("Modal");


        }

        //devolver el prestamo 


    }
}
