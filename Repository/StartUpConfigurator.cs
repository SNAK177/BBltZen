using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.Interface;
using Repository.Service;

namespace Repository
{
    public static class StartUpConfigurator
    {
        public static void AddServiceDb(this IServiceCollection services)
        {
            services.AddScoped<IOrdineRepository, OrdineRepository>();
            services.AddScoped<IDolceRepository, DolceRepository>();
            services.AddScoped<IIngredienteRepository, IngredienteRepository>();
            services.AddScoped<INotificheOperativeRepository, NotificheOperativeRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrdineRepository, OrdineRepository>();
            services.AddScoped<ISessioniQrRepository, SessioniQrRepository>();
            services.AddScoped<IStatoOrdineRepository, StatoOrdineRepository>();
            services.AddScoped<IStatoPagamentoRepository, StatoPagamentoRepository>();
            services.AddScoped<ITavoloRepository, TavoloRepository>();
            services.AddScoped<ITaxRatesRepository, TaxRatesRepository>();
            services.AddScoped<IUtentiRepository, UtentiRepository>();
            services.AddScoped<IVwIngredientiPopolariRepository, VwIngredientiPopolariRepository>();
            services.AddScoped<
                IVwStatisticheOrdiniAvanzateRepository,
                VwStatisticheOrdiniAvanzateRepository
            >();
            //services.AddScoped<IBevandaRepository, BevandaCustomRepository>();
            services.AddScoped<IArticoloRepository, ArticoloRepository>();

        }
    }
}
