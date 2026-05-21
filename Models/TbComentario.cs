using System;
using System.Collections.Generic;

namespace PWS26ApiServer.Models;

public partial class TbComentario
{
    public int IdComentario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string TipoComentario { get; set; } = null!;

    public string Comentarios { get; set; } = null!;
}
