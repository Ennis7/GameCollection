using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GameCollection.Data;
using GameCollection.Models;

namespace GameCollection.Pages.Kazi
{
    public class CreateModel : PageModel
    {
        private readonly GameCollection.Data.GameCollectionContext _context;

        public CreateModel(GameCollection.Data.GameCollectionContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["OwnerID"] = new SelectList(_context.Owner, "ID", "ID");

            ViewData["GenreType"] = Enum.GetValues(typeof(Genre))
                .Cast<Genre>()
                .Select(g => new SelectListItem
                {
                    Value = g.ToString(),
                    Text = g.ToString()
                }).ToList();

            return Page();
        }

        [BindProperty]
        public Games Games { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // Remove ModelState.IsValid check
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

            // Add the new game to the context and save
            _context.Games.Add(Games);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
