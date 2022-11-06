using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using FoodWish.Models;
using System.Windows;


namespace FoodWish.Services
{
    public class DatosUsuarios
    {
        public DatosUsuarios() { }

        public static int InsertarUsuarios(UsuariosModel pusuarios)
        {
            int res = 0;

            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.ConexionBD))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SPINSERTARUSUARIOS";

                        command.Parameters.AddWithValue("@NOMBRE",pusuarios.nombre_usuario);
                        command.Parameters.AddWithValue("@ID_PREGUNTA", pusuarios.id_pregunta);
                        command.Parameters.AddWithValue("@RESPUESTA_USU", pusuarios.respuesta_usu);
                        command.Parameters.AddWithValue("@CONTRASENA", pusuarios.contrasena);
                        command.Parameters.AddWithValue("@TIPO_USU", pusuarios.tipo_usu);

                        res = command.ExecuteNonQuery();

                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocurrio un Error: " + ex.Message, "Error",
                   MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return res;
        }
        public static int ModificarContrasena(UsuariosModel pusuarios)
        {
            int res = 0;

            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.ConexionBD))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SPMODIFICARCONTRASENA";
                        command.Parameters.AddWithValue("@NOMBRE", pusuarios.nombre_usuario);
                        command.Parameters.AddWithValue("@CONTRA", pusuarios.contrasena);
                        res = command.ExecuteNonQuery();

                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocurrio un Error: " + ex.Message, "Error",
                   MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return res;
        }


    }
}
