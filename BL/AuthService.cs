using DL;
using ML;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BL
{
    public class AuthService
    {
        public static async Task<ML.Result> AutentificarAsync(string nombre, string password)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DbPruebaEntities context = new DbPruebaEntities())
                {
                    var user = await context.Users
                        .Where(u => u.Nombre == nombre && u.Password == password)
                        .Select(u => new { u.Nombre, u.Password })
                        .FirstOrDefaultAsync();

                    if (user != null)
                    {
                        var usuario = new ML.Usuario
                        {
                            Nombre = user.Nombre,
                            Password = user.Password,
                            Roles = await ObtenerRolesAsync(user.Nombre)
                        };

                        if (usuario.Roles == null)
                        {
                            result.ErrorMessage = "No se encontró ningún rol para este usuario";
                        }
                        else
                        {
                            result.Object = usuario;
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception exc)
            {
                result.Correct = false;
                result.ErrorMessage = exc.Message;
                result.Excepcion = exc;
            }
            return result;
        }

        private static async Task<List<string>> ObtenerRolesAsync(string nombre)
        {
            try
            {
                using (DbPruebaEntities context = new DbPruebaEntities())
                {
                    var roles = await (from u in context.Users
                                       join ru in context.RolesUsers on u.Id equals ru.IdUser
                                       join r in context.Roles on ru.IdRol equals r.Id
                                       where u.Nombre == nombre
                                       select r.Rol).ToListAsync();

                    return roles.Count > 0 ? roles : null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static ML.Result TieneAcceso(ML.Usuario usuario, string[] rolesRequeridos)
        {
            ML.Result result = new ML.Result();
            result.Strings = usuario.Roles;
            try
            {
                var rolesCoincidentes = usuario.Roles.Intersect(rolesRequeridos);
                result.Correct = rolesCoincidentes.Any();
                if (!result.Correct)
                {
                    result.ErrorMessage = "El usuario no tiene permiso de estar aquí";
                }
            }
            catch (Exception exc)
            {
                result.Correct = false;
                result.ErrorMessage = exc.Message;
                result.Excepcion = exc;
            }
            return result;
        }
    }
}
