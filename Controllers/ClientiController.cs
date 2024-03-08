using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Hotel.Controllers
{
    public class ClientiController : Controller
    {
        // GET: Clienti
        [Authorize]
        public ActionResult Index()
        {
            string connString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
            var conn = new SqlConnection(connString);
            conn.Open();
            var command = new SqlCommand(@"
                SELECT *
                FROM Cliente
            "
            , conn);
            var reader = command.ExecuteReader();

            List<Cliente> clienti = new List<Cliente>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var cliente = new Cliente();
                    cliente.Id = (int)reader["Id"];
                    cliente.Cognome = (string)reader["Cognome"];
                    cliente.Nome = (string)reader["Nome"];
                    cliente.CodFisc = (string)reader["CodFisc"];
                    cliente.Città = (string)reader["Città"];
                    cliente.Provincia = (string)reader["Provincia"];
                    cliente.Email = (string)reader["Email"];
                    cliente.Telefono = (string)reader["Telefono"];
                    clienti.Add(cliente);
                }
            }
            return View(clienti);
        }
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Cognome, Nome, CodFisc, Città, Provincia, Email, Telefono")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                string connString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
                var conn = new SqlConnection(connString);
                conn.Open();
                var command = new SqlCommand(@"
                    INSERT INTO Cliente
                    (Cognome, Nome, CodFisc, Città, Provincia, Email, Telefono)
                    OUTPUT INSERTED.Id
                    VALUES (@Cognome, @Nome, @CodFisc, @Città, @Provincia, @Email, @Telefono)
                ", conn);

                command.Parameters.AddWithValue("@Cognome", cliente.Cognome);
                command.Parameters.AddWithValue("@Nome", cliente.Nome);
                command.Parameters.AddWithValue("@CodFisc", cliente.CodFisc);
                command.Parameters.AddWithValue("@Città", cliente.Città);
                command.Parameters.AddWithValue("@Provincia", cliente.Provincia);
                command.Parameters.AddWithValue("@Email", cliente.Email);
                command.Parameters.AddWithValue("@Telefono", cliente.Telefono);

                var clienteId = command.ExecuteScalar();

                return RedirectToAction("Index", "Clienti", new { id = clienteId });
            }

            return View(cliente);
        }
    }
}