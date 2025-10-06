using Database;
using DTO;
using Microsoft.EntityFrameworkCore;
using Repository.Service;

namespace RepositoryTest
{

    public class SessioniQrRepositoryTest
    {
        private BubbleTeaContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BubbleTeaContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new BubbleTeaContext(options);
        }
        [Fact]
        public async Task AddAsync_Should_Add_Sessione()
        {
            using var context = GetInMemoryContext();
            var repo = new SessioniQrRepository(context);

            var dto = new SessioniQrDTO
            {
                ClienteId = 1,
                QrCode = "ABC123",
                DataScadenza = DateTime.UtcNow.AddDays(1),
                Utilizzato = false
            };

            await repo.AddAsync(dto);
            var saved = await repo.GetByIdAsync(dto.SessioneId);

            Assert.NotNull(saved);
            Assert.Equal("ABC123", saved.QrCode);
            Assert.False(saved.Utilizzato ?? false);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_Sessioni()
        {
            using var context = GetInMemoryContext();
            var repo = new SessioniQrRepository(context);

            await repo.AddAsync(new SessioniQrDTO
            {
                ClienteId = 2,
                QrCode = "QRCODE1",
                DataScadenza = DateTime.UtcNow.AddDays(2),
                Utilizzato = false
            });

            var items = await repo.GetAllAsync();

            Assert.Single(items);
            Assert.Equal("QRCODE1", items.First().QrCode);
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_Sessione()
        {
            using var context = GetInMemoryContext();
            var repo = new SessioniQrRepository(context);

            var dto = new SessioniQrDTO
            {
                ClienteId = 3,
                QrCode = "UPDATE_TEST",
                DataScadenza = DateTime.UtcNow.AddDays(3),
                Utilizzato = false
            };

            await repo.AddAsync(dto);

            dto.Utilizzato = true;
            dto.DataUtilizzo = DateTime.UtcNow;

            await repo.UpdateAsync(dto);

            var updated = await repo.GetByIdAsync(dto.SessioneId);
            Assert.True(updated.Utilizzato);
            Assert.NotNull(updated.DataUtilizzo);
        }

        [Fact]
        public async Task DeleteAsync_Should_Remove_Sessione()
        {
            using var context = GetInMemoryContext();
            var repo = new SessioniQrRepository(context);

            var dto = new SessioniQrDTO
            {
                ClienteId = 4,
                QrCode = "DELETE_TEST",
                DataScadenza = DateTime.UtcNow.AddDays(5),
                Utilizzato = false
            };

            await repo.AddAsync(dto);
            var id = dto.SessioneId;

            await repo.DeleteAsync(id);

            var exists = await repo.ExistsAsync(id);
            Assert.False(exists);
        }

        [Fact]
        public async Task GetScaduteAsync_Should_Return_Expired_Sessioni()
        {
            using var context = GetInMemoryContext();
            var repo = new SessioniQrRepository(context);

            await repo.AddAsync(new SessioniQrDTO
            {
                ClienteId = 5,
                QrCode = "EXPIRED",
                DataScadenza = DateTime.UtcNow.AddDays(-1),
                Utilizzato = false
            });

            var scadute = await repo.GetScaduteAsync();
            Assert.Single(scadute);
            Assert.Equal("EXPIRED", scadute.First().QrCode);
        }

        [Fact]
        public async Task GetNonutilizzateAsync_Should_Return_Unused()
        {
            using var context = GetInMemoryContext();
            var repo = new SessioniQrRepository(context);

            await repo.AddAsync(new SessioniQrDTO
            {
                ClienteId = 6,
                QrCode = "NOT_USED",
                DataScadenza = DateTime.UtcNow.AddDays(1),
                Utilizzato = false
            });

            var nonUsate = await repo.GetNonutilizzateAsync();
            Assert.Single(nonUsate);
            Assert.Equal("NOT_USED", nonUsate.First().QrCode);
        }
    }
}
