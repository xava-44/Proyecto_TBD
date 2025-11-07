using System;
using System.Collections.Generic;

namespace proyecto_TBD.Models;

public partial class Institucione
{
    public int IdInstituto { get; set; }

    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? Descripcion { get; set; }

    public int ID_usuario { get; set; }

    public virtual ICollection<Donativo> Donativos { get; set; } = new List<Donativo>();
}
