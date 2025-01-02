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

    public async Task UpdateAsync(MediaItem mediaItem)
    {
        _context.Set<MediaItem>().Update(mediaItem);
    }
}