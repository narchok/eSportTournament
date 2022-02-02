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

namespace eSportTournament.Pages.Competitions
{
    public class EditMatchModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;

        public EditMatchModel(eSportTournament.Data.ApplicationDbContext context)
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
           //ViewData["CompetitionID"] = new SelectList(_context.Competitions, "ID", "nomCompetition");
           //ViewData["EquipeAID"] = new SelectList(_context.Equipes, "ID", "nomEquipe");
           //ViewData["EquipeBID"] = new SelectList(_context.Equipes, "ID", "nomEquipe");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            System.Console.WriteLine("onPostEditMatch" + Match.EquipeAScore);
            Match temp = await _context.Matchs.FirstOrDefaultAsync(m => m.ID == id);
            temp.EquipeAScore = Match.EquipeAScore;
            temp.EquipeBScore = Match.EquipeBScore;

            if(temp.EquipeAScore > temp.EquipeBScore)
            {
                temp.gagnantID = temp.EquipeAID;
            } else
            {
                temp.gagnantID = temp.EquipeBID;
            }

            _context.Attach(temp).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            Match matchSuivant = new Match();
            matchSuivant.EquipeAID = temp.gagnantID;
            matchSuivant.CompetitionID = temp.CompetitionID;

            matchSuivant.roundNumber = temp.roundNumber + 1; 
            _context.Matchs.Add(matchSuivant);
            await _context.SaveChangesAsync();
           

            return RedirectToPage("./Index");
        }

        private bool MatchExists(int id)
        {
            return _context.Matchs.Any(e => e.ID == id);
        }
    }
}
