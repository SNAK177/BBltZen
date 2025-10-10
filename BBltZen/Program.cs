using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Interface;
using Repository.Service;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Builder;
using Database;

namespace BBltZen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            //Configura JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtServizio.CHIAVE;
                options.DefaultChallengeScheme = JwtServizio.CHIAVE;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(BBltZen.JwtServizio.CHIAVE))
                };
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddDbContext<Database.BubbleTeaContext>();
            builder.Services.AddScoped<IOrdineRepository, OrdineRepository>();
            builder.Services.AddScoped<IDolceRepository, DolceRepository>();
            builder.Services.AddScoped<IUtentiRepository, UtentiRepository>();
            builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            builder.Services.AddScoped<IIngredienteRepository, IngredienteRepository>();
            builder.Services.AddScoped<IStatoPagamentoRepository, StatoPagamentoRepository>();
            builder.Services.AddScoped<IStatoOrdineRepository, StatoOrdineRepository>();
            builder.Services.AddScoped<ITavoloRepository, TavoloRepository>();
            builder.Services.AddScoped<ISessioniQrRepository, SessioniQrRepository>();
            builder.Services.AddScoped<ITaxRatesRepository, TaxRatesRepository>();
            builder.Services.AddScoped<INotificheOperativeRepository, NotificheOperativeRepository>();
            builder.Services.AddScoped<IVwIngredientiPopolariRepository, VwIngredientiPopolariRepository>();
            builder.Services.AddScoped<IVwStatisticheOrdiniAvanzateRepository, VwStatisticheOrdiniAvanzateRepository>();

            var app = builder.Build();

           
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
