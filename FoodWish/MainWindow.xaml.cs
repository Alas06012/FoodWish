using FoodWish.Vistas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using FoodWish.Models;
using FoodWish.Services;


namespace FoodWish
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        #region botones minimizar, cerrar, movimiento de form
        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }


        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void btnMinimizar_MouseEnter(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = Brushes.DeepPink;
            }
        }

        private void btnMinimizar_MouseLeave(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = Brushes.Transparent;
            }
        }

     
        private void btnCerrar_MouseEnter(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = Brushes.DeepPink;
            }
        }

        private void btnCerrar_MouseLeave(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;

            // If source is a Button.
            if (source != null)
            {
                source.Background = Brushes.Transparent;
            }

        }


        #endregion

        SqlConnection cn = new SqlConnection(Properties.Settings.Default.ConexionBD);
        string consultaSQL = null;
        public MainWindow()
        {
            InitializeComponent();

            SqlCommand cmd = new SqlCommand("SELECT*FROM Usuario", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
               
            }
            else
            {
                registro_admin admin = new registro_admin();
                admin.Show();
                this.Close();
            }
            cn.Close();
        }
        public static class Global
        {
            public static string nombre;
        }


        #region Encontrar Usuario 
        void EncontrarUsuario()
        {
            int resultado = 0;

            cn.Open();

            //Consulta SQL para buscar usuario
            consultaSQL = null;
            consultaSQL = "SELECT DBO.FNENCONTRARUSUARIO(@nombre_usuario,@contrasena)";

            SqlCommand sqlCmd = new SqlCommand(consultaSQL, cn);
            sqlCmd.CommandType = CommandType.Text;
            //Enviar valores por parametro
            sqlCmd.Parameters.AddWithValue("@nombre_usuario", txt_usuario.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@contrasena", txt_clave.Password.Trim());

            //Ejecutar consulta SQL
            resultado = Convert.ToInt32(sqlCmd.ExecuteScalar());

            //Evaluar el resultado
            if (resultado == 1)
            {
                if (txt_usuario.Text=="Admin")
                {
                    VentanaMantenimientos admin = new VentanaMantenimientos();
                    admin.Show();
                    this.Close();
                }
                else
                {
                    SqlCommand idusua = new SqlCommand("select u.id_usuario from Usuario u where u.nombre_usuario = @nombre_usuario", cn);
                    idusua.Parameters.AddWithValue("@nombre_usuario", txt_usuario.Text.Trim());
                    SqlDataReader registro = idusua.ExecuteReader();
                    int id_usu = 0;
                    if (registro.Read())
                    {
                        id_usu = int.Parse(registro["id_usuario"].ToString());
                    }
                    
                    Ventana_Acceso_Usuario ventana = new Ventana_Acceso_Usuario(id_usu);
                    ventana.Show();
                    this.Close();


                }             
            }
            else
            {
                bool? Result = new MessageBoxCustom("Usuario o contraseña incorrectos.", MessageType.Info, MessageButtons.Ok).ShowDialog();


            }

            //cerrar la base de datos
            cn.Close();
        }
        #endregion

        /*public void logear (string usuario, string password)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand("SELECT tipo_usu FROM Usuario WHERE nombre_usuario = @usuario AND contrasena = @pas", cn);
            cmd.Parameters.AddWithValue("usuario", usuario);
            cmd.Parameters.AddWithValue("pas", password);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count==1)
            {

                this.Hide();
                if (dt.Rows[0][1].ToString()=="1")
                {


                }
                else if (dt.Rows[0][1].ToString() == "2")
                {
                    Ventana_Acceso_Usuario view = new Ventana_Acceso_Usuario();
                    view.Show();
                    this.Hide();
                    
                        
                }
            }
            else
            {
                MessageBox.Show("Usuario o Contraseña Incorrectos..");
            }
            cn.Close();
        }*/

         


        private void btn_registrar_Click(object sender, RoutedEventArgs e)
        {
            Nuevo_Usuario registrar = new Nuevo_Usuario();
            registrar.Show();
            this.Close();
        }

 
        private void lbl_olvidaste_contra_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Recuperacion recu = new Recuperacion();
            recu.Show();
            this.Close();
        }

        private void btn_iniciar_sesion_Click(object sender, RoutedEventArgs e)
        {
            /*logear(this.txt_usuario.Text, this.txt_clave.Password);*/
            EncontrarUsuario();
            
        }

        private void txt_clave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EncontrarUsuario();
                this.Close();
            }
        }
    }
}
