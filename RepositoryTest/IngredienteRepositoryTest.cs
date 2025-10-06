using Database;
using DTO;
using Microsoft.EntityFrameworkCore;
using Repository.Service;

namespace RepositoryTest;

public class IngredienteRepositoryTest
{
    private BubbleTeaContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<BubbleTeaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new BubbleTeaContext(options);
    }
    [Fact]
    public async Task AddAsync_ShouldAddIngredient()
    {
        using var context = GetInMemoryContext();
        var repository = new IngredienteRepository(context);
        var dto = new IngredienteDTO { Ingrediente1 = "Ingrediente aggiunto" };
        await repository.AddAsync(dto);
        var result= await repository.GetByIdAsync(dto.IngredienteId);
        Assert.NotNull(result);
        Assert.Equal("Ingrediente aggiunto", result.Ingrediente1);
    }
}