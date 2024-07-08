using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth

        public ActionResult Login()
        {
            ML.Usuario usuario = new ML.Usuario();
            return View(usuario);
        }

        [HttpPost]
        public async Task<ActionResult> Login(string nombre, string password)
        {
            ML.Result result = await BL.AuthService.AutentificarAsync(nombre, password);

            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                Session["UsuarioDatos"] = usuario;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "*Usuario o contraseña incorrecta";
                return View();
            }
        }
    }
}