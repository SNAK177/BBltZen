using Database;
using Microsoft.EntityFrameworkCore;

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
    
}