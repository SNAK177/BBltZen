using Database;
using DTO;
using Microsoft.EntityFrameworkCore;
using Repository.Service;
using Xunit;

namespace RepositoryTest
{
    public class StatoOrdineRepositoryTest
    {
        private BubbleTeaContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BubbleTeaContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new BubbleTeaContext(options);
        }

        [Fact]
        public async Task AddAsync_Should_Add_StatoOrdine()
        {
            using var context = GetInMemoryContext();
            var repo = new StatoOrdineRepository(context);

            var dto = new StatoOrdineDTO
            {
                StatoOrdine1 = "In preparazione",
                Terminale = false
            };

            await repo.AddAsync(dto);
            var saved = await repo.GetByIdAsync(dto.StatoOrdineId);

            Assert.NotNull(saved);
            Assert.Equal("In preparazione", saved.StatoOrdine1);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_Records()
        {
            using var context = GetInMemoryContext();
            var repo = new StatoOrdineRepository(context);

            await repo.AddAsync(new StatoOrdineDTO { StatoOrdine1 = "Creato", Terminale = false });
            await repo.AddAsync(new StatoOrdineDTO { StatoOrdine1 = "Completato", Terminale = true });

            var result = await repo.GetAllAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByNomeAsync_Should_Return_Correct_Item()
        {
            using var context = GetInMemoryContext();
            var repo = new StatoOrdineRepository(context);

            await repo.AddAsync(new StatoOrdineDTO { StatoOrdine1 = "In consegna", Terminale = false });

            var result = await repo.GetByNomeAsync("In consegna");

            Assert.NotNull(result);
            Assert.Equal("In consegna", result.StatoOrdine1);
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_Entity()
        {
            using var context = GetInMemoryContext();
            var repo = new StatoOrdineRepository(context);

            var dto = new StatoOrdineDTO { StatoOrdine1 = "Preparazione", Terminale = false };
            await repo.AddAsync(dto);

            dto.StatoOrdine1 = "Consegnato";
            dto.Terminale = true;

            await repo.UpdateAsync(dto);

            var updated = await repo.GetByIdAsync(dto.StatoOrdineId);

            Assert.Equal("Consegnato", updated.StatoOrdine1);
            Assert.True(updated.Terminale);
        }

        [Fact]
        public async Task DeleteAsync_Should_Remove_Item()
        {
            using var context = GetInMemoryContext();
            var repo = new StatoOrdineRepository(context);

            var dto = new StatoOrdineDTO { StatoOrdine1 = "Annullato", Terminale = true };
            await repo.AddAsync(dto);

            await repo.DeleteAsync(dto.StatoOrdineId);

            var exists = await repo.ExistsAsync(dto.StatoOrdineId);
            Assert.False(exists);
        }

        [Fact]
        public async Task GetStatiTerminaliAsync_Should_Return_Only_Terminali()
        {
            using var context = GetInMemoryContext();
            var repo = new StatoOrdineRepository(context);

            await repo.AddAsync(new StatoOrdineDTO { StatoOrdine1 = "Completato", Terminale = true });
            await repo.AddAsync(new StatoOrdineDTO { StatoOrdine1 = "In corso", Terminale = false });

            var result = await repo.GetStatiTerminaliAsync();

            Assert.Single(result);
            Assert.True(result.First().Terminale);
        }
    }
}
