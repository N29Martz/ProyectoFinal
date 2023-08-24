using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Bebida
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "El {0} debe ser de {1} a {2}")]
        [Display(Name = "Nombre de la bebida")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La marca es requerida")]
        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "La {0} es requerida")]
        public int Precio { get; set; }
    }
}
