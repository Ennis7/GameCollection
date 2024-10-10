using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameCollection.Data;
using GameCollection.Models;

namespace GameCollection.Pages.Kazi
{
    public class EditModel : PageModel
    {
        private readonly GameCollection.Data.GameCollectionContext _context;

        public EditModel(GameCollection.Data.GameCollectionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Games Games { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var games = await _context.Games.FirstOrDefaultAsync(m => m.ID == id);
            if (games == null)
            {
                return NotFound();
            }

            Games = games;

            // Populate OwnerID dropdown
            ViewData["OwnerID"] = new SelectList(_context.Owner, "ID", "ID");

            // Populate GenreType dropdown
            ViewData["GenreType"] = Enum.GetValues(typeof(Genre))
                .Cast<Genre>()
                .Select(g => new SelectListItem
                {
                    Value = g.ToString(),
                    Text = g.ToString(),
                    Selected = g == Games.GenreType // Select the current genre
                }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Removed ModelState.IsValid check
            if (_context.Games == null || Games == null)
            {
                return Page();
            }

            // Parse the selected genre from the dropdown
            if (Enum.TryParse<Genre>(Request.Form["Games.GenreType"], out var genre))
            {
                Games.GenreType = genre; // Set the parsed enum value
            }
            else
            {
                ModelState.AddModelError("Games.GenreType", "Invalid genre selected.");
                return Page();
            }

            _context.Attach(Games).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GamesExists(Games.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool GamesExists(int id)
        {
            return (_context.Games?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
