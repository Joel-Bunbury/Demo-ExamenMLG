using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TiendaEntidades
{
    public class Login
    {
        
        [Required]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Contraseña")]
        public string Password { get; set;}
    }
}