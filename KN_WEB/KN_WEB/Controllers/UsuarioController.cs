using KN_WEB.Entidades;
using KN_WEB.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KN_WEB.Controllers
{
    [FiltroAdmin]
    [FiltroSeguridad]
    [OutputCache(NoStore = true, VaryByParam ="*", Duration = 0)]
    public class UsuarioController : Controller
    {
        UsuarioModel usuarioM = new UsuarioModel();

        RolModel rolM = new RolModel();

        [HttpGet]
        public ActionResult ConsultarUsuarios()
        {
            var respuesta = usuarioM.ConsultarUsuarios();

            return View(respuesta); 
        }

        [HttpPost]
        public ActionResult CambiarEstadoUsuario(Usuario entidad)
        {
            var respuesta = usuarioM.CambiarEstadoUsuario(entidad);

            if (respuesta)
                return RedirectToAction("ConsultarUsuarios", "Usuario");
            else
            {
                ViewBag.msj = "No se logro inactivar el Usuario";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ActualizarUsuario(int Consecutivo)
        {
            var respuesta = usuarioM.ConsultarUsuario(Consecutivo);

            var roles = rolM.ConsultarRoles();

            List<SelectListItem> lstRoles = new List<SelectListItem>();
            foreach (var item in roles)
            {
                lstRoles.Add(new SelectListItem { Value = item.IdRol.ToString(), Text = item.NombreRol.ToString()});
            }

            ViewBag.roles = lstRoles;

            return View(respuesta);
        }

        [HttpPost]
        public ActionResult ActualizarUsuario(Usuario entidad)
        {
            var respuesta = usuarioM.ActualizarUsuario(entidad);

            if (respuesta)
                return RedirectToAction("ConsultarUsuarios", "Usuario");
            else
            {
                ViewBag.msj = "No se logro actualizar el Usuario";
                return View();
            }
        }















        //[HttpGet]
        //public ActionResult ConsultarIdentificacion(string cedula) 
        //{
        //    using (var request = new HttpClient())
        //    {
        //        var response = request.GetAsync("https://apis.gometa.org/cedulas/" + cedula).Result;

        //        var nombre = response.Content.ReadFromJsonAsync<Usuario>().Result;
        //        return Json(response, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}