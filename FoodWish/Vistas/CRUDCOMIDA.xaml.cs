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
    /// Lógica de interacción para CRUDCOMIDA.xaml
    /// </summary>
    public partial class CRUDCOMIDA : UserControl
    {
        public CRUDCOMIDA()
        {
            InitializeComponent();
            mostrarTodaComida();
            desactivarObjetos();
        }

        int idComida = 0;


        #region Funciones del formulario para conexiones 

        void mostrarTodaComida()
        {
            dgComida.ItemsSource = DatosComida.MostrarComida();
            List<EntidadesTipoComida> list = DatosComida.llenarCombobox();
            txtTipo.DisplayMemberPath = "NombreTipo";
            txtTipo.SelectedValuePath = "IdTipoComida";
            txtTipo.ItemsSource = list;

        }

        void limpiarObjetos()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtBuscar.Text = "";
            txtPrecio.Text = "";
            txtTipo.Text = "";
        }

        void desactivarObjetos()
        {
            btnEliminar.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            txtNombre.IsEnabled = false;
            txtDescripcion.IsEnabled = false;
            txtTipo.IsEnabled = false;
            txtPrecio.IsEnabled = false;
        }

        #endregion



        private void dgComida_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EntidadesComida user = (EntidadesComida)dgComida.SelectedItem;
            //Preguntar si el datagrid tiene filas
            if (user == null)
            {
                return;
            }

            txtNombre.Text = user.Nombre;
            txtDescripcion.Text = user.Descripcion;
            txtPrecio.Text = user.Precio.ToString();
            txtTipo.SelectedValue = user.Tipo.ToString();
            btnModificar.IsEnabled = true;
            btnEliminar.IsEnabled = true;
            idComida = user.IdComida;


        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            //Validar si hay registros para eliminar
            if (dgComida.Items.Count > 0)
            {
                if (MessageBox.Show("Desea eliminar el registro " + idComida + "?", "Confirmación",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    EntidadesComida usuario = new EntidadesComida();
                    //Estableciendo el ID a eliminar
                    usuario.IdComida = idComida;

                    //Proceder con la eliminacion
                    if (DatosComida.EliminarComida(usuario) > 0)
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
            txtPrecio.IsEnabled = true;
            txtDescripcion.IsEnabled = true;
            txtTipo.IsEnabled = true;
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

                
                if (txtTipo.SelectedValue != null)
                {
                    //Variable de mensajes para confirmar procesos
                    string mensaje = null;

                    EntidadesComida comida = new EntidadesComida();
                    comida.Nombre = txtNombre.Text;
                    comida.Precio = decimal.Parse(txtPrecio.Text);
                    comida.Tipo = int.Parse(txtTipo.SelectedValue.ToString());
                    comida.Descripcion = txtDescripcion.Text;
                    if (idComida == 0)
                    {
                        if (MessageBox.Show("Desea agregar el nuevo producto?", "Agregar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            //Proceso de insercion de nuevo usuario
                            int comidaid = DatosComida.InsertarComida(comida);
                            mensaje = "Registro insertado correctamente";
                            MessageBox.Show(mensaje,
                            "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("El producto no se agrego", "Agregar", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                    }
                    else //Si estoy editando
                    {
                        if (MessageBox.Show("Desea modificar el producto?", "Modificar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            //Estableciendo el ID del registro a modificar
                            comida.IdComida = idComida;
                            //Proceso de actualizacion de usuario
                            int usuarioid = DatosComida.ModificarComida(comida);
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

                    //Recargar el DataGrid con el nuevo cambio
                    mostrarTodaComida();

                }
                else
                {

                    MessageBox.Show("Verifique que no existan campos vacíos.",
                 "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Error);
                }
               

               
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Verifique que no existan campos vacíos.",
             "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.FormatException)
            {

                MessageBox.Show("Ingrese el formato correcto en todos los campos sin dejar \nalguno vacío para poder guardar correctamente.",
                "Error de formato", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            


        }//Fin usuarioid

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            btnEliminar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            btnGuardar.IsEnabled = true;
            idComida = 0;
            limpiarObjetos();
            txtNombre.IsEnabled = true;
            txtDescripcion.IsEnabled = true;
            txtPrecio.IsEnabled = true;
            txtTipo.IsEnabled = true;
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string texto = txtBuscar.Text;

            dgComida.ItemsSource = DatosComida.BuscarComida(texto);

        }

     
    }
}
