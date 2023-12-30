using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BL
{
    public class Autor
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = (from autor in context.Autors
                                 select new
                                 {
                                     IdAutor = autor.IdAutor,
                                     Nombre = autor.Nombre,
                                     ApellidoPaterno = autor.ApellidoPaterno,
                                     ApellidoMaterno = autor.ApellidoMaterno,
                                     FechaNacimiento =  autor.FechaNacimiento,
                                     Foto = autor.Foto
                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var user in query)
                        {
                            ML.Autor usuario = new ML.Autor();
                            usuario.IdAutor = user.IdAutor;
                            usuario.Nombre = user.Nombre;
                            usuario.ApellidoPaterno = user.ApellidoPaterno;
                            usuario.ApellidoMaterno = user.ApellidoMaterno;
                            usuario.FechaNacimiento = user.FechaNacimiento.ToString();
                            usuario.Foto = user.Foto;

                            result.Objects.Add(usuario);

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

        public static ML.Result GetById(int IdAutor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = (from autor in context.Autors

                                 where autor.IdAutor == IdAutor

                                 select new
                                 {
                                     IdAutor = autor.IdAutor,
                                     Nombre = autor.Nombre,
                                     ApellidoPaterno = autor.ApellidoPaterno,
                                     ApellidoMaterno = autor.ApellidoMaterno,
                                     FechaNacimiento = autor.FechaNacimiento,
                                     Foto = autor.Foto
                                 }).FirstOrDefault();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        ML.Autor usuario = new ML.Autor();
                        usuario.IdAutor = query.IdAutor;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.FechaNacimiento = query.FechaNacimiento.ToString();
                        usuario.Foto = query.Foto;

                        result.Object = usuario;

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
        public static ML.Result Add(ML.Autor autor)
        {


            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    //var query = context.Database.ExecuteSqlRawAsync($"AddAutor '{autor.Nombre}','{autor.ApellidoPaterno}','{autor.ApellidoMaterno}','{autor.FechaNacimiento}','{autor.Foto}'");
                    DL.Autor autorLQ = new DL.Autor();
                    autorLQ.Nombre = autor.Nombre;
                    autorLQ.ApellidoPaterno = autor.ApellidoPaterno;
                    autorLQ.ApellidoMaterno = autor.ApellidoMaterno;
                    autorLQ.FechaNacimiento = DateTime.Parse(autor.FechaNacimiento);
                    autorLQ.Foto = autor.Foto;





                    context.Autors.Add(autorLQ);
                    int RowsAffected = context.SaveChanges();

                    //int query = context.UsuarioAdd(usuario.UserName, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Password, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.FechaNacimiento, usuario.CURP, usuario.Rol.IdRol, usuario.Nombre);

                    //cmd.Parameters.AddWithValue("@ID", usuario.ID);
                    // manda el procedure  y la conexion 


                    if (RowsAffected > 0)
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

        public static ML.Result Update(ML.Autor autor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    //var query = context.Database.ExecuteSqlRaw($"UpdateAutor '{autor.IdAutor}','{autor.Nombre}','{autor.ApellidoPaterno}','{autor.ApellidoMaterno}','{autor.FechaNacimiento}','{autor.Foto}'");
                    var usuarioLINQ = (from queryLINQ in context.Autors
                                       where queryLINQ.IdAutor == autor.IdAutor
                                       select queryLINQ).SingleOrDefault();

                    if (usuarioLINQ != null)
                    {
                        //DL_EF.Usuario  = new DL_EF.Usuario();
                        //usuarioLINQ.IdUsuario = usuario.IdUsuario;
                        usuarioLINQ.IdAutor = autor.IdAutor;
                        usuarioLINQ.ApellidoMaterno = autor.ApellidoMaterno;
                        usuarioLINQ.ApellidoPaterno = autor.ApellidoPaterno;
                        usuarioLINQ.Foto = autor.Foto;

                        usuarioLINQ.FechaNacimiento = DateTime.Parse(autor.FechaNacimiento);


                        usuarioLINQ.Nombre = autor.Nombre;

                        //context.Usuario.Update(usuarioLINQ);
                        int RowsAffected = context.SaveChanges();

                        //int query = context.UsuarioAdd(usuario.UserName, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Password, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.FechaNacimiento, usuario.CURP, usuario.Rol.IdRol, usuario.Nombre);

                        //cmd.Parameters.AddWithValue("@ID", usuario.ID);
                        // manda el procedure  y la conexion 
                        if (RowsAffected > 0)
                        //if (query > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }

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

        public static ML.Result Delete(int IdAutor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DeleteAutor '{IdAutor}'");

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

        //add
        //public static ML.Result AddProc(ML.Autor autor)
        //{
        //    ML.Result result = new ML.Result();
        //    //aqui se crea el objeto que se retorna

        //    try
        //    {   //manda la cadena de conexion
        //        //
        //        string query = "UsuarioAdd";
        //        using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
        //        {   //cadena que es el procedure



        //            //cmd.Parameters.AddWithValue("@ID", usuario.ID);
        //            // manda el procedure  y la conexion 

        //            context.Open();
        //            using (SqlCommand cmd = new SqlCommand(query, context))
        //            { //@UserName,@ApellidoPaterno,@ApellidoMaterno,@Email,@Password,@Sexo,@Telefono,@Celular,@FechaNacimiento,@CURP,@IdRol,@Nombre
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
        //                cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
        //                cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
        //                cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
        //                cmd.Parameters.AddWithValue("@Email", usuario.Email);
        //                cmd.Parameters.AddWithValue("@Password", usuario.Password);
        //                cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
        //                cmd.Parameters.AddWithValue("@Celular", usuario.@Celular);
        //                cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
        //                cmd.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);





        //                //  cmd.Parameters.AddWithValue("@Edad", usuario.Edad);


        //                // DataTable usuarioTable = new DataTable();// creamos una tabla vacia

        //                //  SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd); //enviamos el cmd 

        //                // sqlDataAdapter.Fill(usuarioTable); //llenar la tabla que estaba vacia

        //                int RowsAffected = cmd.ExecuteNonQuery();

        //                if (RowsAffected > 0)
        //                {
        //                    result.Correct = true;
        //                    result.ErrorMessage = "Materia insertada correctamente";
        //                }

        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.Ex = ex;
        //        result.ErrorMessage = "Ocurrio un error al realizar la consulta en la base de datos " + result.Ex;
        //        //throw;
        //    }

        //    return result;
        //}
        ///*


    }
}