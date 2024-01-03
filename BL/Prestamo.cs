using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"NuevoPrestamo '{prestamo.FechaSalida},'{prestamo.FechaEntregaEsperada}','{prestamo.Medio.IdMedio}','{prestamo.Id}'");
                    //DL.Autor autorLQ = new DL.Autor();
                    //autorLQ.Nombre = autor.Nombre;
                    //autorLQ.ApellidoPaterno = autor.ApellidoPaterno;
                    //autorLQ.ApellidoMaterno = autor.ApellidoMaterno;
                    //autorLQ.FechaNacimiento = DateTime.Parse(autor.FechaNacimiento);
                    //autorLQ.Foto = autor.Foto;


                    //var query = context.Database.ExecuteSqlRaw($"DeleteAutor '{IdAutor}'");

                    if (query > 0)
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

            return result;
        }

        public static ML.Result RestarInventario(int IdMedio)
        {ML.Result resultMedio = new ML.Result();
            resultMedio = BL.Medio.GetById(IdMedio);
            ML.Medio medio = new ML.Medio();
            medio = (ML.Medio)resultMedio.Object;
            medio.CantidadDisponible = medio.CantidadDisponible - 1;


            ML.Result result = new ML.Result();
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

            return result;
        }


    }
}
