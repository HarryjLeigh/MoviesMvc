using Microsoft.AspNetCore.Mvc.Rendering;

namespace Recipes.Models;

public class RatingViewModel
{
    public List<Rating>? Ratings { get; set; }
    public SelectList? Users { get; set; }
    public SelectList? Recipes { get; set; }
    public string? User { get; set; }
    public string? Recipe { get; set; }
}