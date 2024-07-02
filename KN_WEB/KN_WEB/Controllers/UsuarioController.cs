using KN_WEB.Entidades;
using KN_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KN_WEB.Controllers
{
    
    [OutputCache(NoStore = true, VaryByParam ="*", Duration = 0)]
    public class UsuarioController : Controller
    {
        UsuarioModel usuarioM = new UsuarioModel();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuario entidad)
        {
            var respuesta = usuarioM.IniciarSesion(entidad);

            if (respuesta != null)
            {
                Session["NombreUsuario"] = respuesta.Nombre;
                return RedirectToAction("Home", "Usuario");
            }
            else
            {
                ViewBag.msj = "Usuario incorrecto o no existe";
                return View();
            }
                
        }

        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(Usuario entidad)
        {
            var respuesta = usuarioM.RegistrarUsuario(entidad);

            if (respuesta == true)
                return RedirectToAction("Index", "Usuario");
            else
            {
                ViewBag.msj = "No se logro registrar el Usuario";
                return View();
            }
        }

        [FiltroSeguridad]
        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }

        [FiltroSeguridad]
        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Index","Usuario");
        }

        [FiltroSeguridad]
        [HttpGet]
        public ActionResult ConsultarUsuarios()
        {
            var respuesta = usuarioM.ConsultarUsuarios();

            return View(respuesta); 
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