using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class UsuarioRol
    {
        public int Id { get; set; }
        public ML.Rol Rol { get; set; }
        public ML.Usuario Usuario { get; set; }
        public List<object> RolesUsers { get; set; }
    }
}
