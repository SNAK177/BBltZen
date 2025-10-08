using Database;
using DTO;
using Microsoft.EntityFrameworkCore;
using Repository.Service;

namespace RepositoryTest;

public class CategorieIngredientiRepositoryTests
{
    private BubbleTeaContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<BubbleTeaContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new BubbleTeaContext(options);
    }

    [Fact]
    public async Task AddAsync_ShouldAddCategory()
    {
        using var context = GetInMemoryContext();
        var repo = new CategorieIngredientiRepository(context);

        var dto = new CategorieIngredientiDTO { Categoria = "Frutta" };

        var result = await repo.AddAsync(dto);

        Assert.NotNull(result);
        Assert.True(result.CategoriaId > 0);
        Assert.Equal("Frutta", result.Categoria);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllCategories()
    {
        using var context = GetInMemoryContext();
        var repo = new CategorieIngredientiRepository(context);

        await repo.AddAsync(new CategorieIngredientiDTO { Categoria = "Topping" });
        await repo.AddAsync(new CategorieIngredientiDTO { Categoria = "Base" });

        var result = await repo.GetAllAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectCategory()
    {
        using var context = GetInMemoryContext();
        var repo = new CategorieIngredientiRepository(context);

        var created = await repo.AddAsync(
            new CategorieIngredientiDTO { Categoria = "Dolcificante" }
        );

        var result = await repo.GetByIdAsync(created.CategoriaId);

        Assert.NotNull(result);
        Assert.Equal("Dolcificante", result.Categoria);
    }

    [Fact]
    public async Task UpdateAsync_ShouldModifyCategory()
    {
        using var context = GetInMemoryContext();
        var repo = new CategorieIngredientiRepository(context);

        var created = await repo.AddAsync(new CategorieIngredientiDTO { Categoria = "Sciroppo" });
        created.Categoria = "Sciroppo alla frutta";

        await repo.UpdateAsync(created);

        var updated = await repo.GetByIdAsync(created.CategoriaId);
        Assert.Equal("Sciroppo alla frutta", updated.Categoria);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveCategory()
    {
        using var context = GetInMemoryContext();
        var repo = new CategorieIngredientiRepository(context);

        var created = await repo.AddAsync(new CategorieIngredientiDTO { Categoria = "Granella" });
        await repo.DeleteAsync(created.CategoriaId);

        var deleted = await repo.GetByIdAsync(created.CategoriaId);
        Assert.Null(deleted);
    }
}
