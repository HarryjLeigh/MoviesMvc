using Microsoft.EntityFrameworkCore;
using MvcMovies.Data;

namespace MvcMovies.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcMoviesContext(
                   serviceProvider.GetRequiredService<DbContextOptions<MvcMoviesContext>>()))
        {
            if (context.Movie.Any()) return;
            context.Movie.AddRange(
                new Movie
                {
                    Title = "Star Wars",
                    ReleaseDate = DateTime.Parse("2021-09-01"),
                    Genre = "Action",
                    Rating = "R",
                    Price = 9.99M
                },
                new Movie
                {
                    Title = "Despicable Me",
                    ReleaseDate = DateTime.Parse("2021-09-10"),
                    Genre = "Animation",
                    Rating = "PG",
                    Price = 12.99M
                },
                new Movie
                {
                    Title = "The Dark Knight",
                    ReleaseDate = DateTime.Parse("2018-09-11"),
                    Genre = "Action",
                    Rating = "13",
                    Price = 20.99M
                },
                new Movie
                {
                    Title = "The Fellowship of the Ring",
                    ReleaseDate = DateTime.Parse("10-09-11"),
                    Genre = "Adventure",
                    Rating = "15",
                    Price = 50.99M
                }
            );
            context.SaveChanges();
        }
    }
}