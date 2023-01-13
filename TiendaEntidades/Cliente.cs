using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TiendaEntidades
{
    public class Cliente
    {
        public Guid IdCliente { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Apellido paterno")]
        public string ApellidoPaterno { get; set;}
        [Required]
        [Display(Name = "Apellido materno")]
        public string ApellidoMaterno { get; set;}
        [Required]
        [Display(Name = "Dirección")]
        public string Direccion { get; set;}
        [Required]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        public string Rol { get; set; }
    }
}