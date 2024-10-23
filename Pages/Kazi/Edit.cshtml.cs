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
        private readonly GameCollectionContext _context;

        public EditModel(GameCollectionContext context)
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

            // dropdown
            ViewData["OwnerID"] = new SelectList(_context.Owner, "ID", "ID");

            // dropdown
            ViewData["GenreType"] = Enum.GetValues(typeof(Genre))
                .Cast<Genre>()
                .Select(g => new SelectListItem
                {
                    Value = g.ToString(),
                    Text = g.ToString()
                }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
   
      
            _context.Attach(Games).State = EntityState.Modified;

            try
            {
                // Attempt to save changes
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
