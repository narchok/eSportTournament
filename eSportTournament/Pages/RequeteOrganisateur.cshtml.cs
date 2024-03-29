﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eSportTournament.Data;
using eSportTournament.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace eSportTournament.Pages
{
    [Authorize(Roles = "Admin")]

    public class RequeteOrganisateurModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;
        //private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
       // private readonly UserManager<IdentityUser> _userManager;


        public RequeteOrganisateurModel(UserManager<IdentityUser> userManager,eSportTournament.Data.ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public List<DemandeOrganisateur> DemandeLicence { get; set; } = new List<DemandeOrganisateur>();


        public DemandeLicence[] demandes { get; set; }

        [BindProperty]
        public string[] noms { get; set; }
        public async Task<PageResult> OnGetAsync()
        {
            DemandeLicence = await _context.DemandeOrganisateurs.Where(d => d.approuver == false ).ToListAsync();
            var temp = new string[DemandeLicence.Count];
            for (int i = 0; i < DemandeLicence.Count; i++)
            {
                Utilisateur u = await _context.Utilisateurs.FirstOrDefaultAsync(ut => ut.userID == DemandeLicence[i].userID);
                temp[i] = u.nom + " " + u.prenom;
            }
            noms = temp;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {

            for (int i = 0; i < DemandeLicence.Count(); i++)
            {
                if(DemandeLicence[i].approuver == true)
                {
                    DemandeLicence demande = _context.DemandeLicences.FirstOrDefault(d => d.ID == DemandeLicence[i].ID);
                    demande.approuver = true;
                    var user = await _userManager.FindByIdAsync(demande.userID);
                    await _userManager.AddToRoleAsync(user, "Organisateur");
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
