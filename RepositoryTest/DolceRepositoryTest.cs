using System;
using System.Linq;
using System.Threading.Tasks;
using Database;
using DTO;
using Microsoft.EntityFrameworkCore;
using Repository.Service;
using Xunit;

namespace RepositoryTests
{
    public class DolceRepositoryTests
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
        public async Task AddAsync_ShouldAddDolce()
        {
            var context = GetInMemoryContext();
            var repo = new DolceRepository(context);

            var dto = new DolceDTO
            {
                ArticoloId = 999,
                Nome = "Tiramisu",
                Prezzo = 4.5m,
                Disponibile = true,
                Priorita = 1
            };

            var result = await repo.AddAsync(dto);

            Assert.NotEqual(0, result.ArticoloId);
            Assert.Equal("Tiramisu", result.Nome);
            Assert.True(result.Disponibile);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllNonDeletedDolci()
        {
            var context = GetInMemoryContext();
            var repo = new DolceRepository(context);

            await repo.AddAsync(new DolceDTO { Nome = "Tiramisu", Prezzo = 4.5m, Disponibile = true, Priorita = 1 });
            await repo.AddAsync(new DolceDTO { Nome = "Cheesecake", Prezzo = 3.5m, Disponibile = true, Priorita = 2 });

            var all = await repo.GetAllAsync();
            Assert.Equal(2, all.Count());
        }

        [Fact]
        public async Task DeleteAsync_ShouldSoftDeleteDolce()
        {
            var context = GetInMemoryContext();
            var repo = new DolceRepository(context);

            var dto = await repo.AddAsync(new DolceDTO { Nome = "Tiramisu", Prezzo = 4.5m, Disponibile = true, Priorita = 1 });

            var deleted = await repo.DeleteAsync(dto.ArticoloId);
            Assert.True(deleted);

            var all = await repo.GetAllAsync();
            Assert.Empty(all); // Non deve essere restituito
        }

        [Fact]
        public async Task ToggleDisponibilitaAsync_ShouldChangeDisponibilita()
        {
            var context = GetInMemoryContext();
            var repo = new DolceRepository(context);

            var dto = await repo.AddAsync(new DolceDTO { Nome = "Tiramisu", Prezzo = 4.5m, Disponibile = true, Priorita = 1 });

            var toggled = await repo.ToggleDisponibilitaAsync(dto.ArticoloId, false);
            Assert.True(toggled);

            var updated = await repo.GetByIdAsync(dto.ArticoloId);
            Assert.False(updated!.Disponibile);
        }

        [Fact]
        public async Task GetByPrioritaAsync_ShouldReturnFilteredDolci()
        {
            var context = GetInMemoryContext();
            var repo = new DolceRepository(context);

            await repo.AddAsync(new DolceDTO { Nome = "Tiramisu", Prezzo = 4.5m, Disponibile = true, Priorita = 1 });
            await repo.AddAsync(new DolceDTO { Nome = "Cheesecake", Prezzo = 3.5m, Disponibile = true, Priorita = 2 });

            var filtered = await repo.GetByPrioritaAsync(1);
            Assert.Single(filtered);
            Assert.Equal("Tiramisu", filtered.First().Nome);
        }
    }
}
