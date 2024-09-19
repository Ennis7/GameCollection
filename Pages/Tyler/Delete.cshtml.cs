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
    public class DeleteModel : PageModel
    {
        private readonly GameCollection.Data.GameCollectionContext _context;

        public DeleteModel(GameCollection.Data.GameCollectionContext context)
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
            else 
            {
                Games = games;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }
            var games = await _context.Games.FindAsync(id);

            if (games != null)
            {
                Games = games;
                _context.Games.Remove(Games);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
