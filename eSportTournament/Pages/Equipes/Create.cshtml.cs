using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using eSportTournament.Data;
using eSportTournament.Models;
using System.Security.Claims;

namespace eSportTournament.Pages.Equipes
{
    public class CreateModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;


        [BindProperty]
        public int[] SelectedPlayers { get; set; }
        List<SelectListItem> select { get; set; }



    


        public CreateModel(eSportTournament.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            select = new List<SelectListItem>();
            foreach (Utilisateur element in _context.Utilisateurs.Where(u => u.equipeID == null))
            {
               select.Add(new SelectListItem { Value = element.ID.ToString(), Text = element.prenom });
            }
            ViewData["PLAYERS"] = select;
          //   PlayersOptions = new SelectList(_context.Utilisateurs.Where(u => u.equipeID == null), nameof(Utilisateur.ID), nameof(Utilisateur.prenom));
            //PlayersOptions.Add

          //  SelectedPlayers = _context.Utilisateurs.Where(u => u.equipeID == null).;
            return Page();
        }

        [BindProperty]
        public Equipe Equipe { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Equipe.ownerID = userId;
            var data = _context.Equipes.Add(Equipe);
            await _context.SaveChangesAsync();
            for (int i = 0; i < SelectedPlayers.Length; i++)
            {
                DemandeEquipe demande = new DemandeEquipe();
                demande.userID = SelectedPlayers[i].ToString();
                demande.equipeID = data.Entity.ID;
                _context.DemandesEquipes.Add(demande);
                await _context.SaveChangesAsync();

            }

            DemandeEquipeCreation demandeCreation = new DemandeEquipeCreation();
            demandeCreation.equipeID = data.Entity.ID;
            demandeCreation.userID = userId;
            _context.DemandesEquipeCreations.Add(demandeCreation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
