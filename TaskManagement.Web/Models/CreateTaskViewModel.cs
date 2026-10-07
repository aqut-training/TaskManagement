using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Web.Models;

public class CreateTaskViewModel
{
    [Required(ErrorMessage = "Please enter a title.")]
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
    public string Title { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string? Description { get; set; }
}
