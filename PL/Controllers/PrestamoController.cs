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


        public IActionResult SacarEnPrestamo(int IdMedio)
        {

            ML.Result result = BL.Medio.GetById(IdMedio);
            ML.Medio medio = new ML.Medio();
            if (result.Correct)
            {
                medio = (ML.Medio)result.Object;
            }
            ML.Prestamo prestamo = new ML.Prestamo();
            // se obtiene el id del usuario4
            ClaimsPrincipal principal = new ClaimsPrincipal();
            //  string id = principal.FindFirstValue.ToString();
            //  prestamo.Id = User.getUserId();

            // ClaimsPrincipal principal2 = new ClaimsPrincipal();
            prestamo.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            prestamo.Medio = new ML.Medio();
            prestamo.Medio = medio;
            prestamo.FechaSalida = DateTime.Today.ToString();
            prestamo.FechaEntregaEsperada = DateTime.Today.AddDays(7).ToString();

            ML.Result resultPrestamo = new ML.Result();
           // resultPrestamo = BL.Prestamo.NuevoPrestamo(prestamo);
            ML.Result resultRestarInventario = new ML.Result();
           // resultRestarInventario = BL.Prestamo.RestarInventario(IdMedio);
            ML.DetallePrestamo detallePrestamo = new ML.DetallePrestamo();
            detallePrestamo.Prestamo = new ML.Prestamo();
            detallePrestamo.Prestamo = prestamo;
           
            //medio.TipoMedio.TipoMedios = resultTipoMedios.Objects;
            //medio.Editorial.Editoriales = resultEditoriales.Objects;
            //medio.Idioma.Idiomas = resultIdiomas.Objects;
            //medio.Autor.Autores = resultAutores.Objects;
            //ViewBag.Accion = "Actualizar";
        
            return View(detallePrestamo);

           

        }
    }
}
