using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GameCollection.Data;
using GameCollection.Models;

namespace GameCollection.Pages.SarahOwner
{
    public class DeleteModel : PageModel
    {
        private readonly GameCollection.Data.GameCollectionContext _context;

        public DeleteModel(GameCollection.Data.GameCollectionContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Owner Owner { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Owner == null)
            {
                return NotFound();
            }

            var owner = await _context.Owner.FirstOrDefaultAsync(m => m.ID == id);

            if (owner == null)
            {
                return NotFound();
            }
            else 
            {
                Owner = owner;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Owner == null)
            {
                return NotFound();
            }
            var owner = await _context.Owner.FindAsync(id);

            if (owner != null)
            {
                Owner = owner;
                _context.Owner.Remove(Owner);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
