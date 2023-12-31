using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace BL
{
    public class Idioma
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = (from idioma in context.Idiomas
                                 select new
                                 {
                                     IdIdioma =  idioma.IdIdioma,
                                     Nombre = idioma.Nombre
                                 }).ToList();

                    if(query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach(var idiomas in query)
                        {
                            ML.Idioma idioma = new ML.Idioma();

                            idioma.IdIdioma = idiomas.IdIdioma;
                            idioma.Nombre = idiomas.Nombre;

                            result.Objects.Add(idioma);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Error en la consulta realizada en la BD" + result.Ex;
                //throw;
            }
            return result;
        }
    }
}
