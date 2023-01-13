using System;
using System.Collections.Generic;

namespace TiendaDatos.Models;

public partial class Articulo
{
    public Guid IdArticulo { get; set; }

    public string? Codigo { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public string? Imagen { get; set; }

    public int? Stock { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<ClienteArticulo> ClienteArticulos { get; } = new List<ClienteArticulo>();

    public virtual ICollection<TiendaArticulo> TiendaArticulos { get; } = new List<TiendaArticulo>();
}
