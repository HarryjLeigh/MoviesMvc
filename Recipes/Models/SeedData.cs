using Microsoft.EntityFrameworkCore;
using Recipes.Data;

namespace Recipes.Models;

public static class SeedData
{
    public static void InitializeRecipes(IServiceProvider serviceProvider)
    {
        using (var context = new RecipesContext(
                   serviceProvider.GetRequiredService<DbContextOptions<RecipesContext>>()))
        {
            if (context.Recipe.Any()) return;
            context.Recipe.AddRange(
                new Recipe
                {
                    Name = "Spaghetti Carbonara",
                    Cuisine = "Italian",
                    DishType = "Pasta",
                    IsVegetarian = "n",
                    PrepTime = 10,
                    CookTime = 20,
                    TotalTime = 30,
                    Difficulty = "Medium",
                    AverageRating = 4.5
                },
                new Recipe
                {
                    Name = "Margherita Pizza",
                    Cuisine = "Italian",
                    DishType = "Pizza",
                    IsVegetarian = "y",
                    PrepTime = 15,
                    CookTime = 15,
                    TotalTime = 30,
                    Difficulty = "Easy",
                    AverageRating = 4.7
                },
                new Recipe
                {
                    Name = "Chicken Tikka Masala",
                    Cuisine = "Indian",
                    DishType = "Curry",
                    IsVegetarian = "n",
                    PrepTime = 20,
                    CookTime = 40,
                    TotalTime = 60,
                    Difficulty = "Hard",
                    AverageRating = 4.8
                },
                new Recipe
                {
                    Name = "Vegetable Stir Fry",
                    Cuisine = "Chinese",
                    DishType = "Stir Fry",
                    IsVegetarian = "y",
                    PrepTime = 15,
                    CookTime = 10,
                    TotalTime = 25,
                    Difficulty = "Easy",
                    AverageRating = 4.3
                },
                new Recipe
                {
                    Name = "Beef Tacos",
                    Cuisine = "Mexican",
                    DishType = "Tacos",
                    IsVegetarian = "n",
                    PrepTime = 15,
                    CookTime = 10,
                    TotalTime = 25,
                    Difficulty = "Easy",
                    AverageRating = 4.2
                },
                new Recipe
                {
                    Name = "Mushroom Risotto",
                    Cuisine = "Italian",
                    DishType = "Rice",
                    IsVegetarian = "y",
                    PrepTime = 20,
                    CookTime = 30,
                    TotalTime = 50,
                    Difficulty = "Medium",
                    AverageRating = 4.6
                },
                new Recipe
                {
                    Name = "Grilled Salmon",
                    Cuisine = "Seafood",
                    DishType = "Main Course",
                    IsVegetarian = "n",
                    PrepTime = 15,
                    CookTime = 20,
                    TotalTime = 35,
                    Difficulty = "Medium",
                    AverageRating = 4.4
                },
                new Recipe
                {
                    Name = "Chocolate Chip Cookies",
                    Cuisine = "American",
                    DishType = "Dessert",
                    IsVegetarian = "y",
                    PrepTime = 10,
                    CookTime = 12,
                    TotalTime = 22,
                    Difficulty = "Easy",
                    AverageRating = 4.9
                }
            );
            context.SaveChanges();
        }
    }

    public static void InitializeRatings(IServiceProvider serviceProvider)
    {
        using (var context = new RatingsContext(
                   serviceProvider.GetRequiredService<DbContextOptions<RatingsContext>>()))
        {
            if (context.Ratings.Any()) return;
            context.Ratings.AddRange(
                new Rating
                {
                    User = "Bob123",
                    Date = DateTime.Now,
                    Recipe = "Grandma's cookies",
                    DishRating = 4.5,
                    ReviewText = "Delicious",
                    ChangesMade = "None"
                },
                new Rating
                {
                    User = "AliceW",
                    Date = DateTime.Now.AddDays(-5),
                    Recipe = "Spaghetti Carbonara",
                    DishRating = 5.0,
                    ReviewText = "Perfectly creamy and rich. Added extra pancetta.",
                    ChangesMade = "Increased pancetta from 50g to 75g"
                },
                new Rating
                {
                    User = "ChefMike",
                    Date = DateTime.Now.AddDays(-10),
                    Recipe = "Vegetable Stir Fry",
                    DishRating = 3.8,
                    ReviewText = "Quick and tasty, but a bit too salty for my taste.",
                    ChangesMade = "Reduced soy sauce by half"
                },
                new Rating
                {
                    User = "SunnyDay",
                    Date = DateTime.Now.AddDays(-1),
                    Recipe = "Mushroom Risotto",
                    DishRating = 4.2,
                    ReviewText = "Loved the earthiness—next time I'll use arborio rice.",
                    ChangesMade = "Swapped arborio rice in place of short-grain"
                },
                new Rating
                {
                    User = "Foodie42",
                    Date = DateTime.Now.AddDays(-3),
                    Recipe = "Beef Tacos",
                    DishRating = 4.9,
                    ReviewText = "Amazing flavor! Will make these every week.",
                    ChangesMade = "Added avocado and fresh cilantro"
                }
            );
            context.SaveChanges();
        }
    }
}