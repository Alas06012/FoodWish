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
using System.Text.RegularExpressions;

namespace FoodWish.Vistas
{
    /// <summary>
    /// Lógica de interacción para Pedido.xaml
    /// </summary>
    public partial class Pedido : Window
    {
        public double totalfactt = 0.00;
        public Pedido(string idsesion, string mesa)
        {
            InitializeComponent();
            
            txtid_sesion.Text = idsesion.ToString();
            txtid_mesa.Text = mesa.ToString();
        }
        public Pedido(string idsesion)
        {
            InitializeComponent();

            txtid_sesion.Text = idsesion.ToString();
        }

        void MostrarTablaMesa()
        {
            dgPlatos.ItemsSource = DatosComida.MostrarPlatos();
            dgBebidas.ItemsSource = DatosComida.MostrarBebidas();
            dgComplementos.ItemsSource = DatosComida.MostrarComplementos();
            dgPostres.ItemsSource = DatosComida.MostrarPostres();
            EntidadesCarrito fact = new EntidadesCarrito();
            fact.Fact_DetFact = int.Parse(txtid_factura.Text);
            dgCarrito.ItemsSource = DatosCarrito.MostrarCarrito(fact);

            //label de total de factura:
            lblCalculo.Content = totalfactt;

        }

        private void dgPlatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EntidadesComida user = (EntidadesComida)dgPlatos.SelectedItem;
            //Preguntar si el datagrid tiene filas
            if (user == null)
            {
                return;
            }

            txtNombre.Text = user.Nombre.ToString();
            txtDescripcion.Text = user.Descripcion.ToString();
            txtid_comida.Text = user.IdComida.ToString();
            txtPrecio.Text = user.Precio.ToString();
        }

        private void dgBebidas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EntidadesComida user = (EntidadesComida)dgBebidas.SelectedItem;
            //Preguntar si el datagrid tiene filas
            if (user == null)
            {
                return;
            }

            txtNombre.Text = user.Nombre.ToString();
            txtDescripcion.Text = user.Descripcion.ToString();
            txtid_comida.Text = user.IdComida.ToString();
            txtPrecio.Text = user.Precio.ToString();
        }

        private void dgComplementos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EntidadesComida user = (EntidadesComida)dgComplementos.SelectedItem;
            //Preguntar si el datagrid tiene filas
            if (user == null)
            {
                return;
            }

            txtNombre.Text = user.Nombre.ToString();
            txtDescripcion.Text = user.Descripcion.ToString();
            txtid_comida.Text = user.IdComida.ToString();
            txtPrecio.Text = user.Precio.ToString();
        }

        private void dgPostres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EntidadesComida user = (EntidadesComida)dgPostres.SelectedItem;
            //Preguntar si el datagrid tiene filas
            if (user == null)
            {
                return;
            }

            txtNombre.Text = user.Nombre.ToString();
            txtDescripcion.Text = user.Descripcion.ToString();
            txtid_comida.Text = user.IdComida.ToString();
            txtPrecio.Text = user.Precio.ToString();
        }

        private void dgCarrito_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EntidadesCarrito user = (EntidadesCarrito)dgCarrito.SelectedItem;
            //Preguntar si el datagrid tiene filas
            if (user == null)
            {
                return;
            }

            txtid_carrito.Text = user.Id_Carrito.ToString();
            txtpre_edit.Text = user.Precio_Car.ToString();
            txtcant_edit.Text = user.Cantidad_Car.ToString();
            txtCantidad.Text = user.Cantidad_Car.ToString();
            txtPrecio.Text = user.Precio_Car.ToString();
            txtNombre.Text = user.Nom_Comida.ToString();
            txtDescripcion.Text = "";
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtid_mesa.Text.Trim().Equals(""))
                {
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }
                else
                {
                    //Variable de mensajes para confirmar procesos
                    string mensaje = null;

                    EntidadesMesa mesa = new EntidadesMesa();
                    mesa.IdMesa = int.Parse(txtid_mesa.Text);
                    //Proceso de insercion de nuevo usuario

                    int facturaid = DatosMesa.LiberarMesa(mesa);
                    mensaje = "Muchas gracias por preferirnos";
                    MessageBox.Show(mensaje,
                    "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }

            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Verifique que no existan campos vacíos.",
             "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPagar_Click(object sender, RoutedEventArgs e)
        {
            string sesion = txtid_sesion.Text;
            string id_fact = txtid_factura.Text;
            string mess = txtid_mesa.Text;
            Pago pag = new Pago(sesion, id_fact, totalfactt, mess);
            pag.Show();
            this.Close();
        }

        private void btnGenerarFactura_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            //Variable de mensajes para confirmar procesos
            string mensaje = null;

            EntidadesFactura fact = new EntidadesFactura();
            fact.UsuarioFact = int.Parse(txtid_sesion.Text);
                if (MessageBox.Show("Desea generar una nueva factura?", "Agregar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    //Proceso de insercion de nuevo usuario
                    int facturaid = DatosFactura.GenerarFactura(fact);
                    mensaje = "Factura generada";
                    MessageBox.Show(mensaje,
                    "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                    int last = DatosFactura.LastFactura();
                    txtid_factura.Text = Convert.ToString(last);
                    MostrarTablaMesa();
                    btnCerrarSesion.IsEnabled = false;
                    btnAgregar.IsEnabled = true;
                    btnEditar.IsEnabled = true;
                    btnEliminar.IsEnabled = true;
                    btnGenerarFactura.IsEnabled = false;

                }
                else
                {
                    MessageBox.Show("No se genero su factura", "Agregar", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Verifique que no existan campos vacíos.",
             "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (txtCantidad.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Verifique que no existan campos vacíos.",
             "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Regex rx = new Regex("^[0-9]*");
                    if (rx.IsMatch(txtCantidad.Text))
                    {
                        //Variable de mensajes para confirmar procesos
                        double precioC = Convert.ToDouble(txtPrecio.Text);
                        int cantidadC = Convert.ToInt32(txtCantidad.Text);
                        totalfactt = totalfactt + (precioC * cantidadC);
                        string mensaje = null;

                        EntidadesCarrito fact = new EntidadesCarrito();
                        fact.Comida_Car = int.Parse(txtid_comida.Text);
                        fact.Usuario_Car = int.Parse(txtid_sesion.Text);
                        fact.Cantidad_Car = int.Parse(txtCantidad.Text);
                        fact.Precio_Car = decimal.Parse(txtPrecio.Text);
                        fact.Fact_DetFact = int.Parse(txtid_factura.Text);
                        //Proceso de insercion de nuevo usuario
                        int facturaid = DatosCarrito.IngresarCarrito(fact);
                        mensaje = "Producto ingresado";
                        MessageBox.Show(mensaje,
                        "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                        MostrarTablaMesa();
                        btnPagar.IsEnabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Escriba solo numeros enteros en la cantidad.",
                        "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Verifique que no existan campos vacíos.",
             "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                        double precioC = Convert.ToDouble(txtPrecio.Text);
                        int cantidadC = Convert.ToInt32(txtCantidad.Text);
                        totalfactt = totalfactt - (precioC * cantidadC);
                        string mensaje = null;

                        EntidadesCarrito fact = new EntidadesCarrito();
                        fact.Id_Carrito = int.Parse(txtid_carrito.Text);
                        //Proceso de insercion de nuevo usuario
                        int facturaid = DatosCarrito.EliminarCarrito(fact);
                        mensaje = "Producto eliminado";
                        MessageBox.Show(mensaje,
                        "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                        MostrarTablaMesa();
                    

            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Verifique que no existan campos vacíos.",
             "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtCantidad.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Verifique que no existan campos vacíos.",
             "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Regex rx = new Regex("^[0-9]*");
                    if (rx.IsMatch(txtCantidad.Text))
                    {
                        double precioC_edit = Convert.ToDouble(txtpre_edit.Text);
                        int cantidadC_edit = Convert.ToInt16(txtcant_edit.Text);

                        double precioC = Convert.ToDouble(txtPrecio.Text);
                        int cantidadC = Convert.ToInt16(txtCantidad.Text);

                        double sub = precioC_edit * cantidadC_edit;
                        totalfactt = totalfactt - sub;

                        totalfactt = totalfactt + (precioC * cantidadC);
                        //Variable de mensajes para confirmar procesos
                        string mensaje = null;

                        EntidadesCarrito fact = new EntidadesCarrito();
                        fact.Cantidad_Car = int.Parse(txtCantidad.Text);
                        fact.Precio_Car = decimal.Parse(txtPrecio.Text);
                        fact.Id_Carrito = int.Parse(txtid_carrito.Text);
                        //Proceso de insercion de nuevo usuario
                
                        int facturaid = DatosCarrito.EditarCarrito(fact);
                        mensaje = "Producto modificado";
                        MessageBox.Show(mensaje,
                        "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                
                        MostrarTablaMesa();
                    }
                    else
                    {
                        MessageBox.Show("Escriba solo numeros enteros en la cantidad.",
                        "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }

            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Verifique que no existan campos vacíos.",
             "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
