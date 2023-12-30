using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static ML.Result GetByIdPais(int IdPais)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = (from estado in context.Estados
                                 where estado.IdPais == IdPais
                                 select new
                                 {
                                     IdEstado = estado.IdEstado,
                                     Nombre = estado.Nombre,
                                     IdPais = estado.IdPais

                                 }).ToList();   
                    if(query.Count > 0 )
                    {
                        result.Objects = new List<object>();
                        foreach (var estados in query)
                        {
                            ML.Estado estado = new ML.Estado();
                            estado.IdEstado = estados.IdEstado;
                            estado.Nombre = estados.Nombre;
                            estado.Pais = new ML.Pais();
                            estado.Pais.IdPais = estados.IdPais.Value;

                            result.Objects.Add(estado);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Ocurrio un error durante la consulta" + result.Ex.Message;
                //throw;
            }
            return result;
        }
    }
}
