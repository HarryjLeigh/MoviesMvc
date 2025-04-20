using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Models;

namespace Recipes.Controllers;

public class RecipeController(RecipesContext context) : Controller
{
    private readonly RecipesContext _context = context;

    // GET: Recipe
    public async Task<IActionResult> Index(
        string recipeCuisine,
        string recipeVegetarian,
        string recipeDifficulty,
        string recipeDishType,
        string searchString)
    {
        if (_context.Recipe == null) return Problem("Entity set 'RecipesContext' is null.");
        IQueryable<string> cuisineQuery = from r in _context.Recipe
            orderby r.Cuisine
            select r.Cuisine;

        IQueryable<string> vegetarianQuery = from r in _context.Recipe
            orderby r.IsVegetarian
            select r.IsVegetarian;

        IQueryable<string> difficultyQuery = from r in _context.Recipe
            orderby r.Difficulty
            select r.Difficulty;

        IQueryable<string> dishTypeQuery = from r in _context.Recipe
            orderby r.DishType
            select r.DishType;

        var recipes = from r in _context.Recipe
            select r;

        if (!string.IsNullOrEmpty(searchString))
            recipes = recipes.Where(r => r.Name!.ToLower().Contains(searchString.ToLower()));

        if (!string.IsNullOrEmpty(recipeCuisine))
            recipes = recipes.Where(r => r.Cuisine!.ToLower() == recipeCuisine.ToLower());

        if (!string.IsNullOrEmpty(recipeVegetarian))
            recipes = recipes.Where(r => r.IsVegetarian!.ToLower() == recipeVegetarian.ToLower());

        if (!string.IsNullOrEmpty(recipeDifficulty))
            recipes = recipes.Where(r => r.Difficulty!.ToLower() == recipeDifficulty.ToLower());

        if (!string.IsNullOrEmpty(recipeDishType))
            recipes = recipes.Where(r => r.DishType!.ToLower() == recipeDishType.ToLower());

        var recipeViewModel = new RecipeViewModel
        {
            Cuisines = new SelectList(await cuisineQuery.Distinct().ToListAsync()),
            IsVegetarian = new SelectList(await vegetarianQuery.Distinct().ToListAsync()),
            Difficulties = new SelectList(await difficultyQuery.Distinct().ToListAsync()),
            DishTypes = new SelectList(await dishTypeQuery.Distinct().ToListAsync()),
            Recipes = await recipes.ToListAsync()
        };

        return View(recipeViewModel);
    }

    // GET: Recipe/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var recipe = await _context.Recipe
            .FirstOrDefaultAsync(m => m.Id == id);
        if (recipe == null) return NotFound();

        return View(recipe);
    }

    // GET: Recipe/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Recipe/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Name,Cuisine,DishType,IsVegetarian,PrepTime,CookTime,TotalTime,Difficulty,AverageRating")]
        Recipe recipe)
    {
        if (ModelState.IsValid)
        {
            _context.Add(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(recipe);
    }

    // GET: Recipe/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var recipe = await _context.Recipe.FindAsync(id);
        if (recipe == null) return NotFound();
        return View(recipe);
    }

    // POST: Recipe/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,Name,Cuisine,DishType,IsVegetarian,PrepTime,CookTime,TotalTime,Difficulty,AverageRating")]
        Recipe recipe)
    {
        if (id != recipe.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(recipe);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(recipe.Id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(recipe);
    }

    // GET: Recipe/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var recipe = await _context.Recipe
            .FirstOrDefaultAsync(m => m.Id == id);
        if (recipe == null) return NotFound();

        return View(recipe);
    }

    // POST: Recipe/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var recipe = await _context.Recipe.FindAsync(id);
        if (recipe != null) _context.Recipe.Remove(recipe);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RecipeExists(int id)
    {
        return _context.Recipe.Any(e => e.Id == id);
    }
}