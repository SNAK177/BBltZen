using Database;
using DTO;
using Microsoft.EntityFrameworkCore;
using Repository.Service;

namespace RepositoryTest
{ 
    public class NotificheOperativeRepositoryTest
    {
        private BubbleTeaContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BubbleTeaContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // DB unico per ogni test
                .Options;

            return new BubbleTeaContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldAddNotifica()
        {
            var context = GetInMemoryContext();
            var repo = new NotificheOperativeRepository(context);

            var dto = new NotificheOperativeDTO
            {
                Messaggio = "Test",
                Stato = "Pendente",
                Priorita = 1,
                OrdiniCoinvolti = "ORD123"
            };
            await repo.AddAsync(dto);
            var notifica = await context.NotificheOperative.FirstOrDefaultAsync();
            Assert.NotNull(notifica);
            Assert.Equal("Test", notifica.Messaggio);
            Assert.Equal("Pendente", notifica.Stato);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNotifiche()
        {
            var context = GetInMemoryContext();
            context.NotificheOperative.Add(new NotificheOperative
            {
                NotificaId = 1,
                Messaggio = "Notifica 1",
                Stato = "Pendente",
                Priorita = 2,
                DataCreazione = DateTime.Now,
                OrdiniCoinvolti = "ORD001"
            });
            await context.SaveChangesAsync();
            var repo = new NotificheOperativeRepository(context);
            var result = await repo.GetByIdAsync(1);
            Assert.NotNull(result);
            Assert.Equal("Notifica 1", result.Messaggio);
        }
        [Fact]
        public async Task UpdateAsync_ShouldUpdateNotifica()
        {
           var context = GetInMemoryContext();
           context.NotificheOperative.Add(new NotificheOperative
            {
                NotificaId = 1,
                Messaggio = "Vecchio messaggio",
                Stato = "Pendente",
                Priorita = 1,
                DataCreazione = DateTime.Now,
                OrdiniCoinvolti = "ORD001"
            });
            await context.SaveChangesAsync();
            var repo = new NotificheOperativeRepository(context);
            var dto = new NotificheOperativeDTO
            {
                NotificaId = 1,
                Messaggio = "Nuovo messaggio",
                Stato = "Gestita",
                Priorita = 2,
                OrdiniCoinvolti = "ORD001"
            };
            await repo.UpdateAsync(dto);
            var update = await context.NotificheOperative.FindAsync(1);
            Assert.NotNull(update);
            Assert.Equal("Nuovo messaggio", update.Messaggio);
            Assert.Equal("Gestita", update.Stato);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveNotifica()
        {
            var context = GetInMemoryContext();
            context.NotificheOperative.Add(new NotificheOperative
            {
                NotificaId = 1,
                Messaggio = "Da cancellare",
                Stato = "Pendente",
                Priorita = 1,
                DataCreazione = DateTime.Now,
                OrdiniCoinvolti = "ORD999"
            });
            await context.SaveChangesAsync();
            var repo = new NotificheOperativeRepository(context);
            await repo.DeleteAsync(1);
            var notifica = await context.NotificheOperative.FirstOrDefaultAsync();
            Assert.Null(notifica);
        }
    }
}