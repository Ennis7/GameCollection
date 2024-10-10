using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GameCollection.Data;
using GameCollection.Models;

namespace GameCollection.Pages.Ethan
{
    public class CreateModel : PageModel
    {
        private readonly GameCollection.Data.GameCollectionContext _context;

        public CreateModel(GameCollection.Data.GameCollectionContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {

        ViewData["OwnerID"] = new SelectList(_context.Owner, "ID", "ID");
            ViewData["GenreType"] = new SelectList(Enum.GetValues(typeof(Genre)).Cast<Genre>());
            return Page();

        }

        [BindProperty]
        public Games Games { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {


            _context.Games.Add(Games);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
