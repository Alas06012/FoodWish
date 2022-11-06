using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWish.CapaEntidades
{
    public class EntidadesEstadoMesa
    {

        private int idEstadoMesa;
        private string nombreEstado;

     
        public EntidadesEstadoMesa(int idEstadoMesa, string nombreEstado)
        {
            this.idEstadoMesa = idEstadoMesa;
            this.nombreEstado = nombreEstado;
        }
        public EntidadesEstadoMesa()
        {
          
        }


        public int IdEstadoMesa { get => idEstadoMesa; set => idEstadoMesa = value; }
        public string NombreEstado { get => nombreEstado; set => nombreEstado = value; }


    }
}
