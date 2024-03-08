using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Controllers
{
    public class ServiziController : Controller
    {
        // GET: Servizi
        [Authorize]
        public ActionResult Index()
        {
            {
                string connString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
                var conn = new SqlConnection(connString);
                conn.Open();
                var command = new SqlCommand(@"
                SELECT *
                FROM Servizio"
                , conn);
                var reader = command.ExecuteReader();

                List<Servizio> servizi = new List<Servizio>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var servizio = new Servizio();
                        servizio.Id = (int)reader["Id"];
                        servizio.IdPrenotazione = (int)reader["IdPrenotazione"];
                        servizio.Descrizione = (string)reader["Descrizione"];
                        servizio.Data = (DateTime)reader["Data"];
                        servizio.Quantità = (int)reader["Quantità"];
                        servizio.Prezzo = (decimal)reader["Prezzo"];
                        servizi.Add(servizio);
                    }
                }
                return View(servizi);
            }
        }
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "IdPrenotazione, Descrizione, Data, Quantità, Prezzo")] Servizio servizi)
        {
            if (ModelState.IsValid)
            {
                string connString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
                var conn = new SqlConnection(connString);
                conn.Open();
                var command = new SqlCommand(@"
                    INSERT INTO Servizio
                    (IdPrenotazione, Descrizione, Data, Quantità, Prezzo)
                    OUTPUT INSERTED.Id
                    VALUES (@IdPrenotazione, @Descrizione, @Data, @Quantità, @Prezzo)
                ", conn);

                command.Parameters.AddWithValue("@Descrizione", servizi.Descrizione);
                command.Parameters.AddWithValue("@IdPrenotazione", servizi.IdPrenotazione);
                command.Parameters.AddWithValue("@Data", servizi.Data);
                command.Parameters.AddWithValue("@Quantità", servizi.Quantità);
                command.Parameters.AddWithValue("@Prezzo", servizi.Prezzo);

                var serviziId = command.ExecuteScalar();

                return RedirectToAction("Index", "Servizi", new { id = serviziId });
            }

            return View(servizi);
        }
    }
}