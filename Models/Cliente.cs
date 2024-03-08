using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Massimo 50 caratteri.")]
        public string Cognome {  get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Massimo 50 caratteri.")]
        public string Nome { get; set; }

        [Display(Name = "Codice Fiscale")]
        [StringLength(16, ErrorMessage = "Il Codice Fiscale deve avere 16 caratteri")]
        public string CodFisc { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Massimo 50 caratteri.")]
        public string Città { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Massimo 50 caratteri.")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Massimo 50 caratteri.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [StringLength(20, ErrorMessage = "Massimo 20 caratteri.")]
        public string Telefono { get; set; }

    }
}