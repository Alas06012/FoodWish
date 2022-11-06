using FoodWish.CapaDatos;
using FoodWish.CapaEntidades;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodWish.Vistas
{
    /// <summary>
    /// Lógica de interacción para CRUDUSUARIOS.xaml
    /// </summary>
    public partial class CRUDUSUARIOS : UserControl
    {
        public CRUDUSUARIOS()
        {
            InitializeComponent();
        }

        int idUsuario = 0;


        #region Funciones del formulario para conexiones 

        void mostrarTodosUsuarios()
        {
            dgUsuarios.ItemsSource = DatosUsuarios.MostrarUsuarios();
        }

        void limpiarObjetos()
        {
            txtNombre.Text = "";
            txtTipo.Text = "";
            txtBuscar.Text = "";
        }

        void desactivarObjetos()
        {
            btnEliminar.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            txtNombre.IsEnabled = false;
        }






        #endregion



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mostrarTodosUsuarios();
            btnEliminar.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            txtTipo.IsEnabled = false;
            txtNombre.IsEnabled = false;
        }

        private void dgUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EntidadesUsuarios user = (EntidadesUsuarios)dgUsuarios.SelectedItem;
            //Preguntar si el datagrid tiene filas
            if (user == null)
            {
                return;
            }

            txtNombre.Text = user.NombreUsuario;
            txtTipo.Text = user.TipoUsuario.ToString();
            idUsuario = user.IdUsuario;
            btnModificar.IsEnabled = true;
            btnEliminar.IsEnabled = true;

            if (txtTipo.Text == "1" || txtNombre.Text == "Admin") 
            {
                btnEliminar.IsEnabled = false;
                btnModificar.IsEnabled = false;
                txtNombre.IsEnabled = false;
            }


        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            //Validar si hay registros para eliminar
            if (dgUsuarios.Items.Count > 0)
            {
                if (MessageBox.Show("Desea eliminar el registro " + idUsuario + "?", "Confirmación",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    EntidadesUsuarios usuario = new EntidadesUsuarios();
                    //Estableciendo el ID a eliminar
                    usuario.IdUsuario = idUsuario;

                    //Proceder con la eliminacion
                    if (DatosUsuarios.EliminarUsuario(usuario) > 0)
                    {
                        //Mostrar mensaje
                        MessageBox.Show("Registro eliminado correctamente", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Limpiar formulario
                        limpiarObjetos();

                        //Recargar DataGrid
                        mostrarTodosUsuarios();

                        //Reiniciar las variables de estado
                        desactivarObjetos();
                        //Control de botones

                    }
                }//Fin de confirmacion
            }//Fin de validacion de DataGrid
            else
            {
                MessageBox.Show("No hay nada seleccionado.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                limpiarObjetos();
                desactivarObjetos();
                mostrarTodosUsuarios();
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            btnEliminar.IsEnabled = true;
            btnGuardar.IsEnabled = true;
            txtNombre.IsEnabled = true;
        }

        private void btnRestablecer_Click(object sender, RoutedEventArgs e)
        {
            limpiarObjetos();
            desactivarObjetos();
            txtBuscar.Clear();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            //Validar si hay registros para eliminar
            if (dgUsuarios.Items.Count > 0)
            {
                if (MessageBox.Show("Desea modificar el registro " + idUsuario + "?", "Confirmación",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    EntidadesUsuarios usuario = new EntidadesUsuarios();
                    //Estableciendo el ID a eliminar
                    usuario.IdUsuario = idUsuario;
                    usuario.NombreUsuario = txtNombre.Text;

                    if (DatosUsuarios.BuscarIgual(usuario.NombreUsuario) > 0)
                    {
                        MessageBox.Show("El nombre de usuario ya existe, intente con uno nuevo.", "Modificar", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {

                        //Proceder con la eliminacion
                        if (DatosUsuarios.ModificarUsuario(usuario) > 0)
                        {
                            //Mostrar mensaje
                            MessageBox.Show("El Registro se modifico correctamente", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);

                            //Limpiar formulario
                            limpiarObjetos();

                            //Recargar DataGrid
                            mostrarTodosUsuarios();

                            //Reiniciar las variables de estado

                            desactivarObjetos();
                            //Control de botones

                        }
                    }
                }//Fin de confirmacion
            }//Fin de validacion de DataGrid
            else
            {
                MessageBox.Show("No hay nada seleccionado.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                limpiarObjetos();
                desactivarObjetos();
                mostrarTodosUsuarios();
            }
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string texto = txtBuscar.Text;

            dgUsuarios.ItemsSource = DatosUsuarios.BuscarUsuario(texto);

        }





    }
}
