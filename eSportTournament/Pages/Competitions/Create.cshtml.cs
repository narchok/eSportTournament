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
using Microsoft.AspNetCore.Authorization;

namespace eSportTournament.Pages.Competitions
{
    [Authorize(Roles = "Organisateur, Admin")]

    public class CreateModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;

        [BindProperty]
        public Competition Competition { get; set; }

        [BindProperty]
        public int[] SelectedPlayers { get; set; }
        List<SelectListItem> select { get; set; }

        public IList<Equipe> EquipeAll { get; set; }
        public IList<Jeu> Jeux { get; set; }


        [BindProperty]
        public bool viewPlayer { get; set; } = false;


        [BindProperty]
        public int SelectedGame { get; set; }

        List<SelectListItem> selectJeu { get; set; }

        public CreateModel(eSportTournament.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            select = new List<SelectListItem>();
            Jeux = _context.Jeux.ToList();

            EquipeAll = _context.Equipes.Where(e => e.valider == true).ToList();
            foreach(Equipe equipe in EquipeAll)
            {
                select.Add(new SelectListItem { Value = equipe.ID.ToString(), Text = equipe.nomEquipe });
            }
            ViewData["TEAMS"] = select;

            selectJeu = new List<SelectListItem>();
            foreach (Jeu j in Jeux)
            {
                selectJeu.Add(new SelectListItem { Value = j.ID.ToString(), Text = j.nomJeu });
            }
            ViewData["JEUX"] = selectJeu;

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
            Competition.jeuId = SelectedGame;
            _context.Competitions.Add(Competition);
            await _context.SaveChangesAsync();


            return RedirectToPage("./ChampionnatEquipe", new { id = Competition.ID });
        }
    }
}
