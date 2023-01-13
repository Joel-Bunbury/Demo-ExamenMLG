using System;
using System.Collections.Generic;

namespace TiendaDatos.Models;

public partial class ClienteArticulo
{
    public int IdClienteArticulo { get; set; }

    public Guid IdCliente { get; set; }

    public Guid IdArticulo { get; set; }

    public DateTime Fecha { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Total { get; set; }

    public bool? Pagado { get; set; }

    public virtual Articulo IdArticuloNavigation { get; set; } = null!;

    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
