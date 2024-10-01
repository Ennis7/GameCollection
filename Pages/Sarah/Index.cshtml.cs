using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GameCollection.Data;
using GameCollection.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using System.ComponentModel.DataAnnotations;

namespace GameCollection.Pages.Sarah
{
    public class IndexModel : PageModel
    {
        private readonly GameCollection.Data.GameCollectionContext _context;

        public IndexModel(GameCollection.Data.GameCollectionContext context)
        {
            _context = context;
        }

        public IList<Games> AllGames { get;set; } = default!;
        
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        //public string SearchGenre { get; set; }
        //[BindProperty(SupportsGet = true)]
        public string SearchDeveloper { get; set; }

        [BindProperty]
        public string SelectedGenre { get; set; }    
        public string TitleSort { get; set; }
        public string DeveloperSort { get; set; }
        public string GenreSort { get; set; }
        public string PriceSort { get; set; }
        public string DateSort { get; set; }

        [BindProperty]
        public decimal SelectedPrice { get; set; }



        public IList<Games> Games { get; set; } = default!;

        public IList<Games> FilteredGames { get; set; }
    

        public async Task OnGetAsync(string sortOrder)
        {
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "TitleDesc" : "";
            DeveloperSort = sortOrder == "DeveloperAsc" ? "DeveloperDesc" : "DeveloperAsc";
            GenreSort = sortOrder == "GenreAsc" ? "GenreDesc" : "GenreAsc";
            DateSort = sortOrder == "DateAsc" ? "DateDesc" : "DateAsc";
            PriceSort = sortOrder == "PriceAsc" ? "PriceDesc" : "PriceAsc";

            var gameInfo = from g in _context.Games
                           select g;
            gameInfo = gameInfo.Where(g => g.OwnerID == 1);
        
            switch (sortOrder)
            {
                case "TitleDesc":
                    gameInfo = gameInfo.OrderByDescending(g => g.Title);
                    break;
                case "DeveloperDesc":
                    gameInfo = gameInfo.OrderByDescending(g => g.Developer);
                    break;
                case "DeveloperAsc":
                    gameInfo = gameInfo.OrderBy(g => g.Developer);
                    break;
               case "GenreDesc":
                    gameInfo = gameInfo.OrderByDescending(g => g.GenreType);
                    break;
                case "GenreAsc":
                    gameInfo = gameInfo.OrderBy(g => g.GenreType);
                    break;
                case "PriceDesc":
                    gameInfo = gameInfo.OrderByDescending(g => g.Price);
                    break;
                case "PriceAsc":
                    gameInfo = gameInfo.OrderBy(g => g.Price);
                    break;
                case "DateDesc":
                    gameInfo = gameInfo.OrderByDescending(g => g.ReleaseDate);
                    break;
                case "DateAsc":
                    gameInfo = gameInfo.OrderBy(g => g.ReleaseDate);
                    break;
                default:
                    gameInfo = gameInfo.OrderBy(g => g.Title);
                    break;
            }
                    Games = await gameInfo.ToListAsync();
           
            FilteredGames = Games;
        }

        public async Task OnPostAsync()
        {
            if (Games == null)
            {
                // Reload games if it's null (e.g., on initial load)
                Games = await _context.Games.Where(g => g.OwnerID == 1).ToListAsync();
            }

            FilteredGames = Games;
            
            
            if (SelectedGenre == "None")
            {
                Console.WriteLine("this works");
                FilteredGames = Games;
            } else
            {
                if (Enum.TryParse<Genre>(SelectedGenre, out var selectedGenreEnum))
                {
                    // Filter by the parsed genre
                    FilteredGames = FilteredGames
                        .Where(game => game.GenreType == selectedGenreEnum)
                        .ToList();
                }
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                FilteredGames = FilteredGames
                    .Where(game =>
                        game.Title.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
                        game.Developer.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (SelectedPrice != 0)
            {
                FilteredGames = FilteredGames
                    .Where(game => game.Price <= SelectedPrice)
                    .ToList();
            }
        }
    }
}
