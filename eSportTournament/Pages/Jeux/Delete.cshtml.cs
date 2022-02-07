using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eSportTournament.Data;
using eSportTournament.Models;
using Microsoft.AspNetCore.Authorization;

namespace eSportTournament.Pages.Jeux
{
    [Authorize(Roles = "Admin")]

    public class DeleteModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;

        public DeleteModel(eSportTournament.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Jeu Jeu { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Jeu = await _context.Jeux.FirstOrDefaultAsync(m => m.ID == id);

            if (Jeu == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Jeu = await _context.Jeux.FindAsync(id);

            if (Jeu != null)
            {
                _context.Jeux.Remove(Jeu);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
