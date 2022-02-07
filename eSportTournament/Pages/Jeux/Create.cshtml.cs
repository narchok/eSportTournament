using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using eSportTournament.Data;
using eSportTournament.Models;
using Microsoft.AspNetCore.Authorization;

namespace eSportTournament.Pages.Jeux
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;

        public CreateModel(eSportTournament.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Jeu Jeu { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Jeux.Add(Jeu);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
