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
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace eSportTournament.Pages.Equipes
{
    [AllowAnonymous]

    public class DetailsModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DetailsModel(UserManager<IdentityUser> userManager, eSportTournament.Data.ApplicationDbContext context)
        {
            _userManager = userManager;

            _context = context;
        }

        public Equipe Equipe { get; set; }

        [BindProperty]
        public bool showDelete { get; set; }

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
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != null)
            {
                Equipe c = await _context.Equipes.FirstOrDefaultAsync(co => co.ownerID == userId && co.ID == id);
                if (c != null)
                {
                    showDelete = true;

                } 
            }
            if (Equipe == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
