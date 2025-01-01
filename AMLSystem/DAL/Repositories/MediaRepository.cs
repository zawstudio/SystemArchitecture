using AMLSystem.DAL.Interfaces;
using AMLSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AMLSystem.DAL.Repositories;

public class MediaRepository : IMediaRepository
{
    private readonly AmlContext _context;

    public MediaRepository(AmlContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MediaItem>> GetAllAsync()
    {
        return await _context.Set<MediaItem>().ToListAsync();
    }

    public async Task<MediaItem?> GetByIdAsync(int id)
    {
        return await _context.Set<MediaItem>().FindAsync(id);
    }

    public async Task AddAsync(MediaItem mediaItem)
    {
        await _context.Set<MediaItem>().AddAsync(mediaItem);
    }

    public async Task UpdateAsync(MediaItem mediaItem)
    {
        _context.Set<MediaItem>().Update(mediaItem);
    }

    public async Task DeleteAsync(int id)
    {
        var mediaItem = await GetByIdAsync(id);
        if (mediaItem is not null)
        {
            _context.Set<MediaItem>().Remove(mediaItem);
        }
    }
}