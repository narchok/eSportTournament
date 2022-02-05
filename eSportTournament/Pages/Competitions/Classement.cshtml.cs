using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eSportTournament.Data;
using eSportTournament.Models;
using eSportTournament.VueModel;
using Microsoft.AspNetCore.Authorization;

namespace eSportTournament.Pages.Competitions
{
    [AllowAnonymous]
    public class ClassementModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;

        public ClassementModel(eSportTournament.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Competition Competition { get; set; }

        public IList<ClasssementChampionnat> Data { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Data = new List<ClasssementChampionnat>();
            var equipes = await _context.Equipes.ToListAsync();
            foreach(Equipe e in equipes)
            {
                var matchsWon = await _context.Matchs.Where(m => m.gagnantID == e.ID && m.CompetitionID == id).ToListAsync();
                var matchsLost = await _context.Matchs.Where(m => m.gagnantID != null && (m.EquipeAID == e.ID || m.EquipeBID == e.ID) && m.gagnantID != e.ID && m.CompetitionID == id).ToListAsync();
                var matchsCounted = await _context.Matchs.Where(m => (m.EquipeAID == e.ID || m.EquipeBID == e.ID) && m.CompetitionID == id).ToListAsync();
                var objet = new ClasssementChampionnat();
                objet.equipe = e;
                objet.nombreVictoire = matchsWon.Count();
                objet.nombreDefaite = matchsLost.Count();
                objet.points = matchsWon.Count() * 3;

                if(matchsCounted.Count > 0)Data.Add(objet);
            }
            Data = Data.OrderByDescending(d => d.points).ToList();
            if (id == null)
            {
                return NotFound();
            }

            Competition = await _context.Competitions.FirstOrDefaultAsync(m => m.ID == id);

            if (Competition == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
