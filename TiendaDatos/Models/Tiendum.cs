using System;
using System.Collections.Generic;

namespace TiendaDatos.Models;

public partial class Tiendum
{
    public int IdTienda { get; set; }

    public string? Sucursal { get; set; }

    public string? Direccion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<TiendaArticulo> TiendaArticulos { get; } = new List<TiendaArticulo>();
}
