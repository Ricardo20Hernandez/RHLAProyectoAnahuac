using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = (from colonia in context.Colonia
                                 where colonia.IdMunicipio == IdMunicipio
                                 select new
                                 {
                                     IdColonia = colonia.IdColonia,
                                     Nombre = colonia.Nombre,
                                     IdMunicipio = colonia.IdMunicipio

                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var colonias in query)
                        {
                            ML.Colonia colonia = new ML.Colonia();
                            colonia.IdColonia = colonias.IdColonia;
                            colonia.Nombre = colonias.Nombre;
                            colonia.Municipio = new ML.Municipio();
                            colonia.Municipio.IdMunicipio = colonias.IdMunicipio.Value;

                            result.Objects.Add(colonia);
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
