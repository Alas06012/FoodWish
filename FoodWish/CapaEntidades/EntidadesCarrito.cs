using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWish.CapaEntidades
{
    class EntidadesCarrito
    {
        private int id_carrito;
        private string nom_comida;
        private int comida_car;
        private int usuario_car;
        private int cantidad_car;
        private decimal precio_car;
        private int fact_detfact;

        public EntidadesCarrito(int id_carrito, string nom_comida, int comida_car, int usuario_car, int cantidad_car, decimal precio_car, int fact_detfact)
        {
            this.id_carrito = id_carrito;
            this.nom_comida = nom_comida;
            this.comida_car = comida_car;
            this.usuario_car = usuario_car;
            this.cantidad_car = cantidad_car;
            this.precio_car = precio_car;
            this.fact_detfact = fact_detfact;
        }

        public EntidadesCarrito() {
        
        }

        public int Id_Carrito { get => id_carrito; set => id_carrito = value; }
        public string Nom_Comida { get => nom_comida; set => nom_comida = value; }
        public int Comida_Car { get => comida_car; set => comida_car = value; }
        public int Usuario_Car { get => usuario_car; set => usuario_car = value; }
        public int Cantidad_Car { get => cantidad_car; set => cantidad_car = value; }
        public decimal Precio_Car { get => precio_car; set => precio_car = value; }
        public int Fact_DetFact { get => fact_detfact; set => fact_detfact = value; }

    }
}
