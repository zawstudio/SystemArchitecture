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
    public async Task<ActionResult<IEnumerable<MediaItem>>> GetAll()
    {
        var mediaItems = await _mediaService.GetAllMediaAsync();
        return Ok(mediaItems);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MediaItem>> GetById(int id)
    {
        var mediaItem = await _mediaService.GetMediaByIdAsync(id);
        if (mediaItem == null) return NotFound();
        return mediaItem;
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
        if (id != mediaItem.Id) return BadRequest();
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

    [HttpPost("borrow")]
    public async Task<IActionResult> Borrow([FromQuery] int mediaId)
    {
        try
        {
            await _mediaService.BorrowMediaAsync(mediaId);
            return Ok(new { Message = "Media item borrowed successfully." });
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

    [HttpPost("return")]
    public async Task<IActionResult> Return([FromQuery] int mediaId)
    {
        try
        {
            await _mediaService.ReturnMediaAsync(mediaId);
            return Ok(new { Message = "Media item returned successfully." });
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

    [HttpGet("borrowed")]
    public async Task<IActionResult> ListBorrowedItems()
    {
        var borrowedMediaItems = await _mediaService.GetBorrowedMediaAsync();
        return Ok(borrowedMediaItems);
    }
}