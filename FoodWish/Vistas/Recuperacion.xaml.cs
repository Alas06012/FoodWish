using FoodWish.Models;
using FoodWish.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FoodWish.Vistas
{
    /// <summary>
    /// Lógica de interacción para Recuperacion.xaml
    /// </summary>
    public partial class Recuperacion : Window
    {
        SqlConnection cn = new SqlConnection(Properties.Settings.Default.ConexionBD);
        string consultaSQL = null;
        public Recuperacion()
        {
            InitializeComponent();

            SqlCommand cmd = new SqlCommand("select*from Preguntas", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmb_pregunta.Items.Add(dr.GetString(1));
            }
            cn.Close();
        }

        int contador=0;
        int usuarioid = 0;
        void Activar_Part1()              
        {

            lbl_pregunta.Visibility =Visibility.Visible;
            lbl_respuesta.Visibility = Visibility.Visible;
            cmb_pregunta.Visibility = Visibility.Visible;
            txt_respuesta.Visibility = Visibility.Visible;
            btn_aceptar.Visibility = Visibility.Visible;


        }

       public  void EncontrarUsuario()
        {
            int resultado = 0;

            cn.Open();

            //Consulta SQL para buscar usuario
            consultaSQL = null;
            consultaSQL = "SELECT DBO.FNENCONTRARUSUARIO2(@nombre_usuario)";

            SqlCommand sqlCmd = new SqlCommand(consultaSQL, cn);
            sqlCmd.CommandType = CommandType.Text;
            //Enviar valores por parametro
            sqlCmd.Parameters.AddWithValue("@nombre_usuario", txt_nom_usuario.Text.Trim());
            

            //Ejecutar consulta SQL
            resultado = Convert.ToInt32(sqlCmd.ExecuteScalar());

            //Evaluar el resultado
            if (resultado == 1)
            {
                Activar_Part1();

                contador = 1;
                
            }
            else
            {             
                MessageBox.Show("Usuario no encontrado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //cerrar la base de datos
            cn.Close();
        }
        public void EncontrarPregunta()
        {
            
            string valor1=null;
            int valor2 = 0;
            string valor= null;           
            valor = cmb_pregunta.SelectedValue.ToString();       

            if (valor == "¿Como se llama tu perro?")
            {
                txt_id_pregunta.Text = Convert.ToString(1);
            }
            if (valor == "¿Como se llama tu padre?")
            {
                txt_id_pregunta.Text = Convert.ToString(2);
            }
            if (valor == "¿Cual fue tu primer carro?")
            {
                txt_id_pregunta.Text = Convert.ToString(3);
            }
            if (valor == "¿En que pais naciste?")
            {
                txt_id_pregunta.Text = Convert.ToString(4);
            }
            if (contador==1)
            {

                SqlCommand comando = new SqlCommand("SELECT id_pregunta, respuesta_usu FROM Usuario WHERE nombre_usuario = @nombre_usuario", cn);
                comando.Parameters.AddWithValue("@nombre_usuario", txt_nom_usuario.Text);
                cn.Open();
                SqlDataReader registro = comando.ExecuteReader();
                if (registro.Read())
                {
                    valor2 = int.Parse(registro["id_pregunta"].ToString());
                    valor1 = registro["respuesta_usu"].ToString();
                }
                else
                {
                    MessageBox.Show("Pregunta o Respuesta Incorrectas...");
                    txt_respuesta.Clear();
                    
                }
                cn.Close();
                if (valor2 == int.Parse(txt_id_pregunta.Text) && valor1 == txt_respuesta.Text)
                {
                    txt_clave.Visibility = Visibility.Visible;
                    txt_clave_repetir.Visibility = Visibility.Visible;
                    btn_guardar.Visibility = Visibility.Visible;

                }
            }
           
          
        }
     
        private void btn_regresar_Click(object sender, RoutedEventArgs e)
        {        
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();         
        }

        private void btn_recuperar_Click(object sender, RoutedEventArgs e)
        {
            EncontrarUsuario();        
        }

        private void btn_guardar_Click(object sender, RoutedEventArgs e)
        {

            UsuariosModel usuario = new UsuariosModel();
            usuario.contrasena = txt_clave.Password;
            usuario.nombre_usuario = txt_nom_usuario.Text;
            usuarioid = DatosUsuarios.ModificarContrasena(usuario);
            if (txt_clave.Password != txt_clave_repetir.Password)
            {
                MessageBox.Show("Las contraseñas no coinciden..");
            }
            if (usuarioid>0)
            {
                MessageBox.Show("La contraseña se a cambiado exitosamente.");
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }

        }

        private void btn_aceptar_Click(object sender, RoutedEventArgs e)
        {
            EncontrarPregunta();
        }
    }
}
