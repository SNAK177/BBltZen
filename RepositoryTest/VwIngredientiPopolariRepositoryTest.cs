using Database;
using DTO;
using Microsoft.EntityFrameworkCore;
using Repository.Service;

namespace RepositoryTest
{
    
    public class VwIngredientiPopolariRepositoryTest
    {
        private BubbleTeaContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<BubbleTeaContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new BubbleTeaContext(options);

           
            // Simuliamo dati iniziali
            context.VwIngredientiPopolari.AddRange(
                new VwIngredientiPopolari
                {
                    IngredienteId = 1,
                    NomeIngrediente = "Tapioca",
                    Categoria = "Base",
                    NumeroSelezioni = 150,
                    NumeroOrdiniContenenti = 120,
                    PercentualeTotale = 45.6m
                },
                new VwIngredientiPopolari
                {
                    IngredienteId = 2,
                    NomeIngrediente = "Latte",
                    Categoria = "Liquido",
                    NumeroSelezioni = 200,
                    NumeroOrdiniContenenti = 180,
                    PercentualeTotale = 60.2m
                },
                new VwIngredientiPopolari
                {
                    IngredienteId = 3,
                    NomeIngrediente = "Matcha",
                    Categoria = "Tè",
                    NumeroSelezioni = 90,
                    NumeroOrdiniContenenti = 80,
                    PercentualeTotale = 25.1m
                }
            );

            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllRecords()
        {
            var context = GetInMemoryDbContext();
            var repo = new VwIngredientiPopolariRepository(context);
            var ingre = new IngredienteDTO
            {
                Ingrediente1 = "Tapioca"
            };
            var primo = new VwIngredientiPopolariDTO
            {
                IngredienteId = 1,
                NomeIngrediente = ingre.Ingrediente1,
                Categoria = "Base",
                NumeroSelezioni = 150,
                NumeroOrdiniContenenti = 120,
                PercentualeTotale = 45.6m
            };
            await context.SaveChangesAsync();
            var result = await repo.GetAllAsync();
            
            Assert.NotNull(result);
            Assert.Single(result.Where(r => r.IngredienteId == primo.IngredienteId));
            Assert.Contains(result, sr => r.NomeIngrediente == "Tapioca"));
        }

        [Fact]
        public async Task GetTopNAsync_ShouldReturnOrderedTopN()
        {
            var context = GetInMemoryDbContext();
            var repo = new VwIngredientiPopolariRepository(context);

            var result = await repo.GetTopNAsync(2);

            Assert.Equal(2, result.Count());
            Assert.Equal("Latte", result.First().NomeIngrediente); // Ha il numero selezioni più alto
        }

        [Fact]
        public async Task GetByCategoriaAsync_ShouldReturnFilteredResults()
        {
            var context = GetInMemoryDbContext();
            var repo = new VwIngredientiPopolariRepository(context);

            var result = await repo.GetByCategoriaAsync("Base");

            Assert.Single(result);
            Assert.Equal("Tapioca", result.First().NomeIngrediente);
        }

        [Fact]
        public async Task GetByIngredienteIdAsync_ShouldReturnCorrectItem()
        {
            var context = GetInMemoryDbContext();
            var repo = new VwIngredientiPopolariRepository(context);

            var result = await repo.GetByIngredienteIdAsync(2);

            Assert.NotNull(result);
            Assert.Equal("Latte", result.NomeIngrediente);
        }

        [Fact]
        public async Task GetByIngredienteIdAsync_ShouldReturnNull_WhenNotFound()
        {
            var context = GetInMemoryDbContext();
            var repo = new VwIngredientiPopolariRepository(context);

            var result = await repo.GetByIngredienteIdAsync(999);

            Assert.Null(result);
        }
    }
}