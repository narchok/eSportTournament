using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using eSportTournament.Data;
using eSportTournament.Models;
using Microsoft.EntityFrameworkCore;

namespace eSportTournament.Pages.Competitions
{
    public class CreateModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;

        [BindProperty]
        public Competition Competition { get; set; }

        [BindProperty]
        public int[] SelectedPlayers { get; set; }
        List<SelectListItem> select { get; set; }

        public IList<Equipe> EquipeAll { get; set; }

        [BindProperty]
        public bool viewPlayer { get; set; } = false;

        public CreateModel(eSportTournament.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            select = new List<SelectListItem>();
            EquipeAll = _context.Equipes.Where(e => e.valider == true).ToList();
            foreach(Equipe equipe in EquipeAll)
            {
                select.Add(new SelectListItem { Value = equipe.ID.ToString(), Text = equipe.nomEquipe });
            }
            ViewData["TEAMS"] = select;

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Competition.terminer = false;
            Competition.isTournoi = false;
            _context.Competitions.Add(Competition);
            await _context.SaveChangesAsync();

            System.Console.WriteLine("competition" + Competition.ID);

            return RedirectToPage("./ChampionnatEquipe", new { id = Competition.ID });
        }
    }
}
