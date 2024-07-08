using System.Web;
using System.Web.Mvc;
using ML;

namespace PL.Models
{
    public class AuthRoles : AuthorizeAttribute
    {
        private readonly string[] _roles;
        public AuthRoles(params string[] roles)
        {
            _roles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var usuario = httpContext.Session["UsuarioDatos"] as ML.Usuario;
            if (usuario == null || string.IsNullOrEmpty(usuario.Nombre))
            {
                return false; // No autenticado
            }

            var result = BL.AuthService.TieneAcceso(usuario, _roles);
            return result.Correct;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Redirigir al login si no está autenticado
            if (filterContext.HttpContext.Session["UsuarioDatos"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new { controller = "Auth", action = "Login" }
                    )
                );
            }
            else
            {
                // Redirigir a Home si no tiene permisos
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new { controller = "Home", action = "Index" }
                    )
                );
            }
        }
    }
}

