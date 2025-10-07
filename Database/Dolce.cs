using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database;

public partial class Dolce
{
    //[Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key , ForeignKey(nameof(Articolo))]
    public int ArticoloId { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Prezzo { get; set; }

    public string? Descrizione { get; set; }

    public string? ImmagineUrl { get; set; }

    public bool Disponibile { get; set; }

    public int Priorita { get; set; }

    public DateTime DataCreazione { get; set; }

    public DateTime DataAggiornamento { get; set; }

    public virtual Articolo Articolo { get; set; } = null!;

}
