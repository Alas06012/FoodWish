using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWish.CapaEntidades
{
   public class EntidadesTipoComida
    {
        private int idTipoComida;
        private string nombreTipo;

        public EntidadesTipoComida(int idTipoComida, string nombreTipo)
        {
            this.idTipoComida = idTipoComida;
            this.nombreTipo = nombreTipo;
        }
        public EntidadesTipoComida()
        {
            
        }


        public int IdTipoComida { get => idTipoComida; set => idTipoComida = value; }
        public string NombreTipo { get => nombreTipo; set => nombreTipo = value; }















    }
}
