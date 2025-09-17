using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrdineDTO
    {
        public int OrdineId { get; set; }

        [Required(ErrorMessage = "Il campo ClienteId è obbligatorio.")]
        public int ClienteId { get; set; }
        public DateTime DataCreazione { get; set; }
        public DateTime DataAggiornamento { get; set; }
        public int? StatoOrdineId { get; set; }
        public int? StatoPagamentoId { get; set; }

        [Required(ErrorMessage = "Il campo Totale è obbligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Il campo Totale deve essere maggiore di zero.")]
        public decimal Totale { get; set; }

        [Required(ErrorMessage ="Il campo priorità è maggiore")]
        [Range(1,5, ErrorMessage = "Il campo Priorita deve essere compreso tra 1 e 5.")]
        public int Priorita { get; set; }
    }
}
