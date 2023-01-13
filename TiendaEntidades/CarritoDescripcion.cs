using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TiendaEntidades
{
    public class CarritoDescripcion
    {
        public Guid IdCliente { get; set; }

        public Guid IdArticulo { get; set; }

        public DateTime Fecha { get; set; }

        [Required]
        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

        public decimal Total { get; set; }

        public bool Pagado { get; set; }

        public TiendaEntidades.Articulo Articulo { get; set; }
    }
}