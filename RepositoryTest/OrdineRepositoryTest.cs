using Repository.Service;
using DTO;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace RepositoryTests
{
    public class OrdineRepositoryTest
    {
        private BubbleTeaContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BubbleTeaContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new BubbleTeaContext(options);
        }
        [Fact]
        public async Task AddAsync_ShouldAddOrdine()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var repo = new OrdineRepository(context);
            var ordine = new OrdineDTO
            {
                ClienteId = 1,
                //DataCreazione = DateTime.Now,
                //DataAggiornamento = DateTime.Now,
                //StatoOrdineId = 1,
                //StatoPagamentoId = 1,
                Totale = 10.50m,
                Priorita = 1
            };
            // Act
            var result = await repo.AddAsync(ordine);
            // Assert
            Assert.NotNull(result);
            Assert.True(result.ClienteId>0);
            Assert.Equal(1, result.ClienteId);
        }
        [Fact]
        public async Task GetByIdAsync_Should_Return_Ordine_When_Exists()
        {
            using var context = GetInMemoryContext();
            var repo = new OrdineRepository(context);

            var ordine = new OrdineDTO
            {
                ClienteId = 2,
                Totale = 20m,
                Priorita = 4
            };

            var created = await repo.AddAsync(ordine);

            var result = await repo.GetByIdAsync(created.OrdineId);

            Assert.NotNull(result);
            Assert.Equal(2, result.ClienteId);
        }

        [Fact]
        public async Task UpdateAsync_Should_Modify_Existing_Ordine()
        {
            using var context = GetInMemoryContext();
            var repo = new OrdineRepository(context);

            var ordine = new OrdineDTO
            {
                ClienteId = 3,
                Totale = 30m,
                Priorita = 2
            };

            var created = await repo.AddAsync(ordine);

            created.Totale = 50m;
            created.Priorita = 5;

            await repo.UpdateAsync(created);

            var updated = await repo.GetByIdAsync(created.OrdineId);

            Assert.NotNull(updated);
            Assert.Equal(50m, updated.Totale);
            Assert.Equal(5, updated.Priorita);
        }

        [Fact]
        public async Task DeleteAsync_Should_Remove_Ordine()
        {
            using var context = GetInMemoryContext();
            var repo = new OrdineRepository(context);

            var ordine = new OrdineDTO
            {
                ClienteId = 4,
                Totale = 40m,
                Priorita = 1
            };

            var created = await repo.AddAsync(ordine);

            await repo.DeleteAsync(created.OrdineId);

            var result = await repo.GetByIdAsync(created.OrdineId);

            Assert.Null(result);
        }
    }
}