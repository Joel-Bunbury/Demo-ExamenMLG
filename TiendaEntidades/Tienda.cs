using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TiendaEntidades
{
    public class Tienda
    {
        public int IdTienda { get; set; }

        [Required]
        [Display(Name = "Sucursal")]
        public string Sucursal { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        public string Direccion { get; set;}
    }
}