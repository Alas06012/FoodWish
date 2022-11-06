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
using FoodWish.CapaDatos;
using FoodWish.CapaEntidades;

namespace FoodWish.Vistas
{
    /// <summary>
    /// Lógica de interacción para Elec_Mesa.xaml
    /// </summary>
    public partial class Elec_Mesa : Window
    {
        public Elec_Mesa(string idsesion)
        {
            InitializeComponent();
            MostrarTablaMesa();
            txtid_sesion.Text = idsesion.ToString();
        }

        void MostrarTablaMesa() {
            dgMesa.ItemsSource = DatosMesa.MostrarElecMesas();
        }

        private void dgMesa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EntidadesMesa user = (EntidadesMesa)dgMesa.SelectedItem;
            //Preguntar si el datagrid tiene filas
            if (user == null)
            {
                return;
            }

            txtNum_mesa.Text = user.Numero_mesa.ToString();
            txtestado_mesa.Text = user.IdEstado_mesa;
            txtid_mesa.Text = user.IdMesa.ToString();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtid_sesion.Text);
            Ventana_Acceso_Usuario vau = new Ventana_Acceso_Usuario(id);
            vau.Show();
            this.Close();
        }

        private void btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //Variable de mensajes para confirmar procesos
                string mensaje = null;

                EntidadesMesa mesa = new EntidadesMesa();
                mesa.Numero_mesa = int.Parse(txtNum_mesa.Text);
                mesa.IdUsuario = int.Parse(txtid_sesion.Text);
                mesa.IdMesa = int.Parse(txtid_mesa.Text);

                if (txtestado_mesa.Text.Trim().Equals("OCUPADO"))
                {
                    MessageBox.Show("La mesa elejida esta ocupada por otra persona.", "Modificar", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else //Si estoy editando
                {
                    if (MessageBox.Show("¿Desea reservarla mesa?", "Modificar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        
                            //Estableciendo el ID del registro a modificar
                            mesa.IdMesa = int.Parse(txtid_mesa.Text);
                            //Proceso de actualizacion de usuario
                            int usuarioid = DatosMesa.ReservarMesa(mesa);
                            mensaje = "Registro modificado correctamente";
                            MessageBox.Show(mensaje,
                            "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                            string id = txtid_sesion.Text;
                            string id_mes = txtid_mesa.Text;
                            Pedido ped = new Pedido(id, id_mes);
                            ped.Show();
                            this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El producto no se modifico", "Modificar", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
            }
            catch (System.FormatException)
            {

                MessageBox.Show("Ingrese el formato correcto en todos los campos sin dejar \nalguno vacío para poder guardar correctamente.",
                "Error de formato", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
