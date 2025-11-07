using System;
using System.Collections.Generic;

namespace proyecto_TBD.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? NombreEmpresa { get; set; }

    public string Correo { get; set; } = null!;

    public virtual ICollection<Donativo> Donativos { get; set; } = new List<Donativo>();
}
