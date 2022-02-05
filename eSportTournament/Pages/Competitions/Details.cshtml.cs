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

namespace eSportTournament.Pages.Competitions
{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;

        public DetailsModel(eSportTournament.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Match> Match { get;set; }

        Dictionary<int, string[,]> MatchSortedWithRound =
    new Dictionary<int, string[,]>();


        public async Task OnGetAsync(int id)
        {
            var test = MatchSortedWithRound.Count;
            Competition compet = await _context.Competitions.FirstOrDefaultAsync(c => c.ID == id);

            Match = await _context.Matchs
                .Include(m => m.Competition)
                .Include(m => m.EquipeA)
                .Include(m => m.EquipeB).Where(m => m.CompetitionID == id).ToListAsync();
            
            for (int i = 0; i < nbTour(compet.nombreJoueurs); i++)
          {
                List<Match> temp = Match.Where(m => m.roundNumber == i).ToList();
                int v = 0;
                string[,] tempString = null;
                if(temp.Count > 0)
                {
                    tempString = new string[temp.Count, 7];
                    for (int j = 0; j < temp.Count; j++)
                    {
                        tempString[j, 0] = temp[j].EquipeA.nomEquipe;
                        tempString[j, 1] = temp[j].EquipeAScore.ToString();
                        tempString[j, 2] = temp[j].EquipeBScore.ToString();
                        if (temp[j].EquipeB != null) { tempString[j, 3] = temp[j].EquipeB.nomEquipe; }
                        tempString[j, 4] = temp[j].EquipeA.ID.ToString();
                        if (temp[j].EquipeB != null)
                        {
                            tempString[j, 5] = temp[j].EquipeB.ID.ToString();
                        }
                        tempString[j, 6] = temp[j].gagnantID.ToString();
                    }

                    MatchSortedWithRound.Add(i, tempString);
                }
            }
            ViewData["dict"] = MatchSortedWithRound;

        }

        private int nbTour(int nb)
        {
            int x = 1;
            while(nb != 2)
            {
                nb /= 2;
                x += 1;
            }
            return x;
        }
    }
}
