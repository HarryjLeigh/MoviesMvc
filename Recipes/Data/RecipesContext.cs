using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recipes.Models;

namespace Recipes.Data
{
    public class RecipesContext : DbContext
    {
        public RecipesContext (DbContextOptions<RecipesContext> options)
            : base(options)
        {
        }

        public DbSet<Recipes.Models.Recipe> Recipe { get; set; } = default!;
    }
}
