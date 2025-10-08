using DTO;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // Tabelle
        public DbSet<OrdineDTO> Ordini { get; set; }
        public DbSet<OrderItemDTO> OrderItems { get; set; }
        public DbSet<DolceDTO> Dolci { get; set; }
        public DbSet<IngredienteDTO> Ingredienti { get; set; }
        public DbSet<UtentiDTO> Utenti { get; set; }
        public DbSet<TavoloDTO> Tavoli { get; set; }
        public DbSet<SessioniQrDTO> SessioniQr { get; set; }
        public DbSet<LogAccessiDTO> LogAccessi { get; set; }
        public DbSet<LogAttivitaDTO> LogAttivita { get; set; }
        public DbSet<NotificheOperativeDTO> NotificheOperative { get; set; }
        public DbSet<StatoOrdineDTO> StatoOrdini { get; set; }
        public DbSet<StatoPagamentoDTO> StatoPagamenti { get; set; }
        public DbSet<TaxRatesDTO> TaxRates { get; set; }
        public DbSet<BevandaCustomDTO> BevandeCustom { get; set; }
        public DbSet<BevandaStandardDTO> BevandeStandard { get; set; }
        public DbSet<PersonalizzazioneCustomDTO> PersonalizzazioniCustom { get; set; }
        public DbSet<DimensioneBicchiereDTO> DimensioniBicchiere { get; set; }

        // Views
        public DbSet<VwStatisticheOrdiniAvanzateDTO> VwStatisticheOrdiniAvanzate { get; set; }
        public DbSet<VwDashboardAmministrativaDTO> VwDashboardAmministrativa { get; set; }
        public DbSet<VwDashboardSintesiDTO> VwDashboardSintesi { get; set; }
        public DbSet<VwDashboardStatisticheDTO> VwDashboardStatistiche { get; set; }
        public DbSet<VwStatisticheRecentiDTO> VwStatisticheRecenti { get; set; }
        public DbSet<VwStatisticheMensiliDTO> VwStatisticheMensili { get; set; }
        public DbSet<VwStatisticheGiornaliereDTO> VwStatisticheGiornaliere { get; set; }
    }
}
