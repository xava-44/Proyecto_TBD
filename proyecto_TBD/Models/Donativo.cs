using System;
using System.Collections.Generic;

namespace proyecto_TBD.Models;

public partial class Donativo
{

    public int Iddonativos { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdProducto { get; set; }

    public int? IdInstituto { get; set; }

    public DateTime Fecha { get; set; }

    public int? Cantidad { get; set; }

    public string? Descripcion { get; set; }

    public virtual Institucione? IdInstitutoNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
