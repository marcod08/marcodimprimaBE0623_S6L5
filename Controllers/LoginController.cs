using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Hotel.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated) return RedirectToAction("UserPanel");
            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin admin)
        {
            string connectString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
            var conn = new SqlConnection(connectString);
            conn.Open();
            var command = new SqlCommand("Select * From Admin Where Username = @username AND Password = @password", conn);
            command.Parameters.AddWithValue("@username", admin.Username);
            command.Parameters.AddWithValue("@password", admin.Password);
            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                FormsAuthentication.SetAuthCookie(reader["Id"].ToString(), true);
                return RedirectToAction("UserPanel", "Login");
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult UserPanel()
        {
            var Id = HttpContext.User.Identity.Name;
            ViewBag.Id = Id;
            return View();
        }

        [Authorize, HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}