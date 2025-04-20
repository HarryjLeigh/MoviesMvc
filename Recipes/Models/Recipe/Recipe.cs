using System.ComponentModel.DataAnnotations;
using Recipes.Validation;

namespace Recipes.Models;

public class Recipe
{
    public int Id { get; set; }

    [StringLength(100)] [Required] public string? Name { get; set; }

    [StringLength(30)] [Required] public string? Cuisine { get; set; }

    [Display(Name = "Dish Type")]
    [StringLength(50)]
    [Required]
    public string? DishType { get; set; }

    [Display(Name = "Is Vegetarian? (y/n)")]
    [StringLength(10)]
    [Required]
    [AllowedWordsOnly("y", "n")]
    public string? IsVegetarian { get; set; }

    [Display(Name = "Prep Time (mins)")]
    [Range(1, 60)]
    public int PrepTime { get; set; }

    [Display(Name = "Cook Time (mins)")]
    [Range(0, 240)]
    public int CookTime { get; set; }

    [Display(Name = "Total Time (mins)")]
    [Range(1, 500)]
    public int TotalTime { get; set; }

    [StringLength(30)]
    [Required]
    [AllowedWordsOnly("easy", "medium", "hard")]
    [Display(Name = "Difficulty(easy/medium/hard)")]
    public string? Difficulty { get; set; }

    [Range(0, 5)]
    [Display(Name = "Avg Rating (0-5)")]
    public double AverageRating { get; set; }
}