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
namespace eSportTournament.Pages
{
    public class DemandeLicenceModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;

        public DemandeLicenceModel(eSportTournament.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DemandeLicence DemandeLicence { get; set; }

        public Utilisateur Utilisateur { get; set; }

        [BindProperty]
        public string prenom { get; set; }
        [BindProperty]
        public string nom { get; set; }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
           
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            DemandeLicence demande = new DemandeLicence();
            Utilisateur user = new Utilisateur();

            user.nom = nom;

            user.prenom = prenom;

            user.userID = userId;
        

            demande.userID = userId;
            _context.DemandeLicences.Add(demande);
            _context.Utilisateurs.Add(user);


            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
