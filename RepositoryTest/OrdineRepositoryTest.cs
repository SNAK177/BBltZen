using Moq;
using Repository.Service;
using DTO;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Repository.Tests
{
    public class OrdineRepositoryTests
    {
        private readonly Mock<BubbleTeaContext> _mockContext;
        private readonly OrdineRepository _repository;
        private readonly List<Ordine> _mockOrdini;

        public OrdineRepositoryTests()
        {
            _mockContext = new Mock<BubbleTeaContext>();
            _repository = new OrdineRepository(_mockContext.Object);

            _mockOrdini = new List<Ordine>
            {
                new Ordine
                {
                    OrdineId = 1,
                    ClienteId = 1,
                    DataCreazione = DateTime.Now.AddDays(-2),
                    DataAggiornamento = DateTime.Now.AddDays(-1),
                    StatoOrdineId = 1,
                    StatoPagamentoId = 1,
                    Totale = 25.50m,
                    Priorita = 1
                },
                new Ordine
                {
                    OrdineId = 2,
                    ClienteId = 2,
                    DataCreazione = DateTime.Now.AddDays(-1),
                    DataAggiornamento = DateTime.Now,
                    StatoOrdineId = 2,
                    StatoPagamentoId = 2,
                    Totale = 18.75m,
                    Priorita = 2
                }
            };
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllOrdini()
        {
            // Arrange
            _mockContext.Setup(x => x.Ordine)
                        .ReturnsDbSetAsync(_mockOrdini); // Use ReturnsDbSetAsync instead

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        private object GetMockSet()
        {
            return _mockOrdini.AsQueryable().BuildMockDbSet();
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ShouldReturnOrdine()
        {
            // Arrange
            _mockContext.Setup(x => x.Ordine)
                        .Returns(CreateMockDbSet(_mockOrdini));

            var expectedId = 1;

            // Act
            var result = await _repository.GetByIdAsync(expectedId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedId, result.OrdineId);
            Assert.Equal(1, result.ClienteId);
        }

        [Fact]
        public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            _mockContext.Setup(x => x.Ordine)
                        .Returns(CreateMockDbSet(_mockOrdini));

            var invalidId = 999;

            // Act
            var result = await _repository.GetByIdAsync(invalidId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_WithValidEntity_ShouldAddAndReturnOrdine()
        {
            // Arrange
            var newOrdineDto = new OrdineDTO
            {
                ClienteId = 3,
                DataCreazione = DateTime.Now,
                DataAggiornamento = DateTime.Now,
                StatoOrdineId = 1,
                StatoPagamentoId = 1,
                Totale = 30.00m,
                Priorita = 1
            };

            var mockSet = new Mock<DbSet<Ordine>>();
            var addedOrdine = (Ordine)null;

            mockSet.Setup(x => x.AddAsync(It.IsAny<Ordine>(), It.IsAny<CancellationToken>()))
                   .Callback<Ordine, CancellationToken>((o, ct) =>
                   {
                       o.OrdineId = _mockOrdini.Max(x => x.OrdineId) + 1; // Set the ID
                       addedOrdine = o;
                   })
                   .Returns(ValueTask.FromResult((Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Ordine>)null));

            _mockContext.Setup(x => x.Ordine).Returns(mockSet.Object);
            _mockContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(1);

            // Act
            var result = await _repository.AddAsync(newOrdineDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.OrdineId > 0);
            Assert.Equal(_mockOrdini.Max(x => x.OrdineId) + 1, result.OrdineId);
            mockSet.Verify(x => x.AddAsync(It.IsAny<Ordine>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddAsync_WithNullEntity_ShouldThrowArgumentNullException()
        {
            // Arrange
            OrdineDTO nullOrdine = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _repository.AddAsync(nullOrdine));
        }

        [Fact]
        public async Task UpdateAsync_WithValidEntity_ShouldUpdateOrdine()
        {
            // Arrange
            var existingOrdine = _mockOrdini.First();
            var mockSet = new Mock<DbSet<Ordine>>();

            _mockContext.Setup(x => x.Ordine).Returns(mockSet.Object);
            _mockContext.Setup(x => x.Ordine.FindAsync(It.IsAny<int>()))
                        .ReturnsAsync(existingOrdine);
            _mockContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(1);

            var updatedOrdineDto = new OrdineDTO
            {
                OrdineId = 1,
                ClienteId = 1,
                StatoOrdineId = 3, // Updated
                StatoPagamentoId = 2, // Updated
                Totale = 28.00m, // Updated
                Priorita = 2, // Updated
                DataCreazione = existingOrdine.DataCreazione,
                DataAggiornamento = DateTime.Now
            };

            // Act
            await _repository.UpdateAsync(updatedOrdineDto);

            // Assert
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(3, existingOrdine.StatoOrdineId);
            Assert.Equal(2, existingOrdine.StatoPagamentoId);
            Assert.Equal(28.00m, existingOrdine.Totale);
            Assert.Equal(2, existingOrdine.Priorita);
        }

        [Fact]
        public async Task UpdateAsync_WithNullEntity_ShouldThrowArgumentException()
        {
            // Arrange
            OrdineDTO nullOrdine = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _repository.UpdateAsync(nullOrdine));
        }

        [Fact]
        public async Task UpdateAsync_WithNonExistingEntity_ShouldThrowInvalidOperationException()
        {
            // Arrange
            _mockContext.Setup(x => x.Ordine.FindAsync(It.IsAny<int>()))
                        .ReturnsAsync((Ordine)null);

            var nonExistingOrdine = new OrdineDTO
            {
                OrdineId = 999,
                ClienteId = 1,
                StatoOrdineId = 1,
                StatoPagamentoId = 1,
                Totale = 25.50m,
                Priorita = 1
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _repository.UpdateAsync(nonExistingOrdine));
        }

        [Fact]
        public async Task DeleteAsync_WithValidId_ShouldRemoveOrdine()
        {
            // Arrange
            var existingOrdine = _mockOrdini.First();
            var mockSet = new Mock<DbSet<Ordine>>();

            _mockContext.Setup(x => x.Ordine).Returns(mockSet.Object);
            _mockContext.Setup(x => x.Ordine.FindAsync(It.IsAny<int>()))
                        .ReturnsAsync(existingOrdine);
            _mockContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(1);

            // Act
            await _repository.DeleteAsync(1);

            // Assert
            mockSet.Verify(x => x.Remove(It.IsAny<Ordine>()), Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WithInvalidId_ShouldNotThrowException()
        {
            // Arrange
            _mockContext.Setup(x => x.Ordine.FindAsync(It.IsAny<int>()))
                        .ReturnsAsync((Ordine)null);

            // Act
            await _repository.DeleteAsync(999);

            // Assert - No exception should be thrown
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        private static DbSet<Ordine> CreateMockDbSet(IEnumerable<Ordine> sourceList)
        {
            var queryable = sourceList.AsQueryable();
            var mockSet = new Mock<DbSet<Ordine>>();

            mockSet.As<IQueryable<Ordine>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<Ordine>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<Ordine>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<Ordine>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            return mockSet.Object;
        }
    }
}