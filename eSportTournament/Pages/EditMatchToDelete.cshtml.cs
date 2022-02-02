using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eSportTournament.Data;
using eSportTournament.Models;

namespace eSportTournament.Pages
{
    public class EditMatchToDeleteModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;

        public EditMatchToDeleteModel(eSportTournament.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Match Match { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Match = await _context.Matchs
                .Include(m => m.Competition)
                .Include(m => m.EquipeA)
                .Include(m => m.EquipeB).FirstOrDefaultAsync(m => m.ID == id);

            if (Match == null)
            {
                return NotFound();
            }
           ViewData["CompetitionID"] = new SelectList(_context.Competitions, "ID", "nomCompetition");
           ViewData["EquipeAID"] = new SelectList(_context.Equipes, "ID", "nomEquipe");
           ViewData["EquipeBID"] = new SelectList(_context.Equipes, "ID", "nomEquipe");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(Match.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MatchExists(int id)
        {
            return _context.Matchs.Any(e => e.ID == id);
        }
    }
}
