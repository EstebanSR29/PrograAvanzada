using KN_WEB.BaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KN_WEB.Models
{
    public class RolModel
    {
        public List<tRol> ConsultarRoles()
        {
            using (var context = new KN_WEB_BDEntities())
            {
                return (from x in context.tRol
                        select x).ToList();
            }
        }
    }
}