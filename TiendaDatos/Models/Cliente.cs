using System;
using System.Collections.Generic;

namespace TiendaDatos.Models;

public partial class Cliente
{
    public Guid IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public string? Direccion { get; set; }

    public bool? Activo { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<ClienteArticulo> ClienteArticulos { get; } = new List<ClienteArticulo>();
}
