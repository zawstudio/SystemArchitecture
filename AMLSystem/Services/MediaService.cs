using AMLSystem.DAL.Interfaces;
using AMLSystem.DAL.Models;

namespace AMLSystem.Services;

public class MediaService
{
    private readonly IMediaRepository _mediaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MediaService(IMediaRepository mediaRepository, IUnitOfWork unitOfWork)
    {
        _mediaRepository = mediaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<MediaItem>> GetAllMediaAsync()
    {
        return await _mediaRepository.GetAllAsync();
    }

    public async Task<MediaItem?> GetMediaByIdAsync(int id)
    {
        return await _mediaRepository.GetByIdAsync(id);
    }

    public async Task AddMediaAsync(MediaItem mediaItem)
    {
        await _mediaRepository.AddAsync(mediaItem);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateMediaAsync(MediaItem mediaItem)
    {
        await _mediaRepository.UpdateAsync(mediaItem);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteMediaAsync(int id)
    {
        await _mediaRepository.DeleteAsync(id);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<MediaItem>> SearchMediaAsync(string query, int page, int pageSize)
    {
        var allMedia = await _mediaRepository.GetAllAsync();
        return allMedia
            .Where(m => m.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                        m.Author.Contains(query, StringComparison.OrdinalIgnoreCase))
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }

    public async Task<IEnumerable<MediaItem>> GetBorrowedMediaAsync()
    {
        var allMedia = await _mediaRepository.GetAllAsync();
        return allMedia.Where(m => m.IsBorrowed);
    }

    public async Task BorrowMediaAsync(int mediaId)
    {
        var mediaItem = await _mediaRepository.GetByIdAsync(mediaId);
        if (mediaItem == null) throw new Exception("Media item not found.");
        if (mediaItem.IsBorrowed) throw new InvalidOperationException("Media item is already borrowed.");

        mediaItem.IsBorrowed = true;
        mediaItem.IssueDate = DateTime.UtcNow;
        await _mediaRepository.UpdateAsync(mediaItem);
        await _unitOfWork.SaveAsync();
    }

    public async Task ReturnMediaAsync(int mediaId)
    {
        var mediaItem = await _mediaRepository.GetByIdAsync(mediaId);
        if (mediaItem == null) throw new Exception("Media item not found.");
        if (!mediaItem.IsBorrowed) throw new InvalidOperationException("Media item is not currently borrowed.");

        mediaItem.IsBorrowed = false;
        mediaItem.ReturnDate = DateTime.UtcNow;
        await _mediaRepository.UpdateAsync(mediaItem);
        await _unitOfWork.SaveAsync();
    }
}
