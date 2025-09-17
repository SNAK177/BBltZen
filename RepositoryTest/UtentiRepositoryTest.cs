using Database;
using DTO;
using Microsoft.EntityFrameworkCore;
using Repository.Service;

namespace RepositoryTest
{
    public class UtentiRepositoryTest
    {
        private BubbleTeaContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<BubbleTeaContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // DB unico per ogni test
                .Options;
            return new BubbleTeaContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldAddUser()
        {
            var context = GetInMemoryDb();
            var repository = new UtentiRepository(context);

            var utente = new UtentiDTO
            {
                Email = "test@example.com",
                PasswordHash = "hash",
                TipoUtente = "Cliente",
                Attivo = true,
                DataCreazione = DateTime.UtcNow
            };

            await repository.AddAsync(utente);

            var result = await context.Utenti.FirstOrDefaultAsync(u => u.Email == "test@example.com");

            Assert.NotNull(result);
            Assert.Equal(utente.Email, result.Email);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser()
        {
            var context = GetInMemoryDb();
            var repository = new UtentiRepository(context);

            var utente = new Database.Utenti
            {
                Email = "test2@example.com",
                PasswordHash = "hash2",
                TipoUtente = "Cliente",
                Attivo = true,
                DataCreazione = DateTime.UtcNow
            };
            context.Utenti.Add(utente);
            await context.SaveChangesAsync();

            var result = await repository.GetByIdAsync(utente.UtenteId);

            Assert.NotNull(result);
            Assert.Equal("test2@example.com", result.Email);
        }

        [Fact]
        public async Task UpdateAsync_ShouldModifyUser()
        {
            var context = GetInMemoryDb();
            var repository = new UtentiRepository(context);

            var utente = new Database.Utenti
            {
                Email = "update@example.com",
                PasswordHash = "oldhash",
                TipoUtente = "Cliente",
                Attivo = true,
                DataCreazione = DateTime.UtcNow
            };
            context.Utenti.Add(utente);
            await context.SaveChangesAsync();

            var dto = new UtentiDTO
            {
                UtenteId = utente.UtenteId,
                Email = "update@example.com",
                PasswordHash = "newhash",
                TipoUtente = "Cliente",
                Attivo = true,
                DataCreazione = utente.DataCreazione,
                DataAggiornamento = DateTime.UtcNow
            };

            await repository.UpdateAsync(dto);

            var updated = await context.Utenti.FindAsync(utente.UtenteId);
            Assert.Equal("newhash", updated.PasswordHash);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveUser()
        {
            var context = GetInMemoryDb();
            var repository = new UtentiRepository(context);

            var utente = new Database.Utenti
            {
                Email = "delete@example.com",
                PasswordHash = "hash",
                TipoUtente = "Cliente",
                Attivo = true,
                DataCreazione = DateTime.UtcNow
            };
            context.Utenti.Add(utente);
            await context.SaveChangesAsync();

            await repository.DeleteAsync(utente.UtenteId);

            var deleted = await context.Utenti.FindAsync(utente.UtenteId);
            Assert.Null(deleted);
        }
    }
}
