using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using FoodWish.CapaEntidades;
using System.Windows;

namespace FoodWish.CapaDatos
{
    class DatosFactura
    {
        public static int GenerarFactura(EntidadesFactura fact)
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
                        command.CommandText = $"insert into Factura values(1, {fact.UsuarioFact}, 0.00)";

                        //Ejecutar StoreProcedure
                        res = command.ExecuteNonQuery();
                    }//Fin de usign sql
                }//Fin de using de conexion
            }//Fin try
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error factura: " + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return res;
        }

        public static int PagoFactura(EntidadesFactura fact)
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
                        command.CommandText = $"update Factura set metod_fact = {fact.MetodoFact}, total_fact = {fact.TotalFact} where id_factura = {fact.IdFactura}";

                        //Ejecutar StoreProcedure
                        res = command.ExecuteNonQuery();
                    }//Fin de usign sql
                }//Fin de using de conexion
            }//Fin try
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error factura: " + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return res;
        }

        public static int LastFactura()
        {
            int res = 0;
            int factt = 0;
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.ConexionBD))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = "select max(f.id_factura) as id_factura from Factura f";
                        //Ejecutar StoreProcedure
                        res = command.ExecuteNonQuery();
                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    factt = int.Parse(dr["id_factura"].ToString());
                                }//Fin de While
                            }//Fin de IF hasRows
                        }//FIn de using reader
                    }//Fin de usign sql
                }//Fin de using de conexion
            }//Fin try
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error facttttt: " + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return factt;
        }


        public static List<EntidadesFactura> mostrarFactura()
        {
            List<EntidadesFactura> lst = new List<EntidadesFactura>();
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
                        command.Parameters.AddWithValue("@tabla", "Factura");
                        command.ExecuteNonQuery();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    EntidadesFactura usr = new EntidadesFactura();
                                    usr.IdFactura = int.Parse(dr["id_factura"].ToString());
                                    usr.MetodoFact = int.Parse(dr["metod_fact"].ToString());
                                    usr.UsuarioFact = int.Parse(dr["usuario_fact"].ToString());
                                    usr.TotalFact = decimal.Parse(dr["total_fact"].ToString());



                                    //AGREGAR REGISTRO A LA LISTA
                                    lst.Add(usr);
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

            return lst;
        }


        public static List<EntidadesFactura> BuscarFactura(string numero)
        {
            List<EntidadesFactura> lst = new List<EntidadesFactura>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.ConexionBD))
                {
                    //Aperturando base de datos
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = $"select * from Factura where id_factura like '{numero}%'";//Nombre del procedimiento
                        command.ExecuteNonQuery();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    EntidadesFactura usr = new EntidadesFactura();
                                    usr.IdFactura = int.Parse(dr["id_factura"].ToString());
                                    usr.MetodoFact = int.Parse(dr["metod_fact"].ToString());
                                    usr.UsuarioFact = int.Parse(dr["usuario_fact"].ToString());
                                    usr.TotalFact = decimal.Parse(dr["total_fact"].ToString());



                                    //AGREGAR REGISTRO A LA LISTA
                                    lst.Add(usr);
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

            return lst;
        }




    }
}
