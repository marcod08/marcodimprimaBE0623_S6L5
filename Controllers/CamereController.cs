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
    public class CamereController : Controller
    {
        // GET: Camera
        [AllowAnonymous]
        public ActionResult Index()
        {
            string connString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
            var conn = new SqlConnection(connString);
            conn.Open();
            var command = new SqlCommand(@"
                SELECT *
                FROM Camera
            "
            , conn);
            var reader = command.ExecuteReader();

            List<Camera> camere = new List<Camera>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var camera = new Camera();
                    camera.Id = (int)reader["Id"];
                    camera.Descr = (string)reader["Descr"];
                    camera.Tipologia = (string)reader["Tipologia"];
                    camere.Add(camera);
                }
            }
            return View(camere);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Descr, Tipologia")] Camera camere)
        {
            if (ModelState.IsValid)
            {
                string connString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
                var conn = new SqlConnection(connString);
                conn.Open();
                var command = new SqlCommand(@"
                    INSERT INTO Camera
                    (Descr, Tipologia)
                    OUTPUT INSERTED.Id
                    VALUES (@Descr, @Tipologia)
                ", conn);

                command.Parameters.AddWithValue("@Descr", camere.Descr);
                command.Parameters.AddWithValue("@Tipologia", camere.Tipologia);

                var camereId = command.ExecuteScalar();

                return RedirectToAction("Index", "Camere", new { id = camereId });
            }

            return View(camere);
        }
    }
}