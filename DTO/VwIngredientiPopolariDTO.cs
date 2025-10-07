﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class VwIngredientiPopolariDTO
    {
        public int IngredienteId { get; set; }
        public string NomeIngrediente { get; set; } = null!;
        public string Categoria { get; set; } = null!;
        public int? NumeroSelezioni { get; set; }
        public int? NumeroOrdiniContenenti { get; set; }
        public decimal? PercentualeTotale { get; set; }
    }
    
    
}
