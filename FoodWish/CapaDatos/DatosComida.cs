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
    class DatosComida
    {
        public static List<EntidadesComida> MostrarComida()
        {
            List<EntidadesComida> lstcomida = new List<EntidadesComida>();
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
                        command.Parameters.AddWithValue("@tabla", "Comida");
                        command.ExecuteNonQuery();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    EntidadesComida usr = new EntidadesComida();
                                    usr.IdComida = int.Parse(dr["id_comida"].ToString());
                                    usr.Nombre = dr["nom_comida"].ToString();
                                    usr.Descripcion = dr["decrip_comida"].ToString();
                                    usr.Tipo = int.Parse(dr["tipo_comida"].ToString());
                                    usr.Precio = decimal.Parse(dr["precio_comid"].ToString());
                                   

                                    //AGREGAR REGISTRO A LA LISTA
                                    lstcomida.Add(usr);
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

            return lstcomida;
        }

        public static List<EntidadesComida> MostrarPlatos()
        {
            List<EntidadesComida> lstcomida = new List<EntidadesComida>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.ConexionBD))
                {
                    //Aperturando base de datos
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = "select * from Platos";//Nombre del procedimiento
                        command.ExecuteNonQuery();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    EntidadesComida usr = new EntidadesComida();
                                    usr.IdComida = int.Parse(dr["id_comida"].ToString());
                                    usr.Nombre = dr["nom_comida"].ToString();
                                    usr.Descripcion = dr["decrip_comida"].ToString();
                                    usr.Precio = decimal.Parse(dr["precio_comid"].ToString());


                                    //AGREGAR REGISTRO A LA LISTA
                                    lstcomida.Add(usr);
                                }//Fin de While
                            }//Fin de IF hasRows
                        }//FIn de using reader
                    }//Fin de using de sentencias sql
                }//Fin de using de conexion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error plato: " + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return lstcomida;
        }

        public static List<EntidadesComida> MostrarBebidas()
        {
            List<EntidadesComida> lstcomida = new List<EntidadesComida>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.ConexionBD))
                {
                    //Aperturando base de datos
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = "select * from Bebidas";//Nombre del procedimiento
                        command.ExecuteNonQuery();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    EntidadesComida usr = new EntidadesComida();
                                    usr.IdComida = int.Parse(dr["id_comida"].ToString());
                                    usr.Nombre = dr["nom_comida"].ToString();
                                    usr.Descripcion = dr["decrip_comida"].ToString();
                                    usr.Precio = decimal.Parse(dr["precio_comid"].ToString());


                                    //AGREGAR REGISTRO A LA LISTA
                                    lstcomida.Add(usr);
                                }//Fin de While
                            }//Fin de IF hasRows
                        }//FIn de using reader
                    }//Fin de using de sentencias sql
                }//Fin de using de conexion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error bebida: " + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return lstcomida;
        }

        public static List<EntidadesComida> MostrarComplementos()
        {
            List<EntidadesComida> lstcomida = new List<EntidadesComida>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.ConexionBD))
                {
                    //Aperturando base de datos
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = "select * from Complementos";//Nombre del procedimiento
                        command.ExecuteNonQuery();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    EntidadesComida usr = new EntidadesComida();
                                    usr.IdComida = int.Parse(dr["id_comida"].ToString());
                                    usr.Nombre = dr["nom_comida"].ToString();
                                    usr.Descripcion = dr["decrip_comida"].ToString();
                                    usr.Precio = decimal.Parse(dr["precio_comid"].ToString());


                                    //AGREGAR REGISTRO A LA LISTA
                                    lstcomida.Add(usr);
                                }//Fin de While
                            }//Fin de IF hasRows
                        }//FIn de using reader
                    }//Fin de using de sentencias sql
                }//Fin de using de conexion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error complemento: " + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return lstcomida;
        }

        public static List<EntidadesComida> MostrarPostres()
        {
            List<EntidadesComida> lstcomida = new List<EntidadesComida>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.ConexionBD))
                {
                    //Aperturando base de datos
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = "select * from Postres";//Nombre del procedimiento
                        command.ExecuteNonQuery();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    EntidadesComida usr = new EntidadesComida();
                                    usr.IdComida = int.Parse(dr["id_comida"].ToString());
                                    usr.Nombre = dr["nom_comida"].ToString();
                                    usr.Descripcion = dr["decrip_comida"].ToString();
                                    usr.Precio = decimal.Parse(dr["precio_comid"].ToString());


                                    //AGREGAR REGISTRO A LA LISTA
                                    lstcomida.Add(usr);
                                }//Fin de While
                            }//Fin de IF hasRows
                        }//FIn de using reader
                    }//Fin de using de sentencias sql
                }//Fin de using de conexion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error postre: " + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return lstcomida;
        }

        public static int EliminarComida(EntidadesComida usuario)
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
                        command.CommandText = "sp_eliminarComida";
                        command.Parameters.AddWithValue("@ID", usuario.IdComida);

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


        public static int InsertarComida(EntidadesComida usuario)
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
                        command.CommandText = "sp_insertarComida";
                        command.Parameters.AddWithValue("@NOMBRE", usuario.Nombre);
                        command.Parameters.AddWithValue("@DESCRIP", usuario.Descripcion);
                        command.Parameters.AddWithValue("@TIPO", usuario.Tipo);
                        command.Parameters.AddWithValue("@PRECIO", usuario.Precio);


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


        public static int ModificarComida(EntidadesComida usuario)
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
                        command.CommandText = "sp_modificarComida";
                        command.Parameters.AddWithValue("@ID", usuario.IdComida);
                        command.Parameters.AddWithValue("@NOMBRE", usuario.Nombre);
                        command.Parameters.AddWithValue("@DESCRIP", usuario.Descripcion);
                        command.Parameters.AddWithValue("@TIPO", usuario.Tipo);
                        command.Parameters.AddWithValue("@PRECIO", usuario.Precio);

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



        public static List<EntidadesComida> BuscarComida(string comidaid)
        {
            List<EntidadesComida> lstComida = new List<EntidadesComida>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.ConexionBD))
                {
                    //Aperturando base de datos
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_buscarComida";//Nombre del procedimiento
                        command.Parameters.AddWithValue("@NOMBRE", comidaid);
                        command.ExecuteNonQuery();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    EntidadesComida usr = new EntidadesComida();
                                    usr.IdComida = int.Parse(dr["id_comida"].ToString());
                                    usr.Nombre = dr["nom_comida"].ToString();
                                    usr.Descripcion = dr["decrip_comida"].ToString();
                                    usr.Tipo = int.Parse(dr["tipo_comida"].ToString());
                                    usr.Precio = decimal.Parse(dr["precio_comid"].ToString());
                                    // usr.IdReceta = int.Parse(dr["fk_receta"].ToString());

                                    //AGREGAR REGISTRO A LA LISTA
                                    lstComida.Add(usr);
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

            return lstComida;
        }


        public static List<EntidadesTipoComida> llenarCombobox()
        {
            List<EntidadesTipoComida> lstComida = new List<EntidadesTipoComida>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.ConexionBD))
                {
                    //Aperturando base de datos
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_llenarComboComida";//Nombre del procedimiento
                        command.ExecuteNonQuery();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    EntidadesTipoComida usr = new EntidadesTipoComida();
                                    usr.IdTipoComida = int.Parse(dr["id_tipocomida"].ToString());
                                    usr.NombreTipo = dr["nom_tipocomida"].ToString();
                

                                    //AGREGAR REGISTRO A LA LISTA
                                    lstComida.Add(usr);
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

            return lstComida;
        }









    }
}
