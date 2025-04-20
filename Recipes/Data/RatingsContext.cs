using Microsoft.EntityFrameworkCore;
using Recipes.Models;

namespace Recipes.Data;

public class RatingsContext : DbContext
{
    public RatingsContext(DbContextOptions<RatingsContext> options)
        : base(options)
    {
    }

    public DbSet<Rating> Ratings { get; set; } = default!;
}