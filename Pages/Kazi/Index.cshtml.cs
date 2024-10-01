using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GameCollection.Data;
using GameCollection.Models;

namespace GameCollection.Pages.Kazi
{
    public class IndexModel : PageModel
    {
        private readonly GameCollectionContext _context;

        public IndexModel(GameCollectionContext context)
        {
            _context = context;
        }

        public IList<Games> Games { get; set; } = default!;

        public string TitleSearch { get; set; } = string.Empty;
        public string GenreSearch { get; set; } = string.Empty;
        public string MinPrice { get; set; } = string.Empty;
        public string MaxPrice { get; set; } = string.Empty;

        public string TitleSortOrder { get; set; }
        public string GenreSortOrder { get; set; }
        public string DeveloperSortOrder { get; set; }
        public string ReleaseDateSortOrder { get; set; }
        public string PriceSortOrder { get; set; }
        public string OwnerSortOrder { get; set; }

        public async Task OnGetAsync(string sortOrder, string titleSearch, string genreSearch, string minPrice, string maxPrice)
        {
            TitleSearch = titleSearch ?? string.Empty;
            GenreSearch = genreSearch ?? string.Empty;
            MinPrice = minPrice ?? string.Empty;
            MaxPrice = maxPrice ?? string.Empty;

            IQueryable<Games> gamesQuery = _context.Games.Include(g => g.Owner);
            gamesQuery = gamesQuery.Where(g => g.OwnerID == 4);

            // Filtering logic
            if (!string.IsNullOrEmpty(TitleSearch))
            {
                gamesQuery = gamesQuery.Where(g => g.Title.Contains(TitleSearch));
            }

            if (!string.IsNullOrEmpty(GenreSearch) && Enum.TryParse<Genre>(GenreSearch, true, out var genreEnum))
            {
                gamesQuery = gamesQuery.Where(g => g.GenreType == genreEnum);
            }

            if (decimal.TryParse(MinPrice, out decimal minPriceValue))
            {
                gamesQuery = gamesQuery.Where(g => g.Price >= minPriceValue);
            }

            if (decimal.TryParse(MaxPrice, out decimal maxPriceValue))
            {
                gamesQuery = gamesQuery.Where(g => g.Price <= maxPriceValue);
            }

            // Sorting logic
            switch (sortOrder)
            {
                case "title_desc":
                    gamesQuery = gamesQuery.OrderByDescending(g => g.Title);
                    break;
                case "genre":
                    gamesQuery = gamesQuery.OrderBy(g => g.GenreType);
                    break;
                case "genre_desc":
                    gamesQuery = gamesQuery.OrderByDescending(g => g.GenreType);
                    break;
                case "developer":
                    gamesQuery = gamesQuery.OrderBy(g => g.Developer);
                    break;
                case "developer_desc":
                    gamesQuery = gamesQuery.OrderByDescending(g => g.Developer);
                    break;
                case "releaseDate":
                    gamesQuery = gamesQuery.OrderBy(g => g.ReleaseDate);
                    break;
                case "releaseDate_desc":
                    gamesQuery = gamesQuery.OrderByDescending(g => g.ReleaseDate);
                    break;
                case "price":
                    gamesQuery = gamesQuery.OrderBy(g => g.Price);
                    break;
                case "price_desc":
                    gamesQuery = gamesQuery.OrderByDescending(g => g.Price);
                    break;
                case "owner":
                    gamesQuery = gamesQuery.OrderBy(g => g.Owner.ID);
                    break;
                case "owner_desc":
                    gamesQuery = gamesQuery.OrderByDescending(g => g.Owner.ID);
                    break;
                default:
                    gamesQuery = gamesQuery.OrderBy(g => g.Title);
                    break;
            }

            Games = await gamesQuery.ToListAsync();
        }
    }
}
