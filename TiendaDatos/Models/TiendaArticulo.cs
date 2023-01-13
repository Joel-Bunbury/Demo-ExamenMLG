using System;
using System.Collections.Generic;

namespace TiendaDatos.Models;

public partial class TiendaArticulo
{
    public int IdTiendaArticulo { get; set; }

    public int IdTienda { get; set; }

    public Guid IdArticulo { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Articulo IdArticuloNavigation { get; set; } = null!;

    public virtual Tiendum IdTiendaNavigation { get; set; } = null!;
}
