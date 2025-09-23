using Database;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Service;

namespace RepositoryTest
{

    public class StatoPagamentoRepositoryTest
    {
        private BubbleTeaContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BubbleTeaContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new BubbleTeaContext(options);
        }
         [Fact]
        public async Task AddAsync_Should_Save_Entity()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var repository = new StatoPagamentoRepository(context);
            var dto = new StatoPagamentoDTO { StatoPagamento1 = "Pagato" };

            // Act
            await repository.AddAsync(dto);
            var result = await repository.GetByIdAsync(dto.StatoPagamentoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Pagato", result.StatoPagamento1);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_Entities()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var repository = new StatoPagamentoRepository(context);

            await repository.AddAsync(new StatoPagamentoDTO { StatoPagamento1 = "In Attesa" });
            await repository.AddAsync(new StatoPagamentoDTO { StatoPagamento1 = "Pagato" });

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateAsync_Should_Modify_Entity()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var repository = new StatoPagamentoRepository(context);
            var dto = new StatoPagamentoDTO { StatoPagamento1 = "In Attesa" };
            await repository.AddAsync(dto);

            // Act
            dto.StatoPagamento1 = "Confermato";
            await repository.UpdateAsync(dto);
            var result = await repository.GetByIdAsync(dto.StatoPagamentoId);

            // Assert
            Assert.Equal("Confermato", result.StatoPagamento1);
        }

        [Fact]
        public async Task DeleteAsync_Should_Remove_Entity()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var repository = new StatoPagamentoRepository(context);
            var dto = new StatoPagamentoDTO { StatoPagamento1 = "Annullato" };
            await repository.AddAsync(dto);

            // Act
            await repository.DeleteAsync(dto.StatoPagamentoId);
            var result = await repository.GetByIdAsync(dto.StatoPagamentoId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ExistsAsync_Should_Return_True_If_Entity_Exists()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var repository = new StatoPagamentoRepository(context);
            var dto = new StatoPagamentoDTO { StatoPagamento1 = "In Elaborazione" };
            await repository.AddAsync(dto);

            // Act
            var exists = await repository.ExistsAsync(dto.StatoPagamentoId);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task ExistsAsync_Should_Return_False_If_Entity_Not_Found()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var repository = new StatoPagamentoRepository(context);

            // Act
            var exists = await repository.ExistsAsync(999);

            // Assert
            Assert.False(exists);
        }
        
    }
}