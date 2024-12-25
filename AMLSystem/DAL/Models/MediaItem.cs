using AMLSystem.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace AMLSystem.DAL.Models;

public class MediaItem
{
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string Author { get; set; } = string.Empty;

    [Required]
    public string BookCode { get; set; } = string.Empty;

    public DateTime? IssueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public Genre Genre { get; set; }
    public bool IsBorrowed { get; set; } = false;
}