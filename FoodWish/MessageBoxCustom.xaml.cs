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
    /// Lógica de interacción para MessageBoxCustom.xaml
    /// </summary>
    public partial class MessageBoxCustom : Window
    {
        public MessageBoxCustom(string Message, MessageType Type, MessageButtons Buttons)
        {
            InitializeComponent();
            txtMessage.Text = Message;
            switch (Type)
            {

                case MessageType.Info:
                    txtTitle.Text = "Información";
                    break;
                case MessageType.Confirmation:
                    txtTitle.Text = "Confirmación";
                    break;
                case MessageType.Success:
                    {
                        txtTitle.Text = "Exitoso";
                    }
                    break;
                case MessageType.Warning:
                    txtTitle.Text = "Advertencia";
                    break;
                case MessageType.Error:
                    {
                        txtTitle.Text = "Error";
                    }
                    break;
            }
            switch (Buttons)
            {
                case MessageButtons.OkCancel:
                    btnYes.Visibility = Visibility.Collapsed; btnNo.Visibility = Visibility.Collapsed;
                    break;
                case MessageButtons.YesNo:
                    btnOk.Visibility = Visibility.Collapsed; btnCancel.Visibility = Visibility.Collapsed;
                    break;
                case MessageButtons.Ok:
                    btnOk.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Collapsed;
                    btnYes.Visibility = Visibility.Collapsed; btnNo.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnClose_MouseEnter(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = new SolidColorBrush(Color.FromRgb(235, 132, 132));
                source.Foreground = new SolidColorBrush(Color.FromArgb(255, 38, 40, 43));
            }

        }

        private void btnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = Brushes.Transparent;
                source.Foreground = Brushes.Gray;
            }
        }

        private void cardHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
    public enum MessageType
    {
        Info,
        Confirmation,
        Success,
        Warning,
        Error,
    }
    public enum MessageButtons
    {
        OkCancel,
        YesNo,
        Ok,
    }
}

