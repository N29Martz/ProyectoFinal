using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(maximumLength:50, MinimumLength =3, ErrorMessage = "El {0} debe ser de {1} a {2}")]
        [Display(Name = "Nombre del cliente")]
        public string Nombre { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "La {0} es requerida")]
        public int Cantidad { get; set; }


        [Required(ErrorMessage = "la {0} es requerido")]
        [Display(Name = "Entrega")]
        public string Entrega { get; set; }

        [Required(ErrorMessage = "La {0} es requerida")]
        [Display(Name = "Descripción")]
        [StringLength(maximumLength: 100, MinimumLength = 10, ErrorMessage = "El {0} debe ser de {1} a {2}")]
        public string Descripcion { get; set; }

    }
}
