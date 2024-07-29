using KN_WEB.Entidades;
using KN_WEB.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KN_WEB.Controllers
{
    [FiltroSeguridad]
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
    public class PerfilController : Controller
    {
        UsuarioModel usuarioM = new UsuarioModel();
        [HttpGet]
        public ActionResult PerfilUsuario()
        {
            var respuesta = usuarioM.ConsultarUsuario(int.Parse(Session["ConsecutivoUsuario"].ToString()));
            return View(respuesta);
        }

        [HttpPost]
        public ActionResult ActualizarPerfil(Usuario entidad)
        {
            var respuesta = usuarioM.ActualizarUsuario(entidad);

            if (respuesta)
                return RedirectToAction("PerfilUsuario", "Perfil");
            else
            {
                ViewBag.msj = "No se logro actualizar el Usuario";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ActualizarContrasenna()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ActualizarContrasenna(Usuario entidad)
        {
            if (entidad.Password != entidad.ConfirmarContrasenna)
            {
                ViewBag.msj = "Las contraseñas no coinciden";
                return View();
            }
            int Consecutivo = int.Parse(Session["ConsecutivoUsuario"].ToString());

            var respuesta = usuarioM.CambiarContrasennaUsuario(Consecutivo, entidad.Password, false, DateTime.Now);

            if (respuesta)
                return RedirectToAction("CerrarSesion", "Login");
            else
            {
                ViewBag.msj = "No se logro actualizar el Usuario";
                return View();
            }
        }
    }
}