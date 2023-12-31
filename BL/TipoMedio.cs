using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TipoMedio
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = (from tipoMedio in context.TipoMedios
                                 select new
                                 {
                                     IdTipoMedio = tipoMedio.IdTipoMedio,
                                     Nombre = tipoMedio.Nombre
                                 }).ToList();

                    if(query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var tipoMedios in query)
                        {
                            ML.TipoMedio tipoMedio = new ML.TipoMedio();

                            tipoMedio.IdTipoMedio = tipoMedios.IdTipoMedio;
                            tipoMedio.Nombre = tipoMedios.Nombre;

                            result.Objects.Add(tipoMedio);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Error al realizar la consulta en la BD" + result.Ex;
                //throw;
            }
            return result;
        }
    }
}
