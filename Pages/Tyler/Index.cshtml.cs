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
        public Genre? GenreFilter { get; set; }

        public async Task OnGetAsync(string searchString, Genre? genreFilter, int? id, string sortOrder)
        {
            CurrentFilter = searchString;
            GenreFilter = genreFilter;

            IQueryable<Games> gamesQuery = from g in _context.Games
                                           .Include(g => g.Owner)
                                           select g;

            // Apply search filter
            if (!string.IsNullOrEmpty(searchString))
            {
                gamesQuery = gamesQuery.Where(g => g.Title.Contains(searchString));
            }

            // Apply genre filter
            if (genreFilter.HasValue)
            {
                gamesQuery = gamesQuery.Where(g => g.GenreType == genreFilter.Value);
            }

            // Apply sorting
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["GenreSortParm"] = sortOrder == "Genre" ? "genre_desc" : "Genre";
            ViewData["ReleaseDateSortParm"] = sortOrder == "ReleaseDate" ? "releaseDate_desc" : "ReleaseDate";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            switch (sortOrder)
            {
                case "title_desc":
                    gamesQuery = gamesQuery.OrderByDescending(g => g.Title);
                    break;
                case "Genre":
                    gamesQuery = gamesQuery.OrderBy(g => g.GenreType);
                    break;
                case "genre_desc":
                    gamesQuery = gamesQuery.OrderByDescending(g => g.GenreType);
                    break;
                case "ReleaseDate":
                    gamesQuery = gamesQuery.OrderBy(g => g.ReleaseDate);
                    break;
                case "releaseDate_desc":
                    gamesQuery = gamesQuery.OrderByDescending(g => g.ReleaseDate);
                    break;
                case "Price":
                    gamesQuery = gamesQuery.OrderBy(g => g.Price);
                    break;
                case "price_desc":
                    gamesQuery = gamesQuery.OrderByDescending(g => g.Price);
                    break;
                default: // Title ascending
                    gamesQuery = gamesQuery.OrderBy(g => g.Title);
                    break;
            }

            // Execute the query
            Games = await gamesQuery.ToListAsync();

            // Fetch selected game details if an ID is provided
            if (id.HasValue)
            {
                SelectedGame = await _context.Games
                    .Include(g => g.Owner)
                    .FirstOrDefaultAsync(m => m.ID == id.Value);
            }
        }
    }
}

