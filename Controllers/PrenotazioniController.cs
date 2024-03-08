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
    public class PrenotazioniController : Controller
    {
        // GET: Prenotazioni
        [Authorize]
        public ActionResult Index()
        {
            string connString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
            var conn = new SqlConnection(connString);
            conn.Open();
            var command = new SqlCommand(@"
        SELECT Prenotazione.*, Cliente.Nome, Cliente.Cognome
        FROM Prenotazione
        INNER JOIN Cliente ON Prenotazione.IdCliente = Cliente.Id
    ", conn);
            var reader = command.ExecuteReader();

            List<Prenotazione> prenotazioni = new List<Prenotazione>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var prenotazione = new Prenotazione();
                    prenotazione.Id = (int)reader["Id"];
                    prenotazione.Nome = (string)reader["Nome"]; 
                    prenotazione.Cognome = (string)reader["Cognome"]; 
                    prenotazione.IdCamera = (int)reader["IdCamera"];
                    prenotazione.DataPrenotazione = (DateTime)reader["DataPrenotazione"];
                    prenotazione.Anno = (string)reader["Anno"];
                    prenotazione.SoggiornoInizio = (DateTime)reader["SoggiornoInizio"];
                    prenotazione.SoggiornoFine = (DateTime)reader["SoggiornoFine"];
                    prenotazione.Caparra = (decimal)reader["Caparra"];
                    prenotazione.Tariffa = (decimal)reader["Tariffa"];
                    prenotazione.TipoSoggiorno = (string)reader["TipoSoggiorno"];
                    prenotazioni.Add(prenotazione);
                }
            }
            return View(prenotazioni);
        }

        public ActionResult Add()
        {
            var clienti = new List<Cliente>();
            var camere = new List<int>();

            string connString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                var commandCliente = new SqlCommand("SELECT Id, Cognome FROM Cliente", conn);
                using (var reader = commandCliente.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cliente = new Cliente
                        {
                            Id = (int)reader["Id"],
                            Cognome = (string)reader["Cognome"]
                        };
                        clienti.Add(cliente);
                    }
                }

                var commandCamera = new SqlCommand("SELECT Id FROM Camera", conn);
                using (var reader = commandCamera.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var idCamera = (int)reader["Id"];
                        camere.Add(idCamera);
                    }
                }
            }

            ViewBag.ClientiCognome = new SelectList(clienti, "Id", "Cognome");
            ViewBag.CamereList = new SelectList(camere);

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "IdCliente, IdCamera, DataPrenotazione, Anno, SoggiornoInizio, SoggiornoFine, Caparra, Tariffa, TipoSoggiorno")] Prenotazione pren)
        {
            if (ModelState.IsValid)
            {
                string connString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
                var conn = new SqlConnection(connString);
                conn.Open();
                var command = new SqlCommand(@"
                    INSERT INTO Prenotazione
                    (IdCliente, IdCamera, DataPrenotazione, Anno, SoggiornoInizio, SoggiornoFine, Caparra, Tariffa, TipoSoggiorno)
                    OUTPUT INSERTED.Id
                    VALUES (@IdCliente, @IdCamera, @DataPrenotazione, @Anno, @SoggiornoInizio, @SoggiornoFine, @Caparra, @Tariffa, @TipoSoggiorno)
                ", conn);

                command.Parameters.AddWithValue("@IdCliente", pren.IdCliente);
                command.Parameters.AddWithValue("@IdCamera", pren.IdCamera);
                command.Parameters.AddWithValue("@DataPrenotazione", pren.DataPrenotazione);
                command.Parameters.AddWithValue("@Anno", pren.Anno);
                command.Parameters.AddWithValue("@SoggiornoInizio", pren.SoggiornoInizio);
                command.Parameters.AddWithValue("@SoggiornoFine", pren.SoggiornoFine);
                command.Parameters.AddWithValue("@Caparra", pren.Caparra);
                command.Parameters.AddWithValue("@Tariffa", pren.Tariffa);
                command.Parameters.AddWithValue("@TipoSoggiorno", pren.TipoSoggiorno);

                var prenId = command.ExecuteScalar();

                return RedirectToAction("Index", "Servizi", new { id = prenId });
            }

            return View(pren);
        }

    }
}