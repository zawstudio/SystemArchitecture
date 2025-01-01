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
        var mediaItems = await _mediaService.GetAllMediaAsync();
        return Ok(mediaItems);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var mediaItem = await _mediaService.GetMediaByIdAsync(id);
        if (mediaItem is null)
        {
            return NotFound(new { Message = "Media item not found" });
        }

        return Ok(mediaItem);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MediaItem mediaItem)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _mediaService.AddMediaAsync(mediaItem);
        return CreatedAtAction(nameof(GetById), new { id = mediaItem.Id }, mediaItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] MediaItem mediaItem)
    {
        if (id != mediaItem.Id)
        {
            return BadRequest(new { Message = "ID in route does not match ID in body" });
        }

        await _mediaService.UpdateMediaAsync(mediaItem);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediaService.DeleteMediaAsync(id);
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string query, [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var mediaItems = await _mediaService.SearchMediaAsync(query, page, pageSize);
        return Ok(mediaItems);
    }

    [HttpGet("borrowed")]
    public async Task<IActionResult> GetBorrowedMedia()
    {
        var borrowedItems = await _mediaService.GetBorrowedMediaAsync();
        return Ok(borrowedItems);
    }

    [HttpPost("borrow/{id}")]
    public async Task<IActionResult> BorrowMedia(int id)
    {
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