using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eSportTournament.Data;
using eSportTournament.Models;

namespace eSportTournament.Pages.Equipes
{
    public class DeleteModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;

        public DeleteModel(eSportTournament.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Equipe Equipe { get; set; }

        [BindProperty]
        public bool haveMatchs { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipe = await _context.Equipes.FirstOrDefaultAsync(m => m.ID == id);

            if (Equipe == null)
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

            Equipe = await _context.Equipes.FindAsync(id);

            if (Equipe != null)
            {
                var matchs = await _context.Matchs.Where(m => (m.EquipeAID == id || m.EquipeBID == id) && m.gagnantID == null).ToListAsync();
                if (matchs.Count > 0)
                {
                    Equipe.valider = false;
                    _context.Attach(Equipe).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                } else
                {
                    _context.Equipes.Remove(Equipe);
                    await _context.SaveChangesAsync();
                }
          
            }

            return RedirectToPage("./Index");
        }
    }
}
