using System.ComponentModel.DataAnnotations;

namespace TiendaMLG.Models.ViewModels
{
    public class TiendaViewModel
    {
        [Required]
        [Display(Name = "Sucursal")]
        public string Sucursal { get; set; }

        [Required]
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }
    }
}
