using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProyectoFinal.Models
{
    public class Postres
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "El {0} debe ser de {1} a {2}")]
        [Display(Name = "Tipo del platillo")]
        public string TipoPlatillo { get; set; }

        public byte[] Img { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "El {0} debe ser de {1} a {2}")]
        [Display(Name = "Nombre del platillo")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(maximumLength: 250, ErrorMessage = "El {0} debe tener un maximo de {1} letras")]
        public string Descripcion { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "La {0} es requerida")]
        public int Precio { get; set; }
    }
}
