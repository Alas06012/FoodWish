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
using FoodWish.Reportss;

namespace FoodWish.Vistas
{
    /// <summary>
    /// Lógica de interacción para Pago.xaml
    /// </summary>
    public partial class Pago : Window
    {
        public Pago(string sesion, string fact, double total, string mesa)
        {
            InitializeComponent();
            txtid_sesion.Text = sesion.ToString();
            txtid_fact.Text = fact.ToString();
            txttotal_fact.Text = total.ToString();
            lblCalculo.Content = total.ToString();
            txtid_mesa.Text = mesa.ToString();
            MostrarTablaPago();
        }

        void MostrarTablaPago()
        {
            EntidadesCarrito fact = new EntidadesCarrito();
            fact.Fact_DetFact = int.Parse(txtid_fact.Text);
            dgCarrito.ItemsSource = DatosCarrito.MostrarCarrito(fact);

            List<EntidadesMetodoPago> list = DatosMetodoPago.MostrarMetodos();
            cmbMetodoP.DisplayMemberPath = "NombreMetodo";
            cmbMetodoP.SelectedValuePath = "IdMetodopago";
            cmbMetodoP.ItemsSource = list;

        }

        private void btnPagar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //Variable de mensajes para confirmar procesos
                string mensaje = null;

                EntidadesFactura facat = new EntidadesFactura();
                facat.MetodoFact = int.Parse(cmbMetodoP.SelectedValue.ToString());
                facat.TotalFact = decimal.Parse(txttotal_fact.Text);

                        //Estableciendo el ID del registro a modificar
                        facat.IdFactura = int.Parse(txtid_fact.Text);
                        //Proceso de actualizacion de usuario
                        int facturapago = DatosFactura.PagoFactura(facat);
                        mensaje = "El pago se realizo correctamente";
                        MessageBox.Show(mensaje,
                        "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                        string id = txtid_sesion.Text;
                        string mesita = txtid_mesa.Text;
                        Pedido ped = new Pedido(id, mesita);
                        ped.Show();


                //llamar a visor
                FacturaSeleccionada visor = new FacturaSeleccionada();
                //llamar al rpt
                rptFactSeleccionada rptDet = new rptFactSeleccionada();
                //Carga el reporte al visor
                rptDet.Load(@"rptFactSeleccionada.rep");

                string idCliente = txtid_sesion.Text;
                string idfact = txtid_fact.Text;

                //Asignar valor a los parametros

                rptDet.SetParameterValue("@idusuario", idCliente);

                rptDet.SetParameterValue("@idfactura", idfact);

                rptDet.SetParameterValue("@idmetodo", 0);


                //Se le asigna el origen de datos al rptViewer
                visor.RptViewer.ViewerCore.ReportSource = rptDet;

                //Mostrar el visor
                visor.Show();



                this.Close();
                        



            }
            catch (System.FormatException)
            {

                MessageBox.Show("Ingrese el formato correcto en todos los campos sin dejar \nalguno vacío para poder guardar correctamente.",
                "Error de formato", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void cmbMetodoP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbMetodoP.SelectedIndex == 1 || cmbMetodoP.SelectedIndex == 2)
            {
                txt_numTarjeta.IsEnabled = true;
                txt_CVV.IsEnabled = true;
                txt_Titular.IsEnabled = true;
                txt_vencimiento.IsEnabled = true;
            }
            else {
                txt_numTarjeta.IsEnabled = false;
                txt_CVV.IsEnabled = false;
                txt_Titular.IsEnabled = false;
                txt_vencimiento.IsEnabled = false;
            }
        }
    }
}
