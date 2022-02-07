using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eSportTournament.Data;
using eSportTournament.Models;

namespace eSportTournament.Pages.Competitions
{
    public class VoirMatchsModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;

        public VoirMatchsModel(eSportTournament.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Match> Match { get;set; }

        public async Task OnGetAsync()
        {
            Match = await _context.Matchs
                .Include(m => m.Competition)
                .Include(m => m.EquipeA)
                .Include(m => m.EquipeB).Where(m => m.gagnantID == null && m.EquipeBID !=null && m.Competition.isTournoi == false).ToListAsync();
        }
    }
}
