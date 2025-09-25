using Database;
using Repository;
using Repository.Interface;
using Repository.Service;

namespace RepositoryTest
{
    public class BaseTest
    {
        protected readonly IServiceProvider _service;
        public BaseTest()
        {
            var build = WebApplication.CreateBuilder();

            build.Services.AddTransient<IOrdineRepository, OrdineRepository>();
            build.Services.AddTransient<IDolceRepository, DolceRepository>();
            build.Services.AddTransient<IUtentiRepository, UtentiRepository>();
            build.Services.AddTransient<INotificheOperativeRepository, NotificheOperativeRepository>();
            build.Services.AddTransient<IStatoPagamentoRepository, StatoPagamentoRepository>();
            build.Services.AddTransient<IStatoOrdineRepository, StatoOrdineRepository>();
            build.Services.AddTransient<ITavoloRepository, TavoloRepository>();
            build.Services.AddTransient<ITaxRatesRepository, TaxRatesRepository>();
            build.Services.AddTransient<ISessioniQrRepository, SessioniQrRepository>();
            build.Services.AddTransient<IIngredienteRepository, IngredienteRepository>();
            build.Services.AddTransient<IVwIngredientiPopolariRepository, VwIngredientiPopolariRepository>();
            build.Services.AddTransient<IVwStatisticheOrdiniAvanzateRepository, VwStatisticheOrdiniAvanzateRepository>();
            build.Services.AddTransient<IOrderItemRepository, OrderItemRepository>();
            
            build.Services.AddServiceDb();
            build.Services.AddDbContext<BubbleTeaContext>();
            build.Services.AddDbContext<Database.BubbleTeaContext>();
            var dati = build.Build();
            _service = dati.Services;
        }
    }
}
