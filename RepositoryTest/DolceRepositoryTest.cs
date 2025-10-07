using Database;
using DTO;
using Microsoft.EntityFrameworkCore;
using Repository.Service;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RepositoryTest
{
    public class DolceRepositoryTests
    {
        private BubbleTeaContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BubbleTeaContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            return new BubbleTeaContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldAddDolce()
        {
            var context = GetInMemoryContext();
            var repo = new DolceRepository(context);
            var art = new ArticoloDTO
            {
                
                Tipo = "Dolce"
            };
            var dto = new DolceDTO
            {
                ArticoloId = art.ArticoloId,
                Nome = "Tiramisu",
                Prezzo = 4.5m,
                Disponibile = true,
                Priorita = 1
            };

            var result = await repo.AddAsync(dto);

            Assert.NotNull(result);
            Assert.Equal("Tiramisu", result.Nome);
            Assert.Single(context.Dolce);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllDolci()
        {
            var context = GetInMemoryContext();
            var repo = new DolceRepository(context);

            await repo.AddAsync(new DolceDTO { Nome = "Tiramisu", Prezzo = 4.5m, Disponibile = true });
            await repo.AddAsync(new DolceDTO { Nome = "Cheesecake", Prezzo = 3.5m, Disponibile = true });

            var all = await repo.GetAllAsync();

            Assert.Equal(2, all.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectDolce()
        {
            var context = GetInMemoryContext();
            var repo = new DolceRepository(context);
            

            var added = await repo.AddAsync(new DolceDTO { Nome = "Brownie", Prezzo = 2.5m, Disponibile = true });
            var found = await repo.GetByIdAsync(added.ArticoloId);

            Assert.NotNull(found);
            Assert.Equal("Brownie", found!.Nome);
        }

        [Fact]
        public async Task UpdateAsync_ShouldModifyExistingDolce()
        {
            var context = GetInMemoryContext();
            var repo = new DolceRepository(context);

            var added = await repo.AddAsync(new DolceDTO { Nome = "Muffin", Prezzo = 2m, Disponibile = true });
            added.Nome = "Muffin al Cioccolato";

            await repo.UpdateAsync(added);
            var updated = await repo.GetByIdAsync(added.ArticoloId);

            Assert.Equal("Muffin al Cioccolato", updated!.Nome);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveDolce()
        {
            var context = GetInMemoryContext();
            var repo = new DolceRepository(context);

            var added = await repo.AddAsync(new DolceDTO { Nome = "Panna Cotta", Prezzo = 3m, Disponibile = true });
            var result = await repo.DeleteAsync(added.ArticoloId);

            Assert.True(result);
            Assert.Empty(await repo.GetAllAsync());
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
