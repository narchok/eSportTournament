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

    public class IndexModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;

        public IndexModel(eSportTournament.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Jeu> Jeu { get;set; }

        public async Task OnGetAsync()
        {
            Jeu = await _context.Jeux.ToListAsync();
        }
    }
}
