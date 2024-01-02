using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Medio
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = (from medio in context.Medios
                                 join tipoMedio in context.TipoMedios
                                     on medio.IdTipoMedio equals tipoMedio.IdTipoMedio into tipoMedioJoin
                                 from tipoMedio in tipoMedioJoin.DefaultIfEmpty()
                                 join editorial in context.Editorials
                                     on medio.IdEditorial equals editorial.IdEditorial into editorialJoin
                                 from editorial in editorialJoin.DefaultIfEmpty()
                                 join idioma in context.Idiomas
                                     on medio.IdIdioma equals idioma.IdIdioma into idiomaJoin
                                 from idioma in idiomaJoin.DefaultIfEmpty()
                                 join autor in context.Autors
                                     on medio.IdAutor equals autor.IdAutor into autorJoin
                                 from autor in autorJoin.DefaultIfEmpty()
                                 select new
                                 {
                                     IdMedio = medio.IdMedio,
                                     Titulo = medio.Titulo,
                                     NumeroPaginas = medio.NumeroPaginas,
                                     Duracion = medio.Duracion,
                                     CantidadDisponible = medio.CantidadDisponible,
                                     Imagen = medio.Imagen,
                                     IdTipoMedio = tipoMedio != null ? tipoMedio.IdTipoMedio : 0,
                                     NombreTipoMedio = tipoMedio != null ? tipoMedio.Nombre : "Sin Tipo de Medio",
                                     IdEditorial = editorial != null ? editorial.IdEditorial : 0,
                                     NombreEditorial = editorial != null ? editorial.Nombre : "Sin Editorial",
                                     IdIdioma = idioma != null ? idioma.IdIdioma : 0,
                                     NombreIdioma = idioma != null ? idioma.Nombre : "Sin Idioma",
                                     IdAutor = autor != null ? autor.IdAutor : 0,
                                     NombreAutor = autor != null ? $"{autor.Nombre} {autor.ApellidoPaterno} {autor.ApellidoMaterno}" : "Sin Autor"
                                 }).ToList();


                    if (query.Count() > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var medios in query)
                        {
                            ML.Medio medio = new ML.Medio();

                            medio.IdMedio = medios.IdMedio;
                            medio.Titulo = medios.Titulo;
                            medio.NumeroPaginas = (medios.NumeroPaginas == null) ? null : medios.NumeroPaginas.Value;
                            medio.Duracion = (medios.Duracion == null) ? null : medios.Duracion.Value;
                            medio.CantidadDisponible = medios.CantidadDisponible;
                            medio.Imagen = medios.Imagen;

                            medio.TipoMedio = new ML.TipoMedio();
                            medio.TipoMedio.IdTipoMedio = medios.IdTipoMedio;
                            medio.TipoMedio.Nombre = medios.NombreTipoMedio;
                            medio.Editorial = new ML.Editorial();
                            medio.Editorial.IdEditorial = medios.IdEditorial;
                            medio.Editorial.Nombre = medios.NombreEditorial;
                            medio.Idioma = new ML.Idioma();
                            medio.Idioma.IdIdioma = medios.IdIdioma;
                            medio.Idioma.Nombre = medios.NombreIdioma;
                            medio.Autor = new ML.Autor();
                            medio.Autor.IdAutor = medios.IdAutor;
                            medio.Autor.Nombre = medios.NombreAutor; //Revisar este

                            result.Objects.Add(medio);
                        }
                        result.Correct = true;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Error al hacer la consulta en la BD" + result.Ex;
                //throw;
            }
            return result;
        }

        public static ML.Result GetById(int IdMedio)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = (from medio in context.Medios
                                 join tipoMedio in context.TipoMedios
                                     on medio.IdTipoMedio equals tipoMedio.IdTipoMedio into tipoMedioJoin
                                 from tipoMedio in tipoMedioJoin.DefaultIfEmpty()
                                 join editorial in context.Editorials
                                     on medio.IdEditorial equals editorial.IdEditorial into editorialJoin
                                 from editorial in editorialJoin.DefaultIfEmpty()
                                 join idioma in context.Idiomas
                                     on medio.IdIdioma equals idioma.IdIdioma into idiomaJoin
                                 from idioma in idiomaJoin.DefaultIfEmpty()
                                 join autor in context.Autors
                                     on medio.IdAutor equals autor.IdAutor into autorJoin
                                 from autor in autorJoin.DefaultIfEmpty()
                                 where medio.IdMedio == IdMedio
                                 select new
                                 {
                                     IdMedio = medio.IdMedio,
                                     Titulo = medio.Titulo,
                                     NumeroPaginas = medio.NumeroPaginas,
                                     Duracion = medio.Duracion,
                                     CantidadDisponible = medio.CantidadDisponible,
                                     Imagen = medio.Imagen,
                                     IdTipoMedio = tipoMedio != null ? tipoMedio.IdTipoMedio : 0,
                                     NombreTipoMedio = tipoMedio.Nombre,
                                     IdEditorial = editorial != null ? editorial.IdEditorial : 0,
                                     NombreEditorial = editorial.Nombre,
                                     IdIdioma = idioma != null ? idioma.IdIdioma : 0,
                                     NombreIdioma = idioma.Nombre,
                                     IdAutor = autor != null ? autor.IdAutor : 0,
                                     NombreAutor = $"{autor.Nombre} {autor.ApellidoPaterno} {autor.ApellidoMaterno}"
                                 });

                    if (query != null)
                    {
                        ML.Medio medio = new ML.Medio();
                        var medios = query.FirstOrDefault();

                        medio.IdMedio = medios.IdMedio;
                        medio.Titulo = medios.Titulo;
                        medio.NumeroPaginas = (medios.NumeroPaginas == null) ? null : medios.NumeroPaginas.Value;
                        medio.Duracion = (medios.Duracion == null) ? null : medios.Duracion.Value;
                        medio.CantidadDisponible = medios.CantidadDisponible;
                        medio.Imagen = medios.Imagen;

                        medio.TipoMedio = new ML.TipoMedio();
                        medio.TipoMedio.IdTipoMedio = medios.IdTipoMedio;
                        medio.TipoMedio.Nombre = medios.NombreTipoMedio;
                        medio.Editorial = new ML.Editorial();
                        medio.Editorial.IdEditorial = medios.IdEditorial;
                        medio.Editorial.Nombre = medios.NombreEditorial;
                        medio.Idioma = new ML.Idioma();
                        medio.Idioma.IdIdioma = medios.IdIdioma;
                        medio.Idioma.Nombre = medios.NombreIdioma;
                        medio.Autor = new ML.Autor();
                        medio.Autor.IdAutor = medios.IdAutor;
                        medio.Autor.Nombre = medios.NombreAutor; //Revisar este

                        result.Object = medio;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Error al hacer la consulta en la BD" + result.Ex;
                //throw;
                //throw;
            }
            return result;
        }


        public static ML.Result Add(ML.Medio medio)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    DL.Medio media = new DL.Medio();

                    media.Titulo = medio.Titulo;
                    media.NumeroPaginas = medio.NumeroPaginas;
                    media.Duracion = medio.Duracion;
                    media.CantidadDisponible = medio.CantidadDisponible.Value;
                    media.Imagen = medio.Imagen;
                    media.IdTipoMedio = medio.TipoMedio.IdTipoMedio;
                    media.IdEditorial = medio.Editorial.IdEditorial;
                    media.IdIdioma = medio.Idioma.IdIdioma;
                    media.IdAutor = medio.Autor.IdAutor;

                    context.Medios.Add(media);
                    int RowsAffected = context.SaveChanges();

                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Hubo un error al agregar el medio";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Problemas al hacer la inserción a la BD" + result.Ex;
                throw;
            }
            return result;
        }

        public static ML.Result Update(ML.Medio medio)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = (from medios in context.Medios
                                 where medios.IdMedio == medio.IdMedio
                                 select medios).SingleOrDefault();

                    if (query != null) {

                        query.Titulo = medio.Titulo;
                        query.NumeroPaginas = medio.NumeroPaginas;
                        query.Duracion = medio.Duracion;
                        query.CantidadDisponible = medio.CantidadDisponible.Value;
                        query.Imagen = medio.Imagen;
                        query.IdTipoMedio = medio.TipoMedio.IdTipoMedio;
                        query.IdEditorial = medio.Editorial.IdEditorial;
                        query.IdIdioma = medio.Idioma.IdIdioma;
                        query.IdAutor = medio.Autor.IdAutor;

                        int RowsAffected = context.SaveChanges();

                        if(RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Hubo error al actulizar al actualizar el medio";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Problemas al hacer la inserción a la BD" + result.Ex;
                //throw;
            }

            return result;
        }

        public static ML.Result Delete(int IdMedio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DeleteMedio '{IdMedio}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al eliminar el medio";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Problemas al hacer la eliminación" + result.Ex;
                //throw;
            }
            return result;
        }
    }
}
