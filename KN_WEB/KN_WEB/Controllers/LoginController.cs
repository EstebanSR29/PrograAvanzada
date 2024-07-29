using KN_Web.Models;
using KN_WEB.Entidades;
using KN_WEB.Models;
using System;
using System.Web.Mvc;

namespace KN_WEB.Controllers
{
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
    public class LoginController : Controller
    {
        UsuarioModel usuarioM = new UsuarioModel();

        GeneralModel generalM = new GeneralModel();

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
                if(respuesta.EsClaveTemporal == true && respuesta.ClaveVencimiento <= DateTime.Now)
                {
                    ViewBag.msj = "Solicite una nueva contraseña temporal.";
                    return View();
                }
                Session["NombreUsuario"] = respuesta.Nombre;
                Session["ConsecutivoUsuario"] = respuesta.Consecutivo;
                Session["RolUsuario"] = respuesta.IdRol.ToString();
                return RedirectToAction("Home", "Login");
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
                return RedirectToAction("Index", "Login");
            else
            {
                ViewBag.msj = "Ya existe un usuario con esa cedula o correo registrado";
                return View();
            }
        }

        [FiltroSeguridad]
        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        [FiltroSeguridad]
        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RecuperarAcceso()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarAcceso(Usuario entidad)
        {
            var respuesta = usuarioM.ValidarUsuarioIdentificacion(entidad.Cedula);

            if (respuesta != null)
            {
                var contraTemp = generalM.CreatePassword();
                var fechaVencimientoTemporal = DateTime.Now.AddMinutes(30);
                var actualizacion = usuarioM.CambiarContrasennaUsuario(respuesta.Consecutivo, contraTemp, true, fechaVencimientoTemporal);

                if (actualizacion)
                {
                    string ruta = AppDomain.CurrentDomain.BaseDirectory + "Password.html";
                    string contenido = System.IO.File.ReadAllText(ruta);

                    contenido = contenido.Replace("@@Nombre", respuesta.Nombre);
                    contenido = contenido.Replace("@@Contrasenna", contraTemp);
                    contenido = contenido.Replace("@@Vencimiento", fechaVencimientoTemporal.ToString("dd/MM/yyyy hh:mm:ss tt"));

                    generalM.EnviarCorreo(respuesta.Correo, "Recuperar Acceso KN_WEB", contenido);

                }
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.msj = "No fue posible obtener su informacion";
                return View();
            }
        }


    }


}