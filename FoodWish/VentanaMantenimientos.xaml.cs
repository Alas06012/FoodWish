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

namespace FoodWish
{
    /// <summary>
    /// Lógica de interacción para VentanaMantenimientos.xaml
    /// </summary>
    public partial class VentanaMantenimientos : Window
    {
        public VentanaMantenimientos()
        {
            InitializeComponent();
            
        }


        #region Eventos estéticos de cada botón
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximizar_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMesas_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cast the source of the event to a Button.
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = new SolidColorBrush(Color.FromArgb(255, 235, 132, 132));
                source.Foreground = new SolidColorBrush(Color.FromArgb(255, 38, 40, 43));
            }


        }

        private void btnMesas_MouseLeave(object sender, MouseEventArgs e)
        {
            // Cast the source of the event to a Button.
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = Brushes.Transparent;
            }
        }

        private void btnComidas_MouseEnter(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {

                source.Background = new SolidColorBrush(Color.FromArgb(255, 235, 132, 132));
                source.Foreground = new SolidColorBrush(Color.FromArgb(255, 38, 40, 43));
            }
        }

        private void btnComidas_MouseLeave(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = Brushes.Transparent;
                source.Foreground = Brushes.Gray;
            }
        }

        private void btnTipoComida_MouseEnter(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = new SolidColorBrush(Color.FromArgb(255, 235, 132, 132));
                source.Foreground = new SolidColorBrush(Color.FromArgb(255, 38, 40, 43));
            }
        }

        private void btnTipoComida_MouseLeave(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = Brushes.Transparent;
                source.Foreground = Brushes.Gray;
            }
        }

        private void btnUsuarios_MouseEnter(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = new SolidColorBrush(Color.FromArgb(255, 235, 132, 132));
                source.Foreground = new SolidColorBrush(Color.FromArgb(255, 38, 40, 43));
            }
        }


        private void btnUsuarios_MouseLeave(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = Brushes.Transparent;
                source.Foreground = Brushes.Gray;
            }
        }

        private void btnMinimizar_MouseEnter(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = new SolidColorBrush(Color.FromArgb(255, 235, 132, 132));
                source.Foreground = new SolidColorBrush(Color.FromArgb(255, 38, 40, 43));
            }
        }

        private void btnMinimizar_MouseLeave(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = Brushes.Transparent; 
                source.Foreground = Brushes.Gray;
            }
        }

        private void btnMaximizar_MouseEnter(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = new SolidColorBrush(Color.FromArgb(255, 235, 132, 132));
                source.Foreground = new SolidColorBrush(Color.FromArgb(255, 38, 40, 43));
            }
        }
        private void btnMaximizar_MouseLeave(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = Brushes.Transparent;
                source.Foreground = Brushes.Gray;
            }
        }

        private void btnCerrar_MouseEnter(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = new SolidColorBrush(Color.FromArgb(255, 235, 132, 132));
                source.Foreground = new SolidColorBrush(Color.FromArgb(255, 38, 40, 43));
            }
        }

        private void btnCerrar_MouseLeave(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = Brushes.Transparent;
                source.Foreground = Brushes.Gray;
            }

        }

        private void btnMetodo_MouseEnter(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = new SolidColorBrush(Color.FromArgb(255, 235, 132, 132));
                source.Foreground = new SolidColorBrush(Color.FromArgb(255, 38, 40, 43));
            }
        }

        private void btnMetodo_MouseLeave(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = Brushes.Transparent;
                source.Foreground = Brushes.Gray;
            }
        }

        private void btnReportes_MouseEnter(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = new SolidColorBrush(Color.FromArgb(255, 235, 132, 132));
                source.Foreground = new SolidColorBrush(Color.FromArgb(255,38,40,43));
            }
        }
        private void btnReportes_MouseLeave(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = Brushes.Transparent;
                source.Foreground = Brushes.Gray;
            }
        }



        #endregion


        void AgregarPagina(UserControl control)
        {
            this.StackVENTANAS.Children.Clear();
            this.StackVENTANAS.Children.Add(control);

        }

        private void btnMesas_Click(object sender, RoutedEventArgs e)
        {
            AgregarPagina(new Vistas.CRUDMESAS());
        }

        private void btnComidas_Click(object sender, RoutedEventArgs e)
        {
            AgregarPagina(new Vistas.CRUDCOMIDA());
        }

        private void btnTipoComida_Click(object sender, RoutedEventArgs e)
        {
            AgregarPagina(new Vistas.CRUDTIPOCOMIDA());
        }

        private void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            AgregarPagina(new Vistas.CRUDUSUARIOS());
        }

        private void btnMetodo_Click(object sender, RoutedEventArgs e)
        {
            AgregarPagina(new Vistas.CRUDMETODOPAGO());
        }

        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {
            AgregarPagina(new Vistas.Reportes());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool? Result = new MessageBoxCustom("¿Está seguro de cerrar sesión? ", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();

        if (Result.Value)
            {
                MainWindow wpf = new MainWindow();
                wpf.Show();
                this.Close();
            }
        }
    }
}
