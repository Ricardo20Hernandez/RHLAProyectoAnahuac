using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Pais
    {
       public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = (from pais in context.Pais
                                 select new
                                 {
                                     IdPais = pais.IdPais,
                                     Nombre = pais.Nombre
                                 }).ToList();

                    if(query.Count() > 0 )
                    {
                        result.Objects = new List<object>();    
                        foreach(var paises in query )
                        {
                            ML.Pais pais = new ML.Pais();
                            pais.IdPais = paises.IdPais;
                            pais.Nombre = paises.Nombre;

                            result.Objects.Add(pais);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                //throw;
            }
            return result;  
        }
    }
}
