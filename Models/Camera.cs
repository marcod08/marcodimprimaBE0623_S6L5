using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Camera
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Descrizione della camera")]
        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [StringLength(50, ErrorMessage = "La lunghezza massima è 50 caratteri.")]
        public string Descr { get; set; }

        [Display(Name = "Tipologia di camera")]
        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [StringLength(50, ErrorMessage = "La lunghezza massima è 50 caratteri.")]
        public string Tipologia { get; set; }
    }
}