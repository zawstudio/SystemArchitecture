using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMLSystem.DAL.Models;

public class BorrowedMediaItem
{
    [ForeignKey("MediaItem")] 
    public int MediaItemId { get; set; }
    public MediaItem MediaItem { get; set; }

    public DateTime BorrowedDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
}