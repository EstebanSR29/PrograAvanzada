using KN_WEB.BaseDatos;
using KN_WEB.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace KN_WEB.Models
{
    public class UsuarioModel
    {
        public bool RegistrarUsuario(Usuario entidad)
        {
            var rowsAffected = 0;

            using (var context = new KN_WEB_BDEntities())
            {
               rowsAffected = context.RegistrarUsuario(entidad.Cedula, entidad.Nombre, entidad.Correo, entidad.Password);
            }

            return (rowsAffected > 0 ? true : false);
        }

        public IniciarSesion_Result IniciarSesion(Usuario entidad)
        {

            using (var context = new KN_WEB_BDEntities())
            {
                return context.IniciarSesion(entidad.Correo, entidad.Password).FirstOrDefault();
            }
        }

        public List<tUsuarios> ConsultarUsuarios()
        {
            using (var context = new KN_WEB_BDEntities())
            {
                return (from x in context.tUsuarios select x).ToList();
            }
        }

    }
}
















//INSERTAR USUARIOS SIN PROCESO ALMACENADO POR MEDIO DE LINQ

//public bool RegistrarUsuario(Usuario entidad)
//{
//    var tabla = new tUsuarios();
//    tabla.Cedula = entidad.Cedula;
//    tabla.Nombre = entidad.Nombre;
//    tabla.Correo = entidad.Correo;
//    tabla.Password = entidad.Password;
//    tabla.Estado = true;
//    tabla.IdRol = 1;

//    using (var context = new KN_WEB_BDEntities())
//    {
//        context.tUsuarios.Add(tabla);
//        context.SaveChanges();
//    }
//    return true;
//}

//public bool IniciarSesion(Usuario entidad)
//{
//    using (var context = new KN_WEB_BDEntities())
//    {
//        var resultado = (from x in context.tUsuarios
//                         where x.Correo == entidad.Correo && x.Password == entidad.Password && x.Estado == true
//                         select x).ToList();
//    }
//    return true;
//}