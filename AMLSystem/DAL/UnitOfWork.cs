using AMLSystem.DAL.Interfaces;

namespace AMLSystem.DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly AmlContext _context;

    public UnitOfWork(AmlContext context)
    {
        _context = context;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}