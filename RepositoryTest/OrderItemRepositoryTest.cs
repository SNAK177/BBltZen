using Database;
using DTO;
using Microsoft.EntityFrameworkCore;
using Repository.Service;

namespace RepositoryTest;

public class OrderItemRepositoryTest
{
    private BubbleTeaContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<BubbleTeaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // DB nuovo per ogni test
            .Options;

        return new BubbleTeaContext(options);
    }
    [Fact]
    public async Task AddAsync_ShouldAddOrderItem()
    {
        using var context = GetInMemoryContext();
        var repo = new OrderItemRepository(context);

        var dto = new DTO.OrderItemDTO
        {
            OrdineId = 1,
            ArticoloId = 1,
            Quantita = 2,
            PrezzoUnitario = 5.0m,
            ScontoApplicato = 0.0m,
            Imponibile = 10.0m,
            DataCreazione = DateTime.UtcNow,
            DataAggiornamento = DateTime.UtcNow,
            TipoArticolo = "Bevanda",
            TotaleIvato = 12.2m,
            TaxRateId = 1
        };
        await repo.AddAsync(dto);
        var saved = await repo.GetByIdAsync(dto.OrderItemId);

        Assert.NotNull(saved);
        Assert.Equal("Bevanda", saved.TipoArticolo);
        Assert.Equal(2, saved.Quantita);
    }
    
    [Fact]
        public async Task GetAllAsync_Should_Return_Items()
        {
            using var context = GetInMemoryContext();
            var repo = new OrderItemRepository(context);

            await repo.AddAsync(new OrderItemDTO
            {
                OrdineId = 1,
                ArticoloId = 200,
                Quantita = 3,
                PrezzoUnitario = 4.0m,
                ScontoApplicato = 0.0m,
                Imponibile = 12.0m,
                TipoArticolo = "Snack",
                TotaleIvato = 13.20m,
                TaxRateId = 1
            });

            var items = await repo.GetAllAsync();

            Assert.Single(items);
            Assert.Equal("Snack", items.First().TipoArticolo);
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_OrderItem()
        {
            using var context = GetInMemoryContext();
            var repo = new OrderItemRepository(context);

            var dto = new OrderItemDTO
            {
                OrdineId = 2,
                ArticoloId = 300,
                Quantita = 1,
                PrezzoUnitario = 8.0m,
                ScontoApplicato = 0.0m,
                Imponibile = 8.0m,
                TipoArticolo = "Topping",
                TotaleIvato = 9.76m,
                TaxRateId = 2
            };

            await repo.AddAsync(dto);

            dto.Quantita = 5;
            dto.ScontoApplicato = 2.0m;

            await repo.UpdateAsync(dto);

            var updated = await repo.GetByIdAsync(dto.OrderItemId);

            Assert.Equal(5, updated.Quantita);
            Assert.Equal(2.0m, updated.ScontoApplicato);
        }

        [Fact]
        public async Task DeleteAsync_Should_Remove_Item()
        {
            using var context = GetInMemoryContext();
            var repo = new OrderItemRepository(context);

            var dto = new OrderItemDTO
            {
                OrdineId = 3,
                ArticoloId = 400,
                Quantita = 2,
                PrezzoUnitario = 6.0m,
                ScontoApplicato = 0.0m,
                Imponibile = 12.0m,
                TipoArticolo = "Drink",
                TotaleIvato = 13.20m,
                TaxRateId = 3
            };

            await repo.AddAsync(dto);
            var id = dto.OrderItemId;

            await repo.DeleteAsync(id);

            var exists = await repo.ExistsAsync(id);
            Assert.False(exists);
        }
}