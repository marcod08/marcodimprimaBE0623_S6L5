using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Prenotazione
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cognome del Cliente")]
        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        public int IdCliente { get; set; }

        [Display(Name = "Numero camera")]
        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        public int IdCamera { get; set; }

        [Display(Name = "Data Prenotazione")]
        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        public DateTime DataPrenotazione { get; set; }

        [Display(Name = "Anno")]
        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [StringLength(4, ErrorMessage = "Massimo 4 caratteri.")]
        public string Anno { get; set; }

        [Display(Name = "Data Inizio Soggiorno")]
        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        public DateTime SoggiornoInizio { get; set; }

        [Display(Name = "Data Fine Soggiorno")]
        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        public DateTime SoggiornoFine { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        public decimal Caparra { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        public decimal Tariffa { get; set; }

        [Display(Name = "Tipologia del Soggiorno")]
        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Massimo 50 caratteri.")]
        public string TipoSoggiorno { get; set; }

        public string Nome { get; set; }
        public string Cognome { get; set; }
    }
}