using Database;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Service;

namespace RepositoryTest
{
    public class IngredienteRepositoryTest
    {
        private BubbleTeaContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BubbleTeaContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new BubbleTeaContext(options);
            context.Database.EnsureCreated(); // Important!
            return context;
        }

        [Fact]
        public async Task AddAsync_ShouldAddIngredient()
        {
            using var context = GetInMemoryContext();
            var repository = new IngredienteRepository(context);

            var dto = new IngredienteDTO
            {
                Ingrediente1 = "Ingrediente aggiunto",
                CategoriaId = 1,
                Disponibile = true,
                PrezzoAggiunto = 0.5m,
                DataInserimento = DateTime.UtcNow,
                DataAggiornamento = DateTime.UtcNow
            };

            await repository.AddAsync(dto);
            var result = await repository.GetByIdAsync(dto.IngredienteId);

            Assert.NotNull(result);
            Assert.Equal("Ingrediente aggiunto", result.Ingrediente1);
        }

        [Fact]
        public async Task GetDisponibiliAsync_ShouldReturnOnlyAvailable()
        {
            using var context = GetInMemoryContext();
            var repository = new IngredienteRepository(context);

            await repository.AddAsync(new IngredienteDTO
            {
                Ingrediente1 = "Tapioca",
                CategoriaId = 1,
                PrezzoAggiunto = 0.50m,
                Disponibile = true,
                DataInserimento = DateTime.UtcNow,
                DataAggiornamento = DateTime.UtcNow
            });

            await repository.AddAsync(new IngredienteDTO
            {
                Ingrediente1 = "Gelatina",
                CategoriaId = 2,
                PrezzoAggiunto = 0.30m,
                Disponibile = false,
                DataInserimento = DateTime.UtcNow,
                DataAggiornamento = DateTime.UtcNow
            });

            var disponibili = await repository.GetDisponibiliAsync(true);

            Assert.Single(disponibili);
            Assert.Equal("Tapioca", disponibili.First().Ingrediente1);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveIngredient()
        {
            using var context = GetInMemoryContext();
            var repository = new IngredienteRepository(context);

            var dto = new IngredienteDTO
            {
                Ingrediente1 = "Latte",
                CategoriaId = 1,
                PrezzoAggiunto = 0.20m,
                Disponibile = true,
                DataInserimento = DateTime.UtcNow,
                DataAggiornamento = DateTime.UtcNow
            };

            await repository.AddAsync(dto);

            var before = await repository.GetAllAsync();
            Assert.Single(before);

            await repository.DeleteAsync(dto.IngredienteId);

            var after = await repository.GetAllAsync();
            Assert.Empty(after);
        }
    }
}