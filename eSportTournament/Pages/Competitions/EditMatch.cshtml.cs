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
            Match temp = await _context.Matchs.FirstOrDefaultAsync(m => m.ID == id);

            Competition compt = await _context.Competitions.FirstOrDefaultAsync(c => c.ID == temp.CompetitionID);

            temp.EquipeAScore = Match.EquipeAScore;
            temp.EquipeBScore = Match.EquipeBScore;

            int winner = 0;
            bool final = false;

          
            
            if (temp.EquipeAScore > temp.EquipeBScore)
            {
                temp.gagnantID = temp.EquipeAID;
                winner = (int)temp.EquipeAID;
            } else
            {
                temp.gagnantID = temp.EquipeBID;
                winner = (int)temp.EquipeBID;
            }

            _context.Attach(temp).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var rn = temp.roundNumber + 1;
            if(compt.isTournoi == true) {
                if (temp.roundNumber == nbTour(compt.nombreJoueurs))
                {
                    final = true;
                }
                else
                {
                    final = false;
                }
                if (!final)
                {
                    Match matchSuivantExistant = await _context.Matchs.FirstOrDefaultAsync(m => m.CompetitionID == temp.CompetitionID && m.roundNumber == rn && m.EquipeBID == null && m.EquipeAID != null);
                    if (matchSuivantExistant == null)
                    {
                        Match matchSuivant = new Match();
                        matchSuivant.EquipeAID = temp.gagnantID;
                        matchSuivant.CompetitionID = temp.CompetitionID;
                        matchSuivant.roundNumber = temp.roundNumber + 1;
                        _context.Matchs.Add(matchSuivant);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        matchSuivantExistant.EquipeBID = winner;
                        _context.Attach(matchSuivantExistant).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    compt.terminer = true;
                    _context.Attach(compt).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            


            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private int nbTour(int nb)
        {
            int x = 1;
            while (nb != 2)
            {
                nb /= 2;
                x += 1;
            }
            return x;
        }
    }
}
