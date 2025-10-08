using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database;
[Keyless]

public partial class VwIngredientiPopolari
{
    public int IngredienteId { get; set; }

    public string NomeIngrediente { get; set; } = null!;

    public string Categoria { get; set; } = null!;

    public int? NumeroSelezioni { get; set; }

    public int? NumeroOrdiniContenenti { get; set; }

    public decimal? PercentualeTotale { get; set; }
}
