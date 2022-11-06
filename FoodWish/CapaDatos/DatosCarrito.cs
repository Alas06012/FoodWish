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
    class DatosCarrito
    {

        public static List<EntidadesCarrito> MostrarCarrito(EntidadesCarrito carr)
        {
            List<EntidadesCarrito> lstcarrito = new List<EntidadesCarrito>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.ConexionBD))
                {
                    //Aperturando base de datos
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = $"select * from V_carrito where fact_detfact = {carr.Fact_DetFact}";//Nombre del procedimiento
                        command.ExecuteNonQuery();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorriendo el DataReader
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    EntidadesCarrito usr = new EntidadesCarrito();
                                    usr.Id_Carrito = int.Parse(dr["id_carrito"].ToString());
                                    usr.Nom_Comida = dr["nom_comida"].ToString();
                                    usr.Comida_Car = int.Parse(dr["comida_car"].ToString());
                                    usr.Usuario_Car = int.Parse(dr["usuario_car"].ToString());
                                    usr.Cantidad_Car = int.Parse(dr["cantidad_car"].ToString());
                                    usr.Precio_Car = decimal.Parse(dr["precio_car"].ToString());
                                    usr.Fact_DetFact = int.Parse(dr["fact_detfact"].ToString());


                                    //AGREGAR REGISTRO A LA LISTA
                                    lstcarrito.Add(usr);
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

            return lstcarrito;
        }


        public static int IngresarCarrito(EntidadesCarrito carr)
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
                        command.CommandText = $"insert into Carrito values({carr.Comida_Car}, {carr.Usuario_Car}, {carr.Cantidad_Car}, {carr.Precio_Car}, {carr.Fact_DetFact})";

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
        }



        public static int EliminarCarrito(EntidadesCarrito carr)
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
                        command.CommandText = $"delete from Carrito where id_carrito = {carr.Id_Carrito}";

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
        }

        public static int EditarCarrito(EntidadesCarrito carr)
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
                        command.CommandText = $"update Carrito set cantidad_car = {carr.Cantidad_Car}, precio_car = {carr.Precio_Car} where id_carrito = {carr.Id_Carrito}";

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
        }

    }
}
