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
    /// Lógica de interacción para CRUDMESAS.xaml
    /// </summary>
    public partial class CRUDMESAS : UserControl
    {
        public CRUDMESAS()
        {
            InitializeComponent();
        }

        int idMesa = 0;
        int idUsuario;


        #region Funciones del formulario para conexiones 

        void mostrarTodaMesa()
        {
            dgMesa.ItemsSource = DatosMesa.MostrarMesas();
            List<EntidadesEstadoMesa> list = DatosMesa.llenarCombobox();
            txtEstado.DisplayMemberPath = "NombreEstado";
            txtEstado.SelectedValuePath = "IdEstadoMesa";
            txtEstado.ItemsSource = list;

        }

        void limpiarObjetos()
        {
            txtEstado.Text = "";
            txtNumero.Text = "";
            txtBuscar.Text = "";
        }

        void desactivarObjetos()
        {
            btnEliminar.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            txtNumero.IsEnabled = false;
            txtEstado.IsEnabled = false;
        }


        #endregion


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mostrarTodaMesa();
            desactivarObjetos();
        }

        private void dgMesa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EntidadesMesa user = (EntidadesMesa)dgMesa.SelectedItem;
            //Preguntar si el datagrid tiene filas
            if (user == null)
            {
                return;
            }

            txtNumero.Text = user.Numero_mesa.ToString();
            txtEstado.SelectedValue = user.IdEstado_mesa.ToString();
            btnModificar.IsEnabled = true;
            btnEliminar.IsEnabled = true;
            idMesa = user.IdMesa;
            idUsuario = user.IdUsuario;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            //Validar si hay registros para eliminar
            if (dgMesa.Items.Count > 0)
            {
                if (MessageBox.Show("Desea eliminar el registro " + idMesa + "?", "Confirmación",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    EntidadesMesa usuario = new EntidadesMesa();
                    //Estableciendo el ID a eliminar
                    usuario.IdMesa = idMesa;
                    int aux = int.Parse(txtEstado.SelectedValue.ToString());

                    if (aux == 2)
                    {
                        MessageBox.Show("La mesa esta siendo utilizada, no puede eliminarse", "Eliminar", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        //Proceder con la eliminacion
                        if (DatosMesa.EliminarMesa(usuario) > 0)
                        {
                            //Mostrar mensaje
                            MessageBox.Show("Registro eliminado correctamente", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);

                            //Limpiar formulario
                            limpiarObjetos();

                            //Recargar DataGrid
                            mostrarTodaMesa();

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
                mostrarTodaMesa();
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            btnEliminar.IsEnabled = true;
            btnGuardar.IsEnabled = true;
            txtNumero.IsEnabled = true;
            btnAgregar.IsEnabled = false;
        }

        private void btnRestablecer_Click(object sender, RoutedEventArgs e)
        {
            limpiarObjetos();
            desactivarObjetos();
            txtBuscar.Clear();
            btnAgregar.IsEnabled = true;
            mostrarTodaMesa();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //Variable de mensajes para confirmar procesos
                string mensaje = null;

                EntidadesMesa mesa = new EntidadesMesa();
                mesa.Numero_mesa = int.Parse(txtNumero.Text);
                mesa.IdUsuario = idUsuario;
                mesa.IdEstado_mesa = 1;
                mesa.IdMesa = idMesa;

                if (idMesa == 0)
                {
                    if (MessageBox.Show("Desea agregar el nuevo producto?", "Agregar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (DatosMesa.BuscarIgual(mesa.Numero_mesa) > 0)
                        {
                            MessageBox.Show("El número de mesa ya existe, intente con uno nuevo.", "Agregar", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            mesa.IdEstado_mesa = 1;
                            mesa.IdUsuario = 0;
                            //Proceso de insercion de nuevo usuario
                            int comidaid = DatosMesa.InsertarMesa(mesa);
                            mensaje = "Registro insertado correctamente";
                            MessageBox.Show(mensaje,
                            "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
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
                        if (DatosMesa.BuscarIgual(mesa.Numero_mesa) > 0)
                        {
                            MessageBox.Show("El número de mesa ya existe, intente con uno nuevo.", "Modificar", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            //Estableciendo el ID del registro a modificar
                            mesa.IdMesa = idMesa;
                            mesa.IdEstado_mesa = int.Parse(txtEstado.SelectedValue.ToString());
                            //Proceso de actualizacion de usuario
                            int usuarioid = DatosMesa.ModificarMesa(mesa);
                            mensaje = "Registro modificado correctamente";
                            MessageBox.Show(mensaje,
                            "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
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
                mostrarTodaMesa();
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
            idMesa = 0;
            idUsuario = 0;
            limpiarObjetos();
            txtNumero.IsEnabled = true;
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string texto = txtBuscar.Text;
            if (texto == "")
            {
                mostrarTodaMesa();
            }
            else
            {
                dgMesa.ItemsSource = DatosMesa.BuscarMesa(texto);
            }


        }



    }
}
