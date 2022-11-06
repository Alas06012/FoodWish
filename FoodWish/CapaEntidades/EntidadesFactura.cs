using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWish.CapaEntidades
{
    class EntidadesFactura
    {
        private int id_factura;
        private int metod_fact;
        private int usuario_fact;
        private decimal total_fact;

        EntidadesFactura(int id_factura, int metod_fact, int usuario_fact, decimal total_fact)
        {
            this.id_factura = id_factura;
            this.metod_fact = metod_fact;
            this.usuario_fact = usuario_fact;
            this.total_fact = total_fact;
        }

        public EntidadesFactura() {
        
        }

        public int IdFactura { get => id_factura; set => id_factura = value; }
        public int MetodoFact { get => metod_fact; set => metod_fact = value; }
        public int UsuarioFact { get => usuario_fact; set => usuario_fact = value; }
        public decimal TotalFact { get => total_fact; set => total_fact = value; }


    }
}
