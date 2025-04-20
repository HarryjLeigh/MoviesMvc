using System.ComponentModel.DataAnnotations;

namespace Recipes.Models;

public class Rating
{
    public int Id { get; set; }

    [StringLength(60, MinimumLength = 3)]
    [Required]
    public string? User { get; set; }

    [DataType(DataType.Date)] public DateTime Date { get; set; }

    [Required] public string? Recipe { get; set; }

    [Display(Name = "Rating(0-5)")]
    [Range(0, 5)]
    [DataType(DataType.Currency)]
    public double DishRating { get; set; }

    [Display(Name = "Review Text")]
    [Required]
    public string? ReviewText { get; set; }

    [Display(Name = "Changes Made")]
    [Required]
    public string? ChangesMade { get; set; }
}