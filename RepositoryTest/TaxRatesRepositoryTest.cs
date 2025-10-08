using Database;
using DTO;
using Microsoft.EntityFrameworkCore;
using Repository.Service;
using Xunit;

namespace RepositoryTest
{
    public class TaxRatesRepositoryTests
    {
        private BubbleTeaContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<BubbleTeaContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new BubbleTeaContext(options);
        }

        [Fact]
        public async Task AddAndGetTaxRate_ShouldWork()
        {
            var context = GetInMemoryDbContext();
            var repository = new TaxRatesRepository(context);

            var dto = new TaxRatesDTO { Aliquota = 22, Descrizione = "IVA" };
            await repository.AddAsync(dto);

            var result = await repository.GetByIdAsync(dto.TaxRateId);

            Assert.NotNull(result);
            Assert.Equal(22, result.Aliquota);
            Assert.Equal("IVA", result.Descrizione);
        }

        [Fact]
        public async Task UpdateTaxRate_ShouldWork()
        {
            var context = GetInMemoryDbContext();
            var repository = new TaxRatesRepository(context);

            var dto = new TaxRatesDTO { Aliquota = 10, Descrizione = "IVA 10%" };
            await repository.AddAsync(dto);

            dto.Descrizione = "IVA Ridotta";
            await repository.UpdateAsync(dto);

            var result = await repository.GetByIdAsync(dto.TaxRateId);
            Assert.Equal("IVA Ridotta", result.Descrizione);
        }

        [Fact]
        public async Task DeleteTaxRate_ShouldWork()
        {
            var context = GetInMemoryDbContext();
            var repository = new TaxRatesRepository(context);

            var dto = new TaxRatesDTO { Aliquota = 5, Descrizione = "IVA 5%" };
            await repository.AddAsync(dto);

            await repository.DeleteAsync(dto.TaxRateId);

            var exists = await repository.ExistsAsync(dto.TaxRateId);
            Assert.False(exists);
        }

        [Fact]
        public async Task GetAllTaxRates_ShouldReturnList()
        {
            var context = GetInMemoryDbContext();
            var repository = new TaxRatesRepository(context);

            await repository.AddAsync(new TaxRatesDTO { Aliquota = 5, Descrizione = "IVA 5%" });
            await repository.AddAsync(new TaxRatesDTO { Aliquota = 10, Descrizione = "IVA 10%" });

            var all = await repository.GetAllAsync();
            Assert.Equal(2, all.Count());
        }
    }
}
