using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWish.CapaEntidades
{
    public class EntidadesComida
    {
        private int idComida;
        private string nombre;
        private string descripcion;
        private int tipo;
        private decimal precio;
        private int idReceta;

      
        public EntidadesComida(int idComida, string nombre, string descripcion, int tipo, decimal precio, int idReceta)
        {
            this.idComida = idComida;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.tipo = tipo;
            this.precio = precio;
            this.idReceta = idReceta;
        }

        public EntidadesComida()
        {
    
        }


        public int IdComida { get => idComida; set => idComida = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Tipo { get => tipo; set => tipo = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public int IdReceta { get => idReceta; set => idReceta = value; }













    }
}
