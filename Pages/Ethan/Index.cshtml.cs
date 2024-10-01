using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GameCollection.Data;
using GameCollection.Models;
using Microsoft.Data.SqlClient;

namespace GameCollection.Pages.Ethan
{
    public class IndexModel : PageModel
    {
        private readonly GameCollection.Data.GameCollectionContext _context;

        public IndexModel(GameCollection.Data.GameCollectionContext context)
        {
            _context = context;
        }

        public IList<Games> AllGames { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public decimal MinPrice { get; set; } = 0;

        [BindProperty(SupportsGet = true)]
        public string SearchGenre { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal MaxPrice { get; set; } = 9999; 

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchDeveloper { get; set; }
        public string TitleSort { get; set; }
        public string GenreSort { get; set; }
        public string DeveloperSort { get; set; }
        public string ReleaseDateSort { get; set; }
        public string PriceSort { get; set; }



        [BindProperty(SupportsGet = true)]
        public DateTime SearchRelease { get; set; } 

        public IList<Games> Games { get; set; } = default!;

        public async Task OnGetAsync(string sortOrder)
        {
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "TitleDesc" : "";
            GenreSort = sortOrder == "GenreAsc" ? "GenreDesc" : "GenreAsc";
            DeveloperSort = sortOrder == "DeveloperAsc" ? "DeveloperDesc" : "DeveloperAsc";
            ReleaseDateSort = sortOrder == "ReleaseDateAsc" ? "ReleaseDateDesc" : "ReleaseDateAsc";
            PriceSort = sortOrder == "PriceAsc" ? "PriceDesc" : "PriceAsc";


            var gameFilter = from g in _context.Games
                             select g;


            gameFilter = gameFilter.Where(g => g.OwnerID == 2);
            gameFilter = gameFilter.Where(g => g.Price >= MinPrice && g.Price <= MaxPrice);

            if (!string.IsNullOrEmpty(SearchString))
            {
                gameFilter = gameFilter.Where(g => g.Title.Contains(SearchString));
            }


            if (!string.IsNullOrEmpty(SearchDeveloper))
            {
                gameFilter = gameFilter.Where(g => g.Developer.Contains(SearchDeveloper));
            }

            if (SearchRelease != default(DateTime))
            {
                gameFilter = gameFilter.Where(g => g.ReleaseDate.Date == SearchRelease.Date);
            }
            switch (sortOrder)
            {
                case "TitleDesc":
                    gameFilter = gameFilter.OrderByDescending(g => g.Title);
                    break;
                case "GenreDesc":
                    gameFilter = gameFilter.OrderByDescending(g => g.GenreType);
                    break;
                case "GenreAsc":
                    gameFilter = gameFilter.OrderBy(g => g.GenreType);
                    break;
                case "DeveloperDesc":
                    gameFilter = gameFilter.OrderByDescending(g => g.Developer);
                    break;
                case "DeveloperAsc":
                    gameFilter = gameFilter.OrderBy(g => g.Developer);
                    break;
                case "ReleaseDateDesc":
                    gameFilter = gameFilter.OrderByDescending(g => g.ReleaseDate);
                    break;
                case "ReleaseDateAsc":
                    gameFilter = gameFilter.OrderBy(g => g.ReleaseDate);
                    break;
                case "PriceDesc":
                    gameFilter = gameFilter.OrderByDescending(g => g.Price);
                    break;
                case "PriceAsc":
                    gameFilter = gameFilter.OrderBy(g => g.Price);
                    break;
                default:
                    gameFilter = gameFilter.OrderBy(g => g.Title);
                    break;
            }

            Games = await gameFilter.ToListAsync();
        }
    }
}
