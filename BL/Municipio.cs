using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result GetByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = (from municipio in context.Municipios
                                 where municipio.IdEstado == IdEstado
                                 select new
                                 {
                                     IdMunicipio = municipio.IdMunicipio,
                                     Nombre = municipio.Nombre,
                                     IdEstado = municipio.IdEstado

                                 }).ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var municipios in query)
                        {
                            ML.Municipio municipio = new ML.Municipio();
                            municipio.IdMunicipio = municipios.IdMunicipio;
                            municipio.Nombre = municipios.Nombre;
                            municipio.Estado = new ML.Estado();
                            municipio.Estado.IdEstado = municipios.IdEstado.Value;

                            result.Objects.Add(municipio);
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

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = (from municipio in context.Municipios
                                 select new
                                 {
                                     IdMunicipio = municipio.IdMunicipio,
                                     Nombre = municipio.Nombre
                                 }).ToList();

                    if (query.Count() > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var municipios in query)
                        {
                            ML.Municipio municipio = new ML.Municipio();
                            municipio.IdMunicipio = municipios.IdMunicipio;
                            municipio.Nombre = municipios.Nombre;

                            result.Objects.Add(municipio);
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
