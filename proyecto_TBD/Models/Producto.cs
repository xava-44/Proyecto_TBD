using System;
using System.Collections.Generic;

namespace proyecto_TBD.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripción { get; set; }

    public decimal PesoAprox { get; set; }

    public int ID_usuario{ get; set; }



    public virtual ICollection<Donativo> Donativos { get; set; } = new List<Donativo>();
}
