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
        private readonly GameCollection.Data.GameCollectionContext _context;

        public IndexModel(GameCollection.Data.GameCollectionContext context)
        {
            _context = context;
        }

        public IList<Games> Games { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Games != null)
            {
                Games = await _context.Games
                .Include(g => g.Owner).ToListAsync();
            }
        }
    }
}
