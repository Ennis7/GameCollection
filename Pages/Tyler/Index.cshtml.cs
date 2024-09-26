using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GameCollection.Data;
using GameCollection.Models;

namespace GameCollection.Pages.Tyler
{
    public class IndexModel : PageModel
    {
        private readonly GameCollectionContext _context;

        public IndexModel(GameCollectionContext context)
        {
            _context = context;
        }
        public IList<Games> Games { get; set; } = default!;
        public Games SelectedGame { get; set; } = default!;
        public string CurrentFilter { get; set; }
        public Genre? GenreFilter { get; set; } // Changed to nullable Genre

        public async Task OnGetAsync(string searchString, Genre? genreFilter, int? id)
        {
            IQueryable<Games> gamesQuery = from g in _context.Games
                                           .Include(g => g.Owner)
                                           select g;

            if (!string.IsNullOrEmpty(searchString))
            {
                gamesQuery = gamesQuery.Where(g => g.Title.Contains(searchString));
            }

            if (genreFilter.HasValue)
            {
                gamesQuery = gamesQuery.Where(g => g.GenreType == genreFilter.Value); // Direct comparison
            }

            Games = await gamesQuery.ToListAsync();

            // If a game is selected, fetch its details for the right panel.
            if (id.HasValue)
            {
                SelectedGame = await _context.Games
                    .Include(g => g.Owner)
                    .FirstOrDefaultAsync(m => m.ID == id.Value);
            }
        }
    }
}

