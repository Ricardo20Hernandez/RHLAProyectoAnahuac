using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Editorial
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = (from editorial in context.Editorials
                                 join direccion in context.Direccions
                                 on editorial.IdEditorial equals direccion.IdEditorial
                                 join colonia in context.Colonia
                                 on direccion.IdColonia equals colonia.IdColonia
                                 join municipio in context.Municipios
                                 on colonia.IdMunicipio equals municipio.IdMunicipio
                                 join estado in context.Estados
                                 on municipio.IdEstado equals estado.IdEstado
                                 join pais in context.Pais
                                 on estado.IdPais equals pais.IdPais
                                 select new
                                 {
                                     IdEditorial = editorial.IdEditorial,
                                     Nombre = editorial.Nombre,
                                     Telefono = editorial.Telefono,
                                     Emaill = editorial.Email,
                                     IdDireccion = direccion.IdDireccion,
                                     Calle = direccion.Calle,
                                     NumeroInterior = direccion.NumeroInterior,
                                     NumeroExterior = direccion.NumeroExterior,
                                     IdColonia = colonia.IdColonia,
                                     NombreColonia = colonia.Nombre,
                                     CodigoPostal = colonia.CodigoPostal,
                                     IdMunicipio = municipio.IdMunicipio,
                                     NombreMunicipio = municipio.Nombre,
                                     IdEstado = estado.IdEstado,
                                     NombreEstado = estado.Nombre,
                                     IdPais = pais.IdPais,
                                     NombrePais = pais.Nombre
                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var editoriales in query)
                        {
                            ML.Editorial editorial = new ML.Editorial();
                            editorial.IdEditorial = editoriales.IdEditorial;
                            editorial.Nombre = editoriales.Nombre;
                            editorial.Telefono = editoriales.Telefono;
                            editorial.Email = editoriales.Emaill;
                            editorial.Direccion = new ML.Direccion();
                            editorial.Direccion.IdDireccion = editoriales.IdDireccion;
                            editorial.Direccion.Calle = editoriales.Calle;
                            editorial.Direccion.NumeroInterior = editoriales.NumeroInterior;
                            editorial.Direccion.NumeroExterior = editoriales.NumeroExterior;
                            editorial.Direccion.Colonia = new ML.Colonia();
                            editorial.Direccion.Colonia.IdColonia = editoriales.IdColonia;
                            editorial.Direccion.Colonia.Nombre = editoriales.NombreColonia;
                            editorial.Direccion.Colonia.CodigoPostal = editoriales.CodigoPostal;
                            editorial.Direccion.Colonia.Municipio = new ML.Municipio();
                            editorial.Direccion.Colonia.Municipio.IdMunicipio = editoriales.IdMunicipio;
                            editorial.Direccion.Colonia.Municipio.Nombre = editoriales.NombreMunicipio;
                            editorial.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            editorial.Direccion.Colonia.Municipio.Estado.IdEstado = editoriales.IdEstado;
                            editorial.Direccion.Colonia.Municipio.Estado.Nombre = editoriales.NombreEstado;
                            editorial.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            editorial.Direccion.Colonia.Municipio.Estado.Pais.IdPais = editoriales.IdPais;
                            editorial.Direccion.Colonia.Municipio.Estado.Pais.Nombre = editoriales.NombrePais;

                            result.Objects.Add(editorial);
                        }
                        result.Correct = true;

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Ocurrio un error al realizar la consulta en la base de datos " + result.Ex;
                //throw;
            }
            return result;
        }


        public static ML.Result GetById(int IdEditorial)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = (from editorial in context.Editorials
                                 join direccion in context.Direccions
                                 on editorial.IdEditorial equals direccion.IdEditorial
                                 join colonia in context.Colonia
                                 on direccion.IdColonia equals colonia.IdColonia
                                 join municipio in context.Municipios
                                 on colonia.IdMunicipio equals municipio.IdMunicipio
                                 join estado in context.Estados
                                 on municipio.IdEstado equals estado.IdEstado
                                 join pais in context.Pais
                                 on estado.IdPais equals pais.IdPais
                                 where editorial.IdEditorial == IdEditorial
                                 select new
                                 {
                                     IdEditorial = editorial.IdEditorial,
                                     Nombre = editorial.Nombre,
                                     Telefono = editorial.Telefono,
                                     Emaill = editorial.Email,
                                     IdDireccion = direccion.IdDireccion,
                                     Calle = direccion.Calle,
                                     NumeroInterior = direccion.NumeroInterior,
                                     NumeroExterior = direccion.NumeroExterior,
                                     IdColonia = colonia.IdColonia,
                                     NombreColonia = colonia.Nombre,
                                     CodigoPostal = colonia.CodigoPostal,
                                     IdMunicipio = municipio.IdMunicipio,
                                     NombreMunicipio = municipio.Nombre,
                                     IdEstado = estado.IdEstado,
                                     NombreEstado = estado.Nombre,
                                     IdPais = pais.IdPais,
                                     NombrePais = pais.Nombre
                                 });

                    if (query != null)
                    {

                        ML.Editorial editorial = new ML.Editorial();
                        var editoriales = query.FirstOrDefault();

                        editorial.IdEditorial = editoriales.IdEditorial;
                        editorial.Nombre = editoriales.Nombre;
                        editorial.Telefono = editoriales.Telefono;
                        editorial.Email = editoriales.Emaill;
                        editorial.Direccion = new ML.Direccion();
                        editorial.Direccion.IdDireccion = editoriales.IdDireccion;
                        editorial.Direccion.Calle = editoriales.Calle;
                        editorial.Direccion.NumeroInterior = editoriales.NumeroInterior;
                        editorial.Direccion.NumeroExterior = editoriales.NumeroExterior;
                        editorial.Direccion.Colonia = new ML.Colonia();
                        editorial.Direccion.Colonia.IdColonia = editoriales.IdColonia;
                        editorial.Direccion.Colonia.Nombre = editoriales.NombreColonia;
                        editorial.Direccion.Colonia.CodigoPostal = editoriales.CodigoPostal;
                        editorial.Direccion.Colonia.Municipio = new ML.Municipio();
                        editorial.Direccion.Colonia.Municipio.IdMunicipio = editoriales.IdMunicipio;
                        editorial.Direccion.Colonia.Municipio.Nombre = editoriales.NombreMunicipio;
                        editorial.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        editorial.Direccion.Colonia.Municipio.Estado.IdEstado = editoriales.IdEstado;
                        editorial.Direccion.Colonia.Municipio.Estado.Nombre = editoriales.NombreEstado;
                        editorial.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        editorial.Direccion.Colonia.Municipio.Estado.Pais.IdPais = editoriales.IdPais;
                        editorial.Direccion.Colonia.Municipio.Estado.Pais.Nombre = editoriales.NombrePais;

                        result.Object = editorial;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Ocurrio un error al realizar la consulta en la base de datos " + result.Ex;
                //throw;
            }
            return result;
        }


        public static ML.Result Add(ML.Editorial editorial)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AddEditorial '{editorial.Nombre}', '{editorial.Telefono}', '{editorial.Email}', '{editorial.Direccion.Calle}', '{editorial.Direccion.NumeroInterior}', '{editorial.Direccion.NumeroExterior}', '{editorial.Direccion.Colonia.IdColonia}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al asignar la editorial";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Problemas al hacer la inserción" + result.Ex;
                //throw;
            }
            return result;
        }

        public static ML.Result Update(ML.Editorial editorial)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UpdateEditorial '{editorial.IdEditorial}', '{editorial.Nombre}', '{editorial.Telefono}', '{editorial.Email}', '{editorial.Direccion.Calle}', '{editorial.Direccion.NumeroInterior}', '{editorial.Direccion.NumeroExterior}', '{editorial.Direccion.Colonia.IdColonia}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al actualizar la editorial";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Problemas al hacer la actualización" + result.Ex;
                //throw;
            }
            return result;
        }

        public static ML.Result Delete(int IdEditorial)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DeleteEditorial '{IdEditorial}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al eliminar la editorial";
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
