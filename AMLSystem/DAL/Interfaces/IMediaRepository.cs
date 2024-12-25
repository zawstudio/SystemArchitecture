using AMLSystem.DAL.Models;

namespace AMLSystem.DAL.Interfaces;

public interface IMediaRepository
{
    Task<IEnumerable<MediaItem>> GetAllAsync();
    Task<MediaItem?> GetByIdAsync(int id);
    Task AddAsync(MediaItem mediaItem);
    Task UpdateAsync(MediaItem mediaItem);
    Task DeleteAsync(int id);
}