using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWish.CapaEntidades
{
    public class EntidadesMetodoPago
    {
        private int idMetodopago;
        private string nombreMetodo;

        

        public EntidadesMetodoPago(int idMetodopago, string nombreMetodo)
        {
            this.idMetodopago = idMetodopago;
            this.nombreMetodo = nombreMetodo;
        }

        public EntidadesMetodoPago()
        {
            
        }

        public int IdMetodopago { get => idMetodopago; set => idMetodopago = value; }
        public string NombreMetodo { get => nombreMetodo; set => nombreMetodo = value; }

    }
}
