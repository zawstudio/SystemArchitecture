using AMLSystem.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace AMLSystem.DAL.Models;

public class MediaItem
{
   [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Author { get; set; } = string.Empty;

    [Required]
    public string BookCode { get; set; } = string.Empty;

    public DateTime? IssueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public Genre Genre { get; set; }

    public bool IsBorrowed { get; set; } = false;
    
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}