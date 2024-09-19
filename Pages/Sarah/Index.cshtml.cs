using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GameCollection.Data;
using GameCollection.Models;

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
        public IList<Games> Games { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if (_context.Games != null)
            {
                AllGames = await _context.Games
                .Include(g => g.Owner).ToListAsync();
                Games = AllGames.Where(g => g.OwnerID == 1).ToList();
            }
        }
    }
}
