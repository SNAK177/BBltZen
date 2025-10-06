using Database;
using DTO;
using Microsoft.EntityFrameworkCore;
using Repository.Service;
using Xunit;

namespace RepositoryTest
{
    public class TavoloRepositoryTest
    {
        private BubbleTeaContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BubbleTeaContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new BubbleTeaContext(options);
        }

        [Fact]
        public async Task AddAsync_Should_Add_Tavolo()
        {
            using var context = GetInMemoryContext();
            var repo = new TavoloRepository(context);

            var dto = new TavoloDTO
            {
                Numero = 1,
                Zona = "Interna",
                QrCode = "QR001",
                Disponibile = true
            };

            await repo.AddAsync(dto);

            var saved = await repo.GetByIdAsync(dto.TavoloId);

            Assert.NotNull(saved);
            Assert.Equal("Interna", saved.Zona);
            Assert.True(saved.Disponibile);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_Tavoli()
        {
            using var context = GetInMemoryContext();
            var repo = new TavoloRepository(context);

            await repo.AddAsync(new TavoloDTO { Numero = 2, Zona = "Esterna", QrCode = "QR002", Disponibile = true });
            await repo.AddAsync(new TavoloDTO { Numero = 3, Zona = "Interna", QrCode = "QR003", Disponibile = false });

            var result = await repo.GetAllAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByQrCodeAsync_Should_Return_Correct_Tavolo()
        {
            using var context = GetInMemoryContext();
            var repo = new TavoloRepository(context);

            await repo.AddAsync(new TavoloDTO { Numero = 5, Zona = "VIP", QrCode = "QRVIP", Disponibile = true });

            var result = await repo.GetByQrCodeAsync("QRVIP");

            Assert.NotNull(result);
            Assert.Equal("VIP", result.Zona);
        }

        [Fact]
        public async Task GetDisponibiliAsync_Should_Return_Only_Disponibili()
        {
            using var context = GetInMemoryContext();
            var repo = new TavoloRepository(context);

            await repo.AddAsync(new TavoloDTO { Numero = 6, Zona = "Sala 1", QrCode = "QR006", Disponibile = true });
            await repo.AddAsync(new TavoloDTO { Numero = 7, Zona = "Sala 2", QrCode = "QR007", Disponibile = false });

            var disponibili = await repo.GetDisponibiliAsync();

            Assert.Single(disponibili);
            Assert.True(disponibili.First().Disponibile);
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_Tavolo()
        {
            using var context = GetInMemoryContext();
            var repo = new TavoloRepository(context);

            var dto = new TavoloDTO { Numero = 8, Zona = "Old", QrCode = "QR008", Disponibile = true };
            await repo.AddAsync(dto);

            dto.Zona = "New";
            dto.Disponibile = false;

            await repo.UpdateAsync(dto);

            var updated = await repo.GetByIdAsync(dto.TavoloId);
            Assert.Equal("New", updated.Zona);
            Assert.False(updated.Disponibile);
        }

        [Fact]
        public async Task DeleteAsync_Should_Remove_Tavolo()
        {
            using var context = GetInMemoryContext();
            var repo = new TavoloRepository(context);

            var dto = new TavoloDTO { Numero = 9, Zona = "Rimozione", QrCode = "QR009", Disponibile = true };
            await repo.AddAsync(dto);

            await repo.DeleteAsync(dto.TavoloId);

            var exists = await repo.ExistsAsync(dto.TavoloId);
            Assert.False(exists);
        }

        [Fact]
        public async Task GetByZonaAsync_Should_Filter_By_Zona()
        {
            using var context = GetInMemoryContext();
            var repo = new TavoloRepository(context);

            await repo.AddAsync(new TavoloDTO { Numero = 10, Zona = "Terrazza", QrCode = "QR010", Disponibile = true });
            await repo.AddAsync(new TavoloDTO { Numero = 11, Zona = "Interna", QrCode = "QR011", Disponibile = true });

            var terrazza = await repo.GetByZonaAsync("Terrazza");

            Assert.Single(terrazza);
            Assert.Equal("Terrazza", terrazza.First().Zona);
        }
    }
}
