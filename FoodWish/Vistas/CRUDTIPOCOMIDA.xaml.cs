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
    /// Lógica de interacción para CRUDTIPOCOMIDA.xaml
    /// </summary>
    public partial class CRUDTIPOCOMIDA : UserControl
    {
        public CRUDTIPOCOMIDA()
        {
            InitializeComponent();
        }


        int idTipoComida = 0;


        #region Funciones del formulario para conexiones 

        void mostrarTodaComida()
        {
            dgTipoComida.ItemsSource = DatosTipoComida.MostrarComida();
            List<EntidadesTipoComida> list = DatosComida.llenarCombobox();
        }

        void limpiarObjetos()
        {
            txtNombre.Text = "";
            txtBuscar.Text = "";
            txtID.Text = "";
        }

        void desactivarObjetos()
        {
            btnEliminar.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            txtNombre.IsEnabled = false;
            txtID.IsEnabled = false;
            txtNombre.IsEnabled = false;
        }


        #endregion



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mostrarTodaComida();
            desactivarObjetos();
        }

        private void dgTipoComida_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EntidadesTipoComida user = (EntidadesTipoComida)dgTipoComida.SelectedItem;
            //Preguntar si el datagrid tiene filas
            if (user == null)
            {
                return;
            }

            txtNombre.Text = user.NombreTipo;
            txtID.Text = user.IdTipoComida.ToString();
            btnModificar.IsEnabled = true;
            btnEliminar.IsEnabled = true;
            idTipoComida = user.IdTipoComida;

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            //Validar si hay registros para eliminar
            if (dgTipoComida.Items.Count > 0)
            {
                if (MessageBox.Show("Desea eliminar el registro " + idTipoComida + "?", "Confirmación",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    EntidadesTipoComida usuario = new EntidadesTipoComida();
                    //Estableciendo el ID a eliminar
                    usuario.IdTipoComida = idTipoComida;

                    //Proceder con la eliminacion
                    if (DatosTipoComida.EliminarComida(usuario) > 0)
                    {
                        //Mostrar mensaje
                        MessageBox.Show("Registro eliminado correctamente", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Limpiar formulario
                        limpiarObjetos();

                        //Recargar DataGrid
                        mostrarTodaComida();

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
                mostrarTodaComida();
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            btnEliminar.IsEnabled = true;
            btnGuardar.IsEnabled = true;
            txtNombre.IsEnabled = true;
            btnAgregar.IsEnabled = false;
        }

        private void btnRestablecer_Click(object sender, RoutedEventArgs e)
        {
            limpiarObjetos();
            desactivarObjetos();
            txtBuscar.Clear();
            btnAgregar.IsEnabled = true;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //Variable de mensajes para confirmar procesos
                string mensaje = null;

                EntidadesTipoComida comida = new EntidadesTipoComida();
                comida.NombreTipo = txtNombre.Text;

                if (idTipoComida == 0)
                {
                    if (MessageBox.Show("Desea agregar la nueva categoria?", "Agregar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //Proceso de insercion de nuevo usuario
                        int comidaid = DatosTipoComida.InsertarComida(comida);
                        mensaje = "Registro insertado correctamente";
                        MessageBox.Show(mensaje,
                        "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("La cateogoria no se agrego", "Agregar", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
                else //Si estoy editando
                {
                    if (MessageBox.Show("Desea modificar el nombre de la categoria?", "Modificar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //Estableciendo el ID del registro a modificar
                        comida.IdTipoComida = idTipoComida;
                        //Proceso de actualizacion de usuario
                        int usuarioid = DatosTipoComida.ModificarComida(comida);
                        mensaje = "Registro modificado correctamente";
                        MessageBox.Show(mensaje,
                        "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("El producto no se modifico", "Modificar", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }

                //Limpiar formulario
                limpiarObjetos();

                //Bloquerar objetos de formulario
                desactivarObjetos();
                btnAgregar.IsEnabled = true;

                //Recargar el DataGrid con el nuevo cambio
                mostrarTodaComida();
            }
            catch (System.FormatException)
            {

                MessageBox.Show("Ingrese el formato correcto en todos los campos sin dejar \nalguno vacío para poder guardar correctamente.",
                "Error de formato", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            btnEliminar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            btnGuardar.IsEnabled = true;
            idTipoComida = 0;
            limpiarObjetos();
            txtNombre.IsEnabled = true;
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string texto = txtBuscar.Text;

            dgTipoComida.ItemsSource = DatosTipoComida.BuscarComida(texto);
        }



    }
}
