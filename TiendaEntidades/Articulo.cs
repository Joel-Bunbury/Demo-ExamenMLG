using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TiendaEntidades
{
    public class Articulo
    {
        public Guid IdArticulo { get; set; }
        public int IdTienda { get; set; }
        [Required]
        [Display(Name = "Codigo")]
        public string Codigo { get; set; }
        [Required]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set;}
        [Required]
        [Display(Name = "Precio")]
        public decimal Precio { get; set;}
        public string Imagen { get; set;}
        [Required]
        [Display(Name = "Cantidad")]
        public int Stock { get; set;}
    }
}