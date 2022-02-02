using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eSportTournament.Data;
using eSportTournament.Models;

namespace eSportTournament.Pages
{
    public class IndexCompetModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;

        public IndexCompetModel(eSportTournament.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CompetitionEquipe> CompetitionEquipe { get;set; }

        public async Task OnGetAsync()
        {
            CompetitionEquipe = await _context.CompetitionEquipe
                .Include(c => c.CompetitionCs)
                .Include(c => c.EquipeE).ToListAsync();
        }
    }
}
