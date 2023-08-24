using Microsoft.AspNetCore.Mvc.Rendering;


namespace ProyectoFinal.Models
{
    public class PedidoCreacionViewModel : Pedido
    {
        public IEnumerable<SelectListItem> TiposPlatillo { get; set; }

        public IEnumerable<SelectListItem> TiposBebida { get; set; }

        public IEnumerable<SelectListItem> TiposPostre { get; set; }
    }
}
