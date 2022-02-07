using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using eSportTournament.Data;
using eSportTournament.Models;
using Microsoft.AspNetCore.Identity;

using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace eSportTournament.Pages
{
    [Authorize(Roles = "Licencie")]

    public class DemandeOrganisateurModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DemandeOrganisateurModel(UserManager<IdentityUser> userManager, eSportTournament.Data.ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DemandeOrganisateur DemandeO { get; set; }

        public Utilisateur Utilisateur { get; set; }


        [BindProperty]
        public bool showDemande { get; set; }

        [BindProperty]
        public string prenom { get; set; }
        [BindProperty]
        public string nom { get; set; }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
           
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            DemandeOrganisateur demande = new DemandeOrganisateur();
            var user = await _userManager.GetUserAsync(User);

            demande.userID = userId;
            _context.DemandeOrganisateurs.Add(demande);
            bool isLicencie = await _userManager.IsInRoleAsync(user, "Licencie");
            showDemande = isLicencie;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
