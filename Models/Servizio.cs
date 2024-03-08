using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Servizio
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Id Prenotazione")]
        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        public int IdPrenotazione {  get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Massimo 50 caratteri.")]
        public string Descrizione { get; set; }

        [Display(Name = "Data di richiesta del servizio")]
        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        public int Quantità { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        public decimal Prezzo { get; set; }
    }
}