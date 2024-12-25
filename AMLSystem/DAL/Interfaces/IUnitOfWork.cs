namespace AMLSystem.DAL.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task SaveAsync();
}