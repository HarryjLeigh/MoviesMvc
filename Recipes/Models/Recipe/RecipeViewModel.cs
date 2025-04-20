using Microsoft.AspNetCore.Mvc.Rendering;

namespace Recipes.Models;

public class RecipeViewModel
{
    public List<Recipe>? Recipes { get; set; }
    public SelectList? Cuisines { get; set; }
    public SelectList? IsVegetarian { get; set; }
    public SelectList? Difficulties { get; set; }
    public SelectList? DishTypes { get; set; }

    public string? RecipeCuisine { get; set; }
    public string? RecipeVegetarian { get; set; }
    public string? RecipeDifficulty { get; set; }
    public string? RecipeDishType { get; set; }
    public string? SearchString { get; set; }
}