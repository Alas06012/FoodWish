using FoodWish.CapaDatos;
using FoodWish.CapaEntidades;
using FoodWish.Reportss;
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
    /// Lógica de interacción para Reportes.xaml
    /// </summary>
    public partial class Reportes : UserControl
    {
        public Reportes()
        {
            InitializeComponent();
        }

        int idFact = 0;


        #region Funciones del formulario para conexiones 

        void mostrarTodaFactura()
        {
            dgFacturas.ItemsSource = DatosFactura.mostrarFactura();

            List<EntidadesMetodoPago> list = DatosMetodoPago.MostrarMetodos();
            cmbMetodoP.DisplayMemberPath = "NombreMetodo";
            cmbMetodoP.SelectedValuePath = "IdMetodopago";
            cmbMetodoP.ItemsSource = list;

        }

        void limpiarObjetos()
        {
            txtNombre.Text = "";
            txtBuscar.Text = "";
            txtidFactura.Text = "";
            cmbMetodoP.Text = "";
        }

        void desactivarObjetos()
        {
            txtNombre.IsEnabled = false;
        }


        #endregion



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mostrarTodaFactura();
            desactivarObjetos();
            txtNombre.IsEnabled = false;
            txtidFactura.IsEnabled = false;

        }

        private void dgTipoComida_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EntidadesFactura user = (EntidadesFactura)dgFacturas.SelectedItem;
            //Preguntar si el datagrid tiene filas
            if (user == null)
            {
                return;
            }

            txtNombre.Text = user.UsuarioFact.ToString();
            txtidFactura.Text = user.IdFactura.ToString();
       
            btnFactSelect.IsEnabled = true;



        }

       
        private void btnRestablecer_Click(object sender, RoutedEventArgs e)
        {
            limpiarObjetos();
            desactivarObjetos();
            txtBuscar.Clear();
            
            mostrarTodaFactura();
          
        }

       
        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string texto = txtBuscar.Text;

            dgFacturas.ItemsSource = DatosFactura.BuscarFactura(texto);
        }

        private void btnFactSelect_Click(object sender, RoutedEventArgs e)
        {
            //llamar a visor
            FacturaSeleccionada visor = new FacturaSeleccionada();
            //llamar al rpt
            rptFactSeleccionada rptDet = new rptFactSeleccionada();
            //Carga el reporte al visor
            rptDet.Load(@"rptFactSeleccionada.rep");

            string nombreCliente;
            string idfact;
     

            if (txtNombre.Text == null || String.IsNullOrEmpty(txtNombre.Text))
            {
                nombreCliente = "0";
            }
            else
            {
                nombreCliente = txtNombre.Text;

            }

            if (txtidFactura.Text == null || String.IsNullOrEmpty(txtidFactura.Text) )
            {
                idfact = "0";
            }
            else
            {
                idfact = txtidFactura.Text;

            }


            //Asignar valor a los parametros

            rptDet.SetParameterValue("@idusuario", nombreCliente);

            rptDet.SetParameterValue("@idfactura", idfact);

            rptDet.SetParameterValue("@idmetodo", 0);


            //Se le asigna el origen de datos al rptViewer
            visor.RptViewer.ViewerCore.ReportSource = rptDet;

            //Mostrar el visor
            visor.Show();


        }

        private void btnFactMtd_Click(object sender, RoutedEventArgs e)
        {
            //llamar a visor
            FacturaSeleccionada visor = new FacturaSeleccionada();
            //llamar al rpt
            rptFactSeleccionada rptDet = new rptFactSeleccionada();
            //Carga el reporte al visor
            rptDet.Load(@"rptFactSeleccionada.rep");

            string idmetodo;

            if (cmbMetodoP.SelectedValue == null)
            {
                idmetodo = "0";
            }
            else
            {
                idmetodo = cmbMetodoP.SelectedValue.ToString();
            }

              
            //Asignar valor a los parametros

            rptDet.SetParameterValue("@idusuario", 0);

            rptDet.SetParameterValue("@idfactura", 0);

            rptDet.SetParameterValue("@idmetodo", idmetodo);


            //Se le asigna el origen de datos al rptViewer
            visor.RptViewer.ViewerCore.ReportSource = rptDet;

            //Mostrar el visor
            visor.Show();
        }

        private void btnVentasTotales_Click(object sender, RoutedEventArgs e)
        {
            //llamar a visor
            FacturaSeleccionada visor = new FacturaSeleccionada();
            //llamar al rpt
            rptVentasTotales rptDet = new rptVentasTotales();
            //Carga el reporte al visor
            rptDet.Load(@"rptVentasTotales.rep");

            //Se le asigna el origen de datos al rptViewer
            visor.RptViewer.ViewerCore.ReportSource = rptDet;

            //Mostrar el visor
            visor.Show();



        }

        private void btnComidaVEnd_Click(object sender, RoutedEventArgs e)
        {

            //llamar a visor
            FacturaSeleccionada visor = new FacturaSeleccionada();
            //llamar al rpt
            MasVendido rptDet = new MasVendido();
            //Carga el reporte al visor
            rptDet.Load(@"MasVendido.rep");

            //Se le asigna el origen de datos al rptViewer
            visor.RptViewer.ViewerCore.ReportSource = rptDet;

            //Mostrar el visor
            visor.Show();




        }
    }
}
