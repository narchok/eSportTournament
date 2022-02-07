using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eSportTournament.Data;
using eSportTournament.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace eSportTournament.Pages
{
    public class RequeteEquipeCreationModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;

        public RequeteEquipeCreationModel(eSportTournament.Data.ApplicationDbContext context)
        {
            _context = context;
        }


        public IList<DemandeEquipeCreation> DemandeEquipe { get;set; }

        public List<Equipe> EquipeAll { get; set; }

        public Equipe Equipe { get; set; }


        [BindProperty]
        public int[] ouioui { get; set; }


        public bool ShowTeam { get; set; } = false;


        public async Task<PageResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Utilisateur utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.userID == userId);
                List<Equipe> test = new List<Equipe>();
                DemandeEquipe = await _context.DemandesEquipeCreations.Where(d => d.approuver == false).ToListAsync();


                foreach (DemandeEquipeCreation d in DemandeEquipe)
                {
                    Equipe equipess = new Equipe();
                    equipess = await _context.Equipes.FirstOrDefaultAsync(e => e.ID == d.equipeID);

                    if (equipess == null)
                    {
                        System.Console.WriteLine("null");
                    }
                    else
                    {
                        test.Add(equipess);
                    }
                    EquipeAll = test;
                }

            } else {
                ShowTeam = true;
                Equipe = await _context.Equipes.FirstOrDefaultAsync(e => e.ID == id);
            }
         
            return Page();

        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if(id == null)
            {
                return RedirectToPage("./RequeteEquipe", new { id = id });

            } else
            {
                Equipe = await _context.Equipes.FirstOrDefaultAsync(e => e.ID == id);
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                Equipe.valider = true;
                var de = await _context.DemandesEquipeCreations.FirstOrDefaultAsync(de => de.equipeID == Equipe.ID);
                de.approuver = true;
                _context.Attach(Equipe).State = EntityState.Modified;
                _context.Attach(de).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }



        }
    }
}
