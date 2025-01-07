using AMLSystem.DAL.Models;
using AMLSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AMLSystem.Controllers;

[ApiController]
[Route("media")]
public class MediaController : ControllerBase
{
    private readonly MediaService _mediaService;

    public MediaController(MediaService mediaService)
    {
        _mediaService = mediaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var mediaItems = await _mediaService.GetAllMediaAsync();
            if (!mediaItems.Any())
            {
                return Ok(new { Message = "No media items found." });
            }
            return Ok(mediaItems);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new { Message = "Invalid media ID" });
        }

        try
        {
            var mediaItem = await _mediaService.GetMediaByIdAsync(id);
            if (mediaItem is null)
            {
                return NotFound(new { Message = "Media item not found" });
            }
            return Ok(mediaItem);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] MediaItem mediaItem)
    {
        if (id <= 0 || mediaItem is null)
        {
            return BadRequest(new { Message = "Invalid input data" });
        }

        if (id != mediaItem.Id)
        {
            return BadRequest(new { Message = "ID in route does not match ID in body" });
        }

        try
        {
            await _mediaService.UpdateMediaAsync(mediaItem);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string query, [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        if (string.IsNullOrEmpty(query))
        {
            return BadRequest(new { Message = "Search query cannot be empty" });
        }

        if (page <= 0 || pageSize <= 0)
        {
            return BadRequest(new { Message = "Page and pageSize must be greater than zero" });
        }

        try
        {
            var mediaItems = await _mediaService.SearchMediaAsync(query, page, pageSize);
            if (!mediaItems.Any())
            {
                return Ok(new { Message = "No media items match the search criteria." });
            }
            return Ok(mediaItems);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("borrowed")]
    public async Task<IActionResult> GetBorrowedMedia()
    {
        try
        {
            var borrowedItems = await _mediaService.GetBorrowedMediaAsync();
            if (!borrowedItems.Any())
            {
                return Ok(new { Message = "No borrowed media items." });
            }
            return Ok(borrowedItems);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("borrow/{id}")]
    public async Task<IActionResult> BorrowMedia(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new { Message = "Invalid media ID" });
        }

        try
        {
            await _mediaService.BorrowMediaAsync(id);
            return Ok(new { Message = "Media item borrowed successfully" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return NotFound(new { Error = ex.Message });
        }
    }

    [HttpPost("return/{id}")]
    public async Task<IActionResult> ReturnMedia(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new { Message = "Invalid media ID" });
        }

        try
        {
            await _mediaService.ReturnMediaAsync(id);
            return Ok(new { Message = "Media item returned successfully" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return NotFound(new { Error = ex.Message });
        }
    }
}