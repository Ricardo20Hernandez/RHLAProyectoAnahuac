using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DetallePrestamo
    {
        public static ML.Result DevolverMedio(ML.DetallePrestamo detallePrestamo)
        {
            ML.Result result = new ML.Result();
            ML.Result resultMedio = new ML.Result();
            //resultMedio = BL.Medio.GetById(detallePrestamo.Prestamo.Medio.IdMedio);
            //ML.Medio medio = new ML.Medio();
            //medio = (ML.Medio)resultMedio.Object;
            ML.Result restarInventario = new ML.Result();
            restarInventario = BL.Prestamo.SumarInventario(detallePrestamo.Prestamo.Medio.IdMedio);
            ML.Result obtenerPrestamo = new ML.Result();
            obtenerPrestamo = BL.DetallePrestamo.ObtenerPrestamo(detallePrestamo);
            ML.DetallePrestamo detallePrestamo1 = new ML.DetallePrestamo();
            detallePrestamo1 = (ML.DetallePrestamo)obtenerPrestamo.Object;
            detallePrestamo.IdDetallePrestamo = detallePrestamo1.IdDetallePrestamo;
            detallePrestamo.Prestamo.IdPrestamo = detallePrestamo1.Prestamo.IdPrestamo;
                

            //medio.CantidadDisponible = medio.CantidadDisponible + 1;

            try
                {
                    string cadenaConexion = "Server=.; Database=RHLAProyectoAnahuac; TrustServerCertificate=True; Trusted_Connection=True; User ID=sa; Password=pass@word1;";
                    string query = "EntregarPrestamo";
                    using (SqlConnection context = new SqlConnection(cadenaConexion))
                    {   //cadena que es el procedure





                        //cmd.Parameters.AddWithValue("@ID", usuario.ID);
                        // manda el procedure  y la conexion 

                        context.Open();
                        using (SqlCommand cmd = new SqlCommand(query, context))
                        { //@UserName,@ApellidoPaterno,@ApellidoMaterno,@Email,@Password,@Sexo,@Telefono,@Celular,@FechaNacimiento,@CURP,@IdRol,@Nombre
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@IdDetallePrestamo", detallePrestamo.IdDetallePrestamo);
                            cmd.Parameters.AddWithValue("@FechaEntregaReal", detallePrestamo.FechaEntregaReal);
                           






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
                
          

            return result;
        }

        public static ML.Result ObtenerPrestamo(ML.DetallePrestamo detallePrestamo)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {

                    var query = (from prestamo in context.Prestamos
                                 join detallePrestamoLq in context.DetallePrestamos on prestamo.IdPrestamo equals detallePrestamoLq.IdPrestamo
                                 
                                 where prestamo.Id == detallePrestamo.Prestamo.Id && prestamo.IdMedio == detallePrestamo.Prestamo.Medio.IdMedio
                                 select new
                                 {
                                     IdPrestamo = prestamo.IdPrestamo,
                                    IdDetallePrestamo = detallePrestamoLq.IdDetallePrestamo
                                 }).FirstOrDefault();

                    if (query != null)
                    {
                        
                            ML.DetallePrestamo detallePrestamoSalida = new ML.DetallePrestamo();
                            detallePrestamoSalida.Prestamo = new ML.Prestamo();
                        detallePrestamoSalida.Prestamo.IdPrestamo = query.IdPrestamo;
                        detallePrestamoSalida.IdDetallePrestamo = query.IdDetallePrestamo;


                            result.Object = detallePrestamoSalida;

                        
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
