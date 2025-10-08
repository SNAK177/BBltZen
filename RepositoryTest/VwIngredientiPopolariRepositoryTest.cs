using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Database;
using DTO;
using Microsoft.EntityFrameworkCore;
using Repository.Service;
using Xunit;

namespace RepositoryTest
{
    //test non possibile se non uso una classe di test che chiamo fake
    public class VwIngredientiPopolariRepositoryTest
    {
        private BubbleTeaContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BubbleTeaContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new BubbleTeaContext(options);

            // Dati di test
            var data = new List<FakeVwIngredientiPopolari>
            {
                new()
                {
                    IngredienteId = 1,
                    NomeIngrediente = "Tapioca",
                    Categoria = "Base",
                    NumeroSelezioni = 120,
                    NumeroOrdiniContenenti = 80,
                    PercentualeTotale = 50.5m,
                },
                new()
                {
                    IngredienteId = 2,
                    NomeIngrediente = "Lychee Jelly",
                    Categoria = "Topping",
                    NumeroSelezioni = 90,
                    NumeroOrdiniContenenti = 60,
                    PercentualeTotale = 37.5m,
                },
                new()
                {
                    IngredienteId = 3,
                    NomeIngrediente = "Matcha",
                    Categoria = "Base",
                    NumeroSelezioni = 70,
                    NumeroOrdiniContenenti = 50,
                    PercentualeTotale = 30m,
                },
            };

            // Invece di AddRange() → popola direttamente in memoria
            context.AddRange(data);
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllIngredienti()
        {
            using var context = GetInMemoryContext();
            var repository = new VwIngredientiPopolariRepository(context);

            var result = await repository.GetAllAsync();

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetTopNAsync_ReturnsTopNByNumeroSelezioni()
        {
            using var context = GetInMemoryContext();
            var repository = new VwIngredientiPopolariRepository(context);

            var result = await repository.GetTopNAsync(2);

            Assert.Equal(2, result.Count());
            Assert.Equal("Tapioca", result.First().NomeIngrediente);
        }

        [Fact]
        public async Task GetByCategoriaAsync_ReturnsFilteredByCategoria()
        {
            using var context = GetInMemoryContext();
            var repository = new VwIngredientiPopolariRepository(context);

            var result = await repository.GetByCategoriaAsync("Base");

            Assert.Equal(2, result.Count());
            Assert.All(result, r => Assert.Equal("Base", r.Categoria));
        }

        [Fact]
        public async Task GetByIngredienteIdAsync_ReturnsCorrectItem()
        {
            using var context = GetInMemoryContext();
            var repository = new VwIngredientiPopolariRepository(context);

            var result = await repository.GetByIngredienteIdAsync(2);

            Assert.NotNull(result);
            Assert.Equal("Lychee Jelly", result.NomeIngrediente);
        }

        [Fact]
        public async Task GetByIngredienteIdAsync_ReturnsNull_IfNotFound()
        {
            using var context = GetInMemoryContext();
            var repository = new VwIngredientiPopolariRepository(context);

            var result = await repository.GetByIngredienteIdAsync(999);

            Assert.Null(result);
        }

        public class FakeVwIngredientiPopolari
        {
            [Key] // Chiave solo per test
            public int IngredienteId { get; set; }
            public string NomeIngrediente { get; set; } = null!;
            public string Categoria { get; set; } = null!;
            public int? NumeroSelezioni { get; set; }
            public int? NumeroOrdiniContenenti { get; set; }
            public decimal? PercentualeTotale { get; set; }
        }
    }
}
