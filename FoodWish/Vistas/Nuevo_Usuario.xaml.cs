using FoodWish.Models;
using FoodWish.Services;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Windows.Threading;


namespace FoodWish.Vistas
{
    /// <summary>
    /// Lógica de interacción para Nuevo_Usuario.xaml
    /// </summary>
    public partial class Nuevo_Usuario : Window
    {
        public Nuevo_Usuario()
        {
            InitializeComponent();
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.ConexionBD);
            SqlCommand cmd = new SqlCommand("select*from Preguntas", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmb_pregunta.Items.Add(dr.GetString(1));
            }
            cn.Close();


        }

        bool ValidarFormulario()
        {
            bool estado = true;//Asumir que todo esta OK
            string mensaje = null;

            //Validando objetos
            if (string.IsNullOrEmpty(txt_nom_usuario.Text))
            {
                estado = false;
                mensaje += "- Nombre de usuario\n";
            }

            if (string.IsNullOrEmpty(txt_Clave.Password))
            {
                estado = false;
                mensaje += "- Contraseña\n";
            }

            if (string.IsNullOrEmpty(txt_clave_repetir.Password))
            {
                estado = false;
                mensaje += "- Repetir Contraseña\n";
            }
            if (string.IsNullOrEmpty(cmb_pregunta.Text))
            {
                estado = false;
                mensaje += "- Pregunta\n";
            }
            if (string.IsNullOrEmpty(txt_respuesta.Text))
            {
                estado = false;
                mensaje += "- Respuesta\n";
            }

            if (estado)
            {
                //Indica que todo esta completo en el formulario
                if (txt_Clave.Password != txt_clave_repetir.Password)
                {
                    estado = false;
                    mensaje += "Las clave no son iguales";
                }
            }

            if (!estado)
            {
                MessageBox.Show("Favor de completar o corregir los siguientes campos:\n\n" + mensaje,
                  "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                //bool? Result = new MessageBoxCustom("Favor de completar o corregir los siguientes campos:\n", MessageType.Warning, MessageButtons.Ok).ShowDialog();

            }

            return estado;
        }//Fin de ValidarFormulario       
        int usuarioid = 0;



        private void btn_regresar_Click(object sender, RoutedEventArgs e)
        {
          
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void btn_registrar_Click(object sender, RoutedEventArgs e)
        {
           
           
            if (cmb_pregunta.SelectedValue == null)
            {
                bool? Result = new MessageBoxCustom("Favor de llenar todos los campos.", MessageType.Warning, MessageButtons.Ok).ShowDialog();

            }
            else
            {
                string valor;
                int tipo = 2;
                valor = cmb_pregunta.SelectedValue.ToString();
                txt_tipo_usu.Text = tipo.ToString();



                if (ValidarFormulario())
                {
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


                    UsuariosModel usuario = new UsuariosModel();
                    usuario.nombre_usuario = txt_nom_usuario.Text;
                    usuario.tipo_usu = Convert.ToInt32(txt_tipo_usu.Text);
                    usuario.id_pregunta = Convert.ToInt32(txt_id_pregunta.Text);
                    usuario.respuesta_usu = txt_respuesta.Text;
                    usuario.contrasena = txt_Clave.Password;

                    usuarioid = DatosUsuarios.InsertarUsuarios(usuario);

                    if (usuarioid > 0)
                    {
                        //MessageBox.Show("Usuario registrado Exitosamente!! ");
                        bool? Result = new MessageBoxCustom("Usuario registrado Exitosamente!", MessageType.Success, MessageButtons.Ok).ShowDialog();

                        MainWindow main = new MainWindow();
                        main.Show();
                        this.Close();

                    }

                }



            }

        }
    }
}
