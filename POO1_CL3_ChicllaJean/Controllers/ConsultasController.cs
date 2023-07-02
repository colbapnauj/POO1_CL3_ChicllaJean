using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using POO1_CL3_ChicllaJean.Models;

namespace POO1_CL3_ChicllaJean.Controllers
{
    public class ConsultasController : Controller
    {
        string myConnectionString = "server=127.0.0.1;uid=root;database=negocios";

        IEnumerable<Cliente> Clientes()
        {
                MySqlConnection conn = null;

                List<Cliente> temporal = new List<Cliente>();
            
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();


                MySqlCommand cmd = new MySqlCommand();
                // cmd.CommandText = "Select * from cliente";
            
                cmd.CommandText = "usp_clientes";
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                temporal.Add(new Cliente()
                {
                    IdCLiente = reader.GetInt32(0).ToString(),
                    Nombres = reader.GetString(1),
                    TipoDocumento = reader.GetInt32(2).ToString(),

                    Documento = reader.GetString(3).ToString(),
                    Telefono = reader.GetString(4).ToString(),
                });
                }

                reader.Close();
                conn.Close();

            
                return temporal;



        }

        public ActionResult Listado()
        {
            return View(Clientes());
        }

        public ActionResult Registro()
        {
            return View(new Cliente());
        }

        string Agregar(Cliente cliente)
        {
            string message = "";

            MySqlConnection conn = null;
            

            try
            {
                conn = new MySqlConnection();

                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = "usp_registrar_cliente";
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@p_tipoDocumento", cliente.TipoDocumento);
                cmd.Parameters.AddWithValue("@p_documento", cliente.Documento);
                cmd.Parameters.AddWithValue("@p_telefono", cliente.Telefono);

                int value = cmd.ExecuteNonQuery();

                message = $"Registro exitoso. Se a insertado {value} cliente";

            } catch (MySqlException e)
            {
                message = e.Message;
            } finally
            {
                if (conn != null) { 
                    conn.Close();
                }
            }

            return message;
        }

        [HttpPost]
        public ActionResult Registro(Cliente reg)
        {
            string message = Agregar(reg);
            ViewBag.messge = message;
            return RedirectToAction("Listado");
        }

        string Editar(Cliente cliente)
        {
            string message = "";

            MySqlConnection conn = null;


            try
            {
                conn = new MySqlConnection();

                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = "usp_editar_cliente";
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_idCliente", cliente.IdCLiente);
                cmd.Parameters.AddWithValue("@p_nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@p_tipoDocumento", cliente.TipoDocumento);
                cmd.Parameters.AddWithValue("@p_documento", cliente.Documento);
                cmd.Parameters.AddWithValue("@p_telefono", cliente.Telefono);

                int value = cmd.ExecuteNonQuery();

                message = $"Registro exitoso. Se ha insertado {value} cliente";

            }
            catch (MySqlException e)
            {
                message = e.Message;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return message;
        }

        string Eliminar(int id)
        {
            string message = "";

            MySqlConnection conn = null;


            try
            {
                conn = new MySqlConnection();

                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = "usp_eliminar_cliente";
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_idCliente", id);

                int value = cmd.ExecuteNonQuery();

                message = $"Registro eliminado. Se ha afectado{value} cliente";

            }
            catch (MySqlException e)
            {
                message = e.Message;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return message;
        }

        public ActionResult Edit(int id)
        {
            IEnumerable<Cliente> clientes = Clientes();
            Cliente cliente = clientes.ToList().Find(c => c.IdCLiente == id.ToString());
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Edit(Cliente reg)
        {
            string message = Editar(reg);
            ViewBag.messge = message;
            return RedirectToAction("Listado");
        }

        public ActionResult Delete(int id)
        {
            string message = Eliminar(id);
            ViewBag.messge = message;
            return RedirectToAction("Listado");
        }

        IEnumerable<Cliente> BusquedaPorNombre(string nombre)
        {

            MySqlConnection conn = null;
            List<Cliente> temporal = new List<Cliente>();

            try
            {
                conn = new MySqlConnection();

                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = "usp_buscar_cliente_por_nombre";
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_nombre", nombre);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    temporal.Add(new Cliente()
                    {
                        IdCLiente = reader.GetInt32(0).ToString(),
                        Nombres = reader.GetString(1),
                        TipoDocumento = reader.GetInt32(2).ToString(),
                        Documento = reader.GetString(3).ToString(),
                        Telefono = reader.GetString(4).ToString(),
                    });
                }

            } 
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            
            return temporal;
        }


        

        [HttpPost]
        public ActionResult SearchByName(string nombre)
        {
            IEnumerable<Cliente> clientes = BusquedaPorNombre(nombre);
            return View("Listado", clientes);
        }

    }


}