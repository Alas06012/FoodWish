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
    class DatosMesa
    {

        public static List<EntidadesMesa> MostrarMesas()
        {
            List<EntidadesMesa> lstmesa = new List<EntidadesMesa>();
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
                        command.Parameters.AddWithValue("@tabla", "Mesa");
                        command.ExecuteNonQuery();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    int aux;
                                    EntidadesMesa usr = new EntidadesMesa();
                                    usr.IdMesa = int.Parse(dr["id_mesa"].ToString());
                                    usr.Numero_mesa = int.Parse(dr["numero_mesa"].ToString());
                                    usr.IdEstado_mesa = int.Parse(dr["idestado_mesa"].ToString());

                                    if (String.IsNullOrEmpty(dr["id_usuario"].ToString()))
                                    {
                                        usr.IdUsuario = 0;
                                    }
                                    else
                                    {
                                        usr.IdUsuario = int.Parse(dr["id_usuario"].ToString());
                                    }       

                                    //AGREGAR REGISTRO A LA LISTA
                                    lstmesa.Add(usr);
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

            return lstmesa;
        }

        public static List<EntidadesMesa> MostrarElecMesas()
        {
            List<EntidadesMesa> lstmesa = new List<EntidadesMesa>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.ConexionBD))
                {
                    //Aperturando base de datos
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = "select * from V_Mesa";//Nombre del procedimiento
                        command.ExecuteNonQuery();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    int aux;
                                    EntidadesMesa usr = new EntidadesMesa();
                                    usr.IdMesa = int.Parse(dr["id_mesa"].ToString());
                                    usr.Numero_mesa = int.Parse(dr["numero_mesa"].ToString());
                                    usr.IdEstado_mesa = dr["nom_estadomesa"].ToString();
                                    usr.IdUsuario = dr["nombre_usuario"].ToString();

                                    //AGREGAR REGISTRO A LA LISTA
                                    lstmesa.Add(usr);
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

            return lstmesa;
        }


        public static int EliminarMesa(EntidadesMesa usuario)
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
                        command.CommandText = $"delete from Mesa where id_mesa = {usuario.IdMesa}";
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


        public static int InsertarMesa(EntidadesMesa usuario)
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
                        command.CommandText = $"insert into Mesa values ('{usuario.Numero_mesa}','{usuario.IdEstado_mesa}',null)";

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
        }//Fin de EliminarMEsas

        public static int ModificarMesa(EntidadesMesa usuario)
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
                        command.CommandText = $"Update Mesa set numero_mesa = '{usuario.Numero_mesa}', idestado_mesa='{usuario.IdEstado_mesa}' where id_mesa = {usuario.IdMesa}";

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
        }//Fin de ModificarMesa

        public static int LiberarMesa(EntidadesMesa mesa)
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
                        command.CommandText = $"Update Mesa set idestado_mesa= 1, id_usuario = NULL where id_mesa = {mesa.IdMesa}";

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
        }//Fin de ModificarMesa

        public static int ReservarMesa(EntidadesMesa usuario)
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
                        command.CommandText = $"Update Mesa set idestado_mesa=2, id_usuario={usuario.IdUsuario} where id_mesa = {usuario.IdMesa}";

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
        }//Fin de ModificarMesa

        public static List<EntidadesMesa> BuscarMesa(string mesa)
        {
            List<EntidadesMesa> lstMesa = new List<EntidadesMesa>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.ConexionBD))
                {
                    //Aperturando base de datos
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = $"select * from Mesa where numero_mesa like '{mesa}%'";
                        command.ExecuteNonQuery();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    EntidadesMesa usr = new EntidadesMesa();
                                    usr.IdMesa = int.Parse(dr["id_mesa"].ToString());
                                    usr.Numero_mesa = int.Parse(dr["numero_mesa"].ToString());
                                    usr.IdEstado_mesa = int.Parse(dr["idestado_mesa"].ToString());
                                    usr.IdUsuario = int.Parse(dr["id_usuario"].ToString());

                                    //AGREGAR REGISTRO A LA LISTA
                                    lstMesa.Add(usr);
                                }//Fin de While
                            }//Fin de IF hasRows
                        }//FIn de using reader
                    }//Fin de using de sentencias sql
                }//Fin de using de conexion
            }
            catch (Exception ex)
            {
           //nada
            }

            return lstMesa;
        }


        public static List<EntidadesEstadoMesa> llenarCombobox()
        {
            List<EntidadesEstadoMesa> lstestados = new List<EntidadesEstadoMesa>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.ConexionBD))
                {
                    //Aperturando base de datos
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = $"select * from Estado_mesa";//Nombre del procedimiento
                        command.ExecuteNonQuery();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    EntidadesEstadoMesa usr = new EntidadesEstadoMesa();
                                    usr.IdEstadoMesa = int.Parse(dr["id_estadomesa"].ToString());
                                    usr.NombreEstado = dr["nom_estadomesa"].ToString();


                                    //AGREGAR REGISTRO A LA LISTA
                                    lstestados.Add(usr);
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

            return lstestados;
        }


        public static int BuscarIgual(int numero)
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
                        command.CommandText = $"select numero_mesa from Mesa where numero_mesa = {numero}";
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
