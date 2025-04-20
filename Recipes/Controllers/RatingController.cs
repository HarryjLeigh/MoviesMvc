using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Models;

namespace Recipes.Controllers;

public class RatingController : Controller
{
    private readonly RatingsContext _context;

    public RatingController(RatingsContext context)
    {
        _context = context;
    }

    // GET: Rating
    public async Task<IActionResult> Index(string user, string recipe)
    {
        if (!_context.Ratings.Any()) return Problem("Entity set 'Ratings' is null.");

        IQueryable<Rating> userRatings = from r in _context.Ratings
            select r;

        IQueryable<string> users = from u in _context.Ratings
            orderby u.User
            select u.User;

        IQueryable<string> recipes = from r in _context.Ratings
            orderby r.Recipe
            select r.Recipe;


        if (!string.IsNullOrEmpty(user))
            userRatings = userRatings.Where(r => r.User!.ToLower() == user.ToLower());

        if (!string.IsNullOrEmpty(recipe))
            userRatings = userRatings.Where(r => r.Recipe!.ToLower() == recipe.ToLower());

        var ratingViewModel = new RatingViewModel
        {
            Ratings = await userRatings.ToListAsync(),
            Users = new SelectList(await users.Distinct().ToListAsync()),
            Recipes = new SelectList(await recipes.Distinct().ToListAsync())
        };

        return View(ratingViewModel);
    }

    // GET: Rating/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var rating = await _context.Ratings
            .FirstOrDefaultAsync(m => m.Id == id);
        if (rating == null) return NotFound();

        return View(rating);
    }

    // GET: Rating/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Rating/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,User,Date,Recipe,DishRating,ReviewText,ChangesMade")]
        Rating rating)
    {
        if (ModelState.IsValid)
        {
            _context.Add(rating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(rating);
    }

    // GET: Rating/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var rating = await _context.Ratings.FindAsync(id);
        if (rating == null) return NotFound();
        return View(rating);
    }

    // POST: Rating/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,User,Date,Recipe,DishRating,ReviewText,ChangesMade")]
        Rating rating)
    {
        if (id != rating.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(rating);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(rating.Id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(rating);
    }

    // GET: Rating/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var rating = await _context.Ratings
            .FirstOrDefaultAsync(m => m.Id == id);
        if (rating == null) return NotFound();

        return View(rating);
    }

    // POST: Rating/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var rating = await _context.Ratings.FindAsync(id);
        if (rating != null) _context.Ratings.Remove(rating);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RatingExists(int id)
    {
        return _context.Ratings.Any(e => e.Id == id);
    }
}