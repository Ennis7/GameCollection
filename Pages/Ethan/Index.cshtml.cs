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
        public string DeveloperSort { get; set; }



        [BindProperty(SupportsGet = true)]
        public DateTime SearchRelease { get; set; } 

        public IList<Games> Games { get; set; } = default!;

        public async Task OnGetAsync(string sortOrder)
        {
            TitleSort = sortOrder;
            
            var gameFilter = from g in _context.Games
                             select g;


            gameFilter = gameFilter.Where(g => g.OwnerID == 2);


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
            if (TitleSort == "desc")
            {
                gameFilter = gameFilter.OrderByDescending(g => g.Title);
            }
            else
            {
                gameFilter = gameFilter.OrderBy(g => g.Title);
            }

            gameFilter = gameFilter.Where(g => g.Price >= MinPrice && g.Price <= MaxPrice);

            Games = await gameFilter.ToListAsync();
        }
    }
}
