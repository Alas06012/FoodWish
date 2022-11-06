using FoodWish.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FoodWish.Vistas
{
    /// <summary>
    /// Lógica de interacción para Ventana_Acceso_Usuario.xaml
    /// </summary>
    public partial class Ventana_Acceso_Usuario : Window
    {
        public Ventana_Acceso_Usuario(int id_usu)
        {
            InitializeComponent();
            txt_sesion.Text = id_usu.ToString();
        }
       
        private void txt_sesion_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_comer_aqui_Click(object sender, RoutedEventArgs e)
        {
            string idsesion = txt_sesion.Text;
            Elec_Mesa elec = new Elec_Mesa(idsesion);
            elec.Show();
            this.Close();
        }

        private void btn_llevar_Click(object sender, RoutedEventArgs e)
        {
            string idsesion = txt_sesion.Text;
            Pedido pedido = new Pedido(idsesion);
            pedido.Show();
            this.Close();
        }
    }
}
