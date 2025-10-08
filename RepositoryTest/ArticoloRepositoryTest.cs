using Database;
using DTO;
using Microsoft.EntityFrameworkCore;
using Repository.Service;

namespace RepositoryTest;

public class ArticoloRepositoryTest
{
     public class ArticoloRepositoryTests
    {
        private BubbleTeaContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BubbleTeaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new BubbleTeaContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldAddArticolo()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var repo = new ArticoloRepository(context);

            var dto = new ArticoloDTO
            {
                Tipo = "Bubble Tea",
                DataCreazione = DateTime.UtcNow
            };

            // Act
            var result = await repo.AddAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Bubble Tea", result.Tipo);
            Assert.True(result.ArticoloId > 0);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnArticolo()
        {
            using var context = GetInMemoryContext();
            var repo = new ArticoloRepository(context);

            var created = await repo.AddAsync(new ArticoloDTO { Tipo = "Tè Verde" });

            var fetched = await repo.GetByIdAsync(created.ArticoloId);

            Assert.NotNull(fetched);
            Assert.Equal("Tè Verde", fetched.Tipo);
        }

        [Fact]
        public async Task UpdateAsync_ShouldModifyArticolo()
        {
            using var context = GetInMemoryContext();
            var repo = new ArticoloRepository(context);

            var created = await repo.AddAsync(new ArticoloDTO { Tipo = "Tè Nero" });
            created.Tipo = "Tè Nero Premium";

            var updated = await repo.UpdateAsync(created);

            Assert.True(updated);
            var fetched = await repo.GetByIdAsync(created.ArticoloId);
            Assert.Equal("Tè Nero Premium", fetched.Tipo);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveArticolo()
        {
            using var context = GetInMemoryContext();
            var repo = new ArticoloRepository(context);

            var created = await repo.AddAsync(new ArticoloDTO { Tipo = "Oolong" });
            var deleted = await repo.DeleteAsync(created.ArticoloId);

            Assert.True(deleted);
            var fetched = await repo.GetByIdAsync(created.ArticoloId);
            Assert.Null(fetched);
        }
    }
}
