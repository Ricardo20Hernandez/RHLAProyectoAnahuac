using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class IdentityUser
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = (from user in context.AspNetUsers
                                 select new
                                 {
                                     IdUser = user.Id,
                                     UserName = user.UserName
                                 }).ToList();

                    if (query.Count() > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var user in query)
                        {
                            ML.UserIdentity usuario = new ML.UserIdentity();

                            usuario.IdUsuario = user.IdUser;
                            usuario.UserName = user.UserName;

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Hubo error al realizar la consulta en la BD" + result.Ex;
                //throw;
            }
            return result;
        }

        public static ML.Result Asignar(ML.UserIdentity user)
        {
            ML.Result result = new ML.Result();

            try
            {

                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AddAspNetUserRoles '{user.IdUsuario}', '{user.Rol.RoleId}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al asignar el Rol al Usuario";
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

        public static ML.Result GetById(string id)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RhlaproyectoAnahuacContext context = new DL.RhlaproyectoAnahuacContext())
                {
                    var query = (from rol in context.AspNetRoles
                                 where rol.Id == id
                                 select new
                                 {
                                     IdRol = rol.Id,
                                     Name = rol.Name
                                 });

                    if (query != null)
                    {

                        ML.Rol rol = new ML.Rol();
                        var roles = query.SingleOrDefault();
                        rol.RoleId = new Guid(roles.IdRol);
                        rol.Name = roles.Name;

                        result.Object = rol;

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Hubo error al realizar la consulta en la BD" + result.Ex;
                //throw;
            }
            return result;
        }
    }
}
