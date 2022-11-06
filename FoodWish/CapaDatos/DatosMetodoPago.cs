using FoodWish.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FoodWish.CapaDatos
{
    class DatosMetodoPago
    {
        public static List<EntidadesMetodoPago> MostrarMetodos()
        {
            List<EntidadesMetodoPago> lstmetodo = new List<EntidadesMetodoPago>();
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
                        command.Parameters.AddWithValue("@tabla", "Metodo_pago");
                        command.ExecuteNonQuery();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    EntidadesMetodoPago usr = new EntidadesMetodoPago();
                                    usr.IdMetodopago = int.Parse(dr["id_metodo"].ToString());
                                    usr.NombreMetodo = dr["nom_metodo"].ToString();

                                    //AGREGAR REGISTRO A LA LISTA
                                    lstmetodo.Add(usr);
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

            return lstmetodo;
        }



        public static int EliminarMetodo(EntidadesMetodoPago usuario)
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
                        command.CommandText = $"delete from Metodo_pago where id_metodo = '{+usuario.IdMetodopago}'";

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



        public static int InsertarMetodo(EntidadesMetodoPago usuario)
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
                        command.CommandText = $"insert into Metodo_pago values ('{usuario.NombreMetodo}')";

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

        public static int ModificarMetodo(EntidadesMetodoPago usuario)
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
                        command.CommandText = $"Update Metodo_pago set nom_metodo = '{usuario.NombreMetodo}' where id_metodo = {usuario.IdMetodopago}";

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


        public static List<EntidadesMetodoPago> BuscarMetodo(string metodoid)
        {
            List<EntidadesMetodoPago> lstmetodo = new List<EntidadesMetodoPago>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.ConexionBD))
                {
                    //Aperturando base de datos
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = $"select * from Metodo_pago where nom_metodo like '{metodoid}%'";
                        command.ExecuteNonQuery();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    EntidadesMetodoPago usr = new EntidadesMetodoPago();
                                    usr.IdMetodopago = int.Parse(dr["id_metodo"].ToString());
                                    usr.NombreMetodo = dr["nom_metodo"].ToString();

                                    //AGREGAR REGISTRO A LA LISTA
                                    lstmetodo.Add(usr);
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

            return lstmetodo;
        }







    }
}
