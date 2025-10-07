using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DolceDTO
    {
        [Key]
        [DatabaseGenerated((DatabaseGeneratedOption.Identity))]
        public int ArticoloId { get; set; }

        [Required] 
        public string Nome { get; set; } = null!;
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Prezzo { get; set; }
        public string? Descrizione { get; set; }
        public string? ImmagineUrl { get; set; }
        public bool Disponibile { get; set; }
        public int Priorita { get; set; }
        public DateTime DataCreazione { get; set; }
        public DateTime DataAggiornamento { get; set; }

    }
}
