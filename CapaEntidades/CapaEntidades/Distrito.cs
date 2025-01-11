using System;
using System.Collections.Generic;

namespace CapaEntidades.CapaEntidades;

public partial class Distrito
{
    public string IdDistrito { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string IdDepartamento { get; set; } = null!;

    public string IdMunicipio { get; set; } = null!;
}
