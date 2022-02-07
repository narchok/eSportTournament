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

namespace eSportTournament.Pages.Equipes
{
    [AllowAnonymous]

    public class DetailsModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;

        public DetailsModel(eSportTournament.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Equipe Equipe { get; set; }


        [BindProperty]
        public IList<Utilisateur> joueursEquipe { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipe = await _context.Equipes.FirstOrDefaultAsync(m => m.ID == id);
            joueursEquipe = await _context.Utilisateurs.Where(u => u.equipeID == id).ToListAsync();
            if (Equipe == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
