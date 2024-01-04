using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Prestamo
    {

        public static ML.Result NuevoPrestamo(ML.Prestamo prestamo)
        {


            ML.Result result = new ML.Result();
            ML.Result librosprestados = new ML.Result();
            librosprestados = BL.Prestamo.LibrosPrestados(prestamo.Id);
            ML.DetallePrestamo detallePrestamo = new ML.DetallePrestamo();
            detallePrestamo.DetallePrestamos = librosprestados.Objects;
            bool EstelibroloPuedeSacarElUsuario = true;
            if (detallePrestamo.DetallePrestamos != null)
            {
                foreach (ML.DetallePrestamo detallePrestamoTemp in detallePrestamo.DetallePrestamos)
                {
                    if (detallePrestamoTemp.Prestamo.Medio.IdMedio == prestamo.Medio.IdMedio)
                    {
                        EstelibroloPuedeSacarElUsuario = false;
                    }
                }

            }
            if(EstelibroloPuedeSacarElUsuario)
            {
    try
                {
                    string cadenaConexion = "Server=.; Database=RHLAProyectoAnahuac; TrustServerCertificate=True; Trusted_Connection=True; User ID=sa; Password=pass@word1;";
                    string query = "NuevoPrestamo";
                    using (SqlConnection context = new SqlConnection(cadenaConexion))
                    {   //cadena que es el procedure





                        //cmd.Parameters.AddWithValue("@ID", usuario.ID);
                        // manda el procedure  y la conexion 

                        context.Open();
                        using (SqlCommand cmd = new SqlCommand(query, context))
                        { //@UserName,@ApellidoPaterno,@ApellidoMaterno,@Email,@Password,@Sexo,@Telefono,@Celular,@FechaNacimiento,@CURP,@IdRol,@Nombre
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@FechaSalida", prestamo.FechaSalida);
                            cmd.Parameters.AddWithValue("@FechaEntregaEsperada", prestamo.FechaEntregaEsperada);
                            cmd.Parameters.AddWithValue("@IdMedio", prestamo.Medio.IdMedio);
                            cmd.Parameters.AddWithValue("@Id", prestamo.Id);
                       





                            int RowsAffected = cmd.ExecuteNonQuery();

                            if (RowsAffected > 0)
                            {
                                result.Correct = true;
                                result.ErrorMessage = "Materia insertada correctamente";
                            }

                        }
                    }

                    //using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                    //{
                    //    var query = context.Database.ExecuteSqlRaw($"NuevoPrestamo '{prestamo.FechaSalida},'{prestamo.FechaEntregaEsperada}','{prestamo.Medio.IdMedio}','{prestamo.Id}'");
                    //    //DL.Autor autorLQ = new DL.Autor();
                    //    //autorLQ.Nombre = autor.Nombre;
                    //    //autorLQ.ApellidoPaterno = autor.ApellidoPaterno;
                    //    //autorLQ.ApellidoMaterno = autor.ApellidoMaterno;
                    //    //autorLQ.FechaNacimiento = DateTime.Parse(autor.FechaNacimiento);
                    //    //autorLQ.Foto = autor.Foto;


                    //    //var query = context.Database.ExecuteSqlRaw($"DeleteAutor '{IdAutor}'");

                    //    if (query > 0)
                    //    {
                    //        result.Correct = true;
                    //    }
                    //    else
                    //    {
                    //        result.Correct = false;
                    //    }
                    

                    //}
                }
                catch (Exception e)
                {
                    result.Correct = false;
                    result.ErrorMessage = e.InnerException.Message;
                    result.Ex = e;
                }
ML.Result restarInventario = new ML.Result();
                restarInventario = BL.Prestamo.RestarInventario(prestamo.Medio.IdMedio);
            }
            else
            {
                result.Correct = false;
                   result.ErrorMessage = "Este libro no puede sacarlo ya que cuenta con una copia";

            }

            return result;
        }

        public static ML.Result RestarInventario(int IdMedio)
        {ML.Result resultMedio = new ML.Result();
            resultMedio = BL.Medio.GetById(IdMedio);
            ML.Medio medio = new ML.Medio();
            medio = (ML.Medio)resultMedio.Object;
            
      //     if(medio.CantidadDisponible > 0) { }

            medio.CantidadDisponible = medio.CantidadDisponible - 1;


            ML.Result result = new ML.Result();

            //if(EstelibroloPuedeSacarElUsuario)
            //{
try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"RestarInventario '{medio.CantidadDisponible}','{IdMedio}'");
                    //DL.Autor autorLQ = new DL.Autor();
                    //autorLQ.Nombre = autor.Nombre;
                    //autorLQ.ApellidoPaterno = autor.ApellidoPaterno;
                    //autorLQ.ApellidoMaterno = autor.ApellidoMaterno;
                    //autorLQ.FechaNacimiento = DateTime.Parse(autor.FechaNacimiento);
                    //autorLQ.Foto = autor.Foto;





                    //context.Autors.Add(autorLQ);
                    //int RowsAffected = context.SaveChanges();

                    //int query = context.UsuarioAdd(usuario.UserName, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Password, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.FechaNacimiento, usuario.CURP, usuario.Rol.IdRol, usuario.Nombre);

                    //cmd.Parameters.AddWithValue("@ID", usuario.ID);
                    // manda el procedure  y la conexion 


                    if (query != null)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.InnerException.Message;
                result.Ex = e;
            }
            //}
            //else
            //{ result.Correct = false;
            //    result.ErrorMessage = "Este libro no puede sacarlo ya que cuenta con una copia";
            //}

            

            return result;
        }

        public static ML.Result SumarInventario(int IdMedio)
        {
            ML.Result resultMedio = new ML.Result();
            resultMedio = BL.Medio.GetById(IdMedio);
            ML.Medio medio = new ML.Medio();
            medio = (ML.Medio)resultMedio.Object;



            medio.CantidadDisponible = medio.CantidadDisponible + 1;


            ML.Result result = new ML.Result();
            //if(EstelibroloPuedeSacarElUsuario)
            //{
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"RestarInventario '{medio.CantidadDisponible}','{IdMedio}'");
                    //DL.Autor autorLQ = new DL.Autor();
                    //autorLQ.Nombre = autor.Nombre;
                    //autorLQ.ApellidoPaterno = autor.ApellidoPaterno;
                    //autorLQ.ApellidoMaterno = autor.ApellidoMaterno;
                    //autorLQ.FechaNacimiento = DateTime.Parse(autor.FechaNacimiento);
                    //autorLQ.Foto = autor.Foto;





                    //context.Autors.Add(autorLQ);
                    //int RowsAffected = context.SaveChanges();

                    //int query = context.UsuarioAdd(usuario.UserName, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Password, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.FechaNacimiento, usuario.CURP, usuario.Rol.IdRol, usuario.Nombre);

                    //cmd.Parameters.AddWithValue("@ID", usuario.ID);
                    // manda el procedure  y la conexion 


                    if (query != null)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.InnerException.Message;
                result.Ex = e;
            }
            //}
            //else
            //{ result.Correct = false;
            //    result.ErrorMessage = "Este libro no puede sacarlo ya que cuenta con una copia";
            //}



            return result;
        }


        //historial de prestamo por alumno
        public static ML.Result LibrosPrestados(string ID)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    //                var innerJoinQuery =
                    //from category in categories
                    //join prod in products on category.ID equals prod.CategoryID
                    //select new { ProductName = prod.Name, Category = category.Name };
                    //var query = (from producto in context.Productos
                    //             join Proveedor in context.Proveedors on producto.IdProveedor equals Proveedor.IdProveedor
                    //             join departamento in context.Departamentos on producto.IdDepartamento equals departamento.IdDepartamento
                    //             select new
                    //             {
                    //                 IdProducto = producto.IdProducto,
                    //                 Nombre = producto.Nombre,
                    //                 PrecioUnitario = producto.PrecioUnitario,
                    //                 Stock = producto.Stock,
                    //                 IdProovedor = producto.IdProveedor,
                    //                 IdDepartamento = producto.IdDepartamento,
                    //                 Descripcion = producto.Descripcion,
                    //                 Foto = producto.Foto,
                    //                 Telefono = Proveedor.Telefono,
                    //                 NombreDepartamento = departamento.Nombre
                    //             }).ToList();

                    var query = (from prestamo in context.Prestamos
                                 join detallePrestamo in context.DetallePrestamos on prestamo.IdPrestamo equals detallePrestamo.IdPrestamo
                                 join medio in context.Medios on prestamo.IdMedio equals medio.IdMedio
                                 join tipoMedio in context.TipoMedios on medio.IdTipoMedio equals tipoMedio.IdTipoMedio
                                 join autor in context.Autors on medio.IdAutor equals autor.IdAutor
                                 where prestamo.Id == ID && detallePrestamo.Entregado == false
                                 select new
                                 {
                                     IdPrestamo = prestamo.IdPrestamo,
                                     FechaSalida = prestamo.FechaSalida,
                                     FechaEntregaEsperada = prestamo.FechaEntregaEsperada,
                                     IdMedio = prestamo.IdMedio,
                                     Titulo = medio.Titulo,
                                     Imagen = medio.Imagen,
                                     IdAutor = medio.IdAutor,
                                     IdTipoMedio = tipoMedio.IdTipoMedio,
                                     NombreTipoMedio = tipoMedio.Nombre,
                                     Nombre = autor.Nombre,
                                     ApellidoPaterno = autor.ApellidoPaterno,
                                     ApellidoMaterno = autor.ApellidoMaterno,
                                     Id = prestamo.Id,
                                     FechaEntregaReal = detallePrestamo.FechaEntregaReal,
                                     Entregado = detallePrestamo.Entregado
                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var prestamoLQ in query)
                        {
                            ML.DetallePrestamo detallePrestamo = new ML.DetallePrestamo();
                            detallePrestamo.Prestamo = new ML.Prestamo();
                            detallePrestamo.Prestamo.Medio = new ML.Medio();
                            detallePrestamo.Prestamo.Medio.TipoMedio = new ML.TipoMedio();
                            detallePrestamo.Prestamo.Medio.Autor = new ML.Autor();
                            detallePrestamo.Prestamo.IdPrestamo = prestamoLQ.IdPrestamo;
                            detallePrestamo.Prestamo.FechaSalida = prestamoLQ.FechaSalida.ToString();
                            detallePrestamo.Prestamo.FechaEntregaEsperada = prestamoLQ.FechaEntregaEsperada.ToString();
                            detallePrestamo.Prestamo.Medio.IdMedio = prestamoLQ.IdMedio.Value;
                            detallePrestamo.Prestamo.Medio.Titulo = prestamoLQ.Titulo;
                            detallePrestamo.Prestamo.Medio.Imagen = prestamoLQ.Imagen;
                            detallePrestamo.Prestamo.Medio.TipoMedio.IdTipoMedio = prestamoLQ.IdTipoMedio;
                            detallePrestamo.Prestamo.Medio.TipoMedio.Nombre = prestamoLQ.NombreTipoMedio;
                            detallePrestamo.Prestamo.Medio.Autor.IdAutor = prestamoLQ.IdAutor.Value;
                            detallePrestamo.Prestamo.Medio.Autor.Nombre = prestamoLQ.Nombre;
                            detallePrestamo.Prestamo.Medio.Autor.ApellidoPaterno = prestamoLQ.ApellidoPaterno;
                            detallePrestamo.Prestamo.Medio.Autor.ApellidoMaterno = prestamoLQ.ApellidoMaterno;
                            detallePrestamo.Prestamo.Id = prestamoLQ.Id;
                            detallePrestamo.FechaEntregaReal = prestamoLQ.FechaEntregaReal.ToString();
                            detallePrestamo.Entregado = prestamoLQ.Entregado;
                         

                            result.Objects.Add(detallePrestamo);

                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.InnerException.Message;
                result.Ex = e;
            }

            return result;
        }
        public static ML.Result LibrosDisponibles(string ID)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    //                var innerJoinQuery =
                    //from category in categories
                    //join prod in products on category.ID equals prod.CategoryID
                    //select new { ProductName = prod.Name, Category = category.Name };
                    //var query = (from producto in context.Productos
                    //             join Proveedor in context.Proveedors on producto.IdProveedor equals Proveedor.IdProveedor
                    //             join departamento in context.Departamentos on producto.IdDepartamento equals departamento.IdDepartamento
                    //             select new
                    //             {
                    //                 IdProducto = producto.IdProducto,
                    //                 Nombre = producto.Nombre,
                    //                 PrecioUnitario = producto.PrecioUnitario,
                    //                 Stock = producto.Stock,
                    //                 IdProovedor = producto.IdProveedor,
                    //                 IdDepartamento = producto.IdDepartamento,
                    //                 Descripcion = producto.Descripcion,
                    //                 Foto = producto.Foto,
                    //                 Telefono = Proveedor.Telefono,
                    //                 NombreDepartamento = departamento.Nombre
                    //             }).ToList();

                    var query = (from prestamo in context.Prestamos
                                 join detallePrestamo in context.DetallePrestamos on prestamo.IdPrestamo equals detallePrestamo.IdPrestamo
                                 join medio in context.Medios on prestamo.IdMedio equals medio.IdMedio
                                 join tipoMedio in context.TipoMedios on medio.IdTipoMedio equals tipoMedio.IdTipoMedio
                                 join autor in context.Autors on medio.IdAutor equals autor.IdAutor
                                 join editorial in context.Editorials on medio.IdEditorial equals editorial.IdEditorial
                                 join idioma in context.Idiomas on medio.IdIdioma equals idioma.IdIdioma
                                 where prestamo.Id == ID 
                                 select new
                                 {
                                     IdPrestamo = prestamo.IdPrestamo,
                                     FechaSalida = prestamo.FechaSalida,
                                     FechaEntregaEsperada = prestamo.FechaEntregaEsperada,
                                     IdMedio = prestamo.IdMedio,
                                     Titulo = medio.Titulo,
                                     Imagen = medio.Imagen,
                                     IdAutor = medio.IdAutor,
                                     IdTipoMedio = tipoMedio.IdTipoMedio,
                                     NombreTipoMedio = tipoMedio.Nombre,
                                     Nombre = autor.Nombre,
                                     ApellidoPaterno = autor.ApellidoPaterno,
                                     ApellidoMaterno = autor.ApellidoMaterno,
                                     Id = prestamo.Id,
                                     FechaEntregaReal = detallePrestamo.FechaEntregaReal,
                                     Entregado = detallePrestamo.Entregado,
                                     IdEditorial = editorial.IdEditorial,
                                     NombreEditorial = editorial.Nombre,
                                     IdIdioma = idioma.IdIdioma,
                                     NombreIdioma = idioma.Nombre
                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var prestamoLQ in query)
                        {
                            ML.DetallePrestamo detallePrestamo = new ML.DetallePrestamo();
                            detallePrestamo.Prestamo = new ML.Prestamo();
                            detallePrestamo.Prestamo.Medio = new ML.Medio();
                            detallePrestamo.Prestamo.Medio.TipoMedio = new ML.TipoMedio();
                            detallePrestamo.Prestamo.Medio.Autor = new ML.Autor();
                            detallePrestamo.Prestamo.Medio.Editorial = new ML.Editorial();
                            detallePrestamo.Prestamo.Medio.Idioma = new ML.Idioma();
                            detallePrestamo.Prestamo.IdPrestamo = prestamoLQ.IdPrestamo;
                            detallePrestamo.Prestamo.FechaSalida = prestamoLQ.FechaSalida.ToString();
                            detallePrestamo.Prestamo.FechaEntregaEsperada = prestamoLQ.FechaEntregaEsperada.ToString();
                            detallePrestamo.Prestamo.Medio.IdMedio = prestamoLQ.IdMedio.Value;
                            detallePrestamo.Prestamo.Medio.Titulo = prestamoLQ.Titulo;
                            detallePrestamo.Prestamo.Medio.Imagen = prestamoLQ.Imagen;
                            detallePrestamo.Prestamo.Medio.TipoMedio.IdTipoMedio = prestamoLQ.IdTipoMedio;
                            detallePrestamo.Prestamo.Medio.TipoMedio.Nombre = prestamoLQ.NombreTipoMedio;
                            detallePrestamo.Prestamo.Medio.Autor.IdAutor = prestamoLQ.IdAutor.Value;
                            detallePrestamo.Prestamo.Medio.Autor.Nombre = prestamoLQ.Nombre;
                            detallePrestamo.Prestamo.Medio.Autor.ApellidoPaterno = prestamoLQ.ApellidoPaterno;
                            detallePrestamo.Prestamo.Medio.Autor.ApellidoMaterno = prestamoLQ.ApellidoMaterno;
                            detallePrestamo.Prestamo.Id = prestamoLQ.Id;
                            detallePrestamo.FechaEntregaReal = prestamoLQ.FechaEntregaReal.ToString();
                            detallePrestamo.Entregado = prestamoLQ.Entregado;
                            detallePrestamo.Prestamo.Medio.Editorial.IdEditorial = prestamoLQ.IdEditorial;
                            detallePrestamo.Prestamo.Medio.Editorial.Nombre = prestamoLQ.NombreEditorial;
                            detallePrestamo.Prestamo.Medio.Idioma.IdIdioma = prestamoLQ.IdIdioma;
                            detallePrestamo.Prestamo.Medio.Idioma.Nombre = prestamoLQ.NombreIdioma;




                            result.Objects.Add(detallePrestamo);

                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.InnerException.Message;
                result.Ex = e;
            }

            return result;
        }



    }
}
