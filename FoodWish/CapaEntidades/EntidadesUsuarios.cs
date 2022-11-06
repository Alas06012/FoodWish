using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWish.CapaEntidades
{
    public class EntidadesUsuarios
    {
        private int idUsuario;
        private string nombreUsuario;
        private int tipoUsuario;
        private int idPregunta;
        private string respuesta;
       // private string contrasena;

        public EntidadesUsuarios(int idUsuario, string nombreUsuario, int tipoUsuario, int idPregunta, string respuesta)// string contrasena)
        {
            this.idUsuario = idUsuario;
            this.nombreUsuario = nombreUsuario;
            this.tipoUsuario = tipoUsuario;
            this.idPregunta = idPregunta;
            this.respuesta = respuesta;
          //  this.contrasena = contrasena;
        }
        public EntidadesUsuarios()
        {
 
        }

        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public int TipoUsuario { get => tipoUsuario; set => tipoUsuario = value; }
        public int IdPregunta { get => idPregunta; set => idPregunta = value; }
        public string Respuesta { get => respuesta; set => respuesta = value; }
       // public string Contrasena { get => contrasena; set => contrasena = value; }
    }
}
