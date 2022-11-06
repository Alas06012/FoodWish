using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWish.CapaEntidades
{
    public class EntidadesMesa
    {
        private int idMesa;
        private int numero_mesa;
        private dynamic idEstado_mesa;
        private dynamic idUsuario;

        
        public EntidadesMesa(int idMesa, int numero_mesa, dynamic idEstado_mesa, dynamic idUsuario)
        {
            this.idMesa = idMesa;
            this.numero_mesa = numero_mesa;
            this.idEstado_mesa = idEstado_mesa;
            this.idUsuario = idUsuario;
        }

        public EntidadesMesa()
        {
       
        }

        public int IdMesa { get => idMesa; set => idMesa = value; }
        public int Numero_mesa { get => numero_mesa; set => numero_mesa = value; }
        public dynamic IdEstado_mesa { get => idEstado_mesa; set => idEstado_mesa = value; }
        public dynamic IdUsuario { get => idUsuario; set => idUsuario = value; }









    }
}
