using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FoodWish.CapaEntidades;


namespace FoodWish.CapaDatos
{
    class DatosUsuarios
    {
        public static List<EntidadesUsuarios> MostrarUsuarios()
        {
            List<EntidadesUsuarios> lstUsuarios = new List<EntidadesUsuarios>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.ConexionBD))
                {
                    //Aperturando base de datos
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_mostrarTablas";//Nombre del procedimiento
                        command.Parameters.AddWithValue("@tabla", "Usuario");
                        command.ExecuteNonQuery();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    EntidadesUsuarios usr = new EntidadesUsuarios();
                                    usr.IdUsuario = int.Parse(dr["id_usuario"].ToString());
                                    usr.NombreUsuario = dr["nombre_usuario"].ToString();
                                    usr.TipoUsuario = int.Parse( dr["tipo_usu"].ToString());
                                    usr.IdPregunta= int.Parse(dr["id_pregunta"].ToString());
                                    usr.Respuesta = dr["respuesta_usu"].ToString();
                                    //usr.Contrasena= dr["contrasena"].ToString();

                                    //AGREGAR REGISTRO A LA LISTA
                                    lstUsuarios.Add(usr);
                                }//Fin de While
                            }//Fin de IF hasRows
                        }//FIn de using reader
                    }//Fin de using de sentencias sql
                }//Fin de using de conexion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error: " + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return lstUsuarios;
        }



        public static int EliminarUsuario(EntidadesUsuarios usuario)
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
                        command.CommandText = "sp_eliminarUsuarios";
                        command.Parameters.AddWithValue("@ID", usuario.IdUsuario);

                        //Ejecutar StoreProcedure
                        res = command.ExecuteNonQuery();
                    }//Fin de usign sql
                }//Fin de using de conexion
            }//Fin try
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error: " + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return res;
        }//Fin de Eliminarusuarios


        public static int ModificarUsuario(EntidadesUsuarios usuario)
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
                        command.CommandText = "sp_modificarUsuario";
                        command.Parameters.AddWithValue("@ID", usuario.IdUsuario);
                        command.Parameters.AddWithValue("@NOMBRE", usuario.NombreUsuario);
     
                        //Ejecutar StoreProcedure
                        res = command.ExecuteNonQuery();


                    }//Fin de usign sql
                }//Fin de using de conexion
            }//Fin try
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error: " + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return res;
        }//Fin de Modificarusuarios


        public static List<EntidadesUsuarios> BuscarUsuario(string usuario)
        {
            List<EntidadesUsuarios> lstUsuarios = new List<EntidadesUsuarios>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.ConexionBD))
                {
                    //Aperturando base de datos
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_buscarUsuario";//Nombre del procedimiento
                        command.Parameters.AddWithValue("@NOMBRE", usuario);
                        command.ExecuteNonQuery();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    EntidadesUsuarios usr = new EntidadesUsuarios();
                                    usr.IdUsuario = int.Parse(dr["id_usuario"].ToString());
                                    usr.NombreUsuario = dr["nombre_usuario"].ToString();
                                    usr.TipoUsuario = int.Parse(dr["tipo_usu"].ToString());
                                    usr.IdPregunta = int.Parse(dr["id_pregunta"].ToString());
                                    usr.Respuesta = dr["respuesta_usu"].ToString();
                                   // usr.Contrasena = dr["contrasena"].ToString();

                                    //AGREGAR REGISTRO A LA LISTA
                                    lstUsuarios.Add(usr);
                                }//Fin de While
                            }//Fin de IF hasRows
                        }//FIn de using reader
                    }//Fin de using de sentencias sql
                }//Fin de using de conexion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error: " + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return lstUsuarios;
        }




        public static int BuscarIgual(string nombre)
        {
            int res = 0;
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.ConexionBD))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = $"select nombre_usuario from Usuario where nombre_usuario = '{nombre}'";
                        res = command.ExecuteNonQuery();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                res = 1;
                            }
                            else
                            {
                                res = 0;
                            }
                        }//FIn de using reader





                    }//Fin de usign sql
                }//Fin de using de conexion
            }//Fin try
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error: " + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return res;
        }//Fin de Buscar Igual




    }
}
