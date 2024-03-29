﻿using eSportTournament.Models;
using eSportTournament.VueModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eSportTournament.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly eSportTournament.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(UserManager<IdentityUser> userManager, eSportTournament.Data.ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public bool showAdminThings { get; set; }

        [BindProperty]
        public bool isLicencie { get; set; }
        [BindProperty]
        public int nbDemandesCreationEquipes { get; set; }

        [BindProperty]
        public int nbDemandesLicences { get; set; }


        [BindProperty]
        public int nbDemandesOrganisateurs { get; set; }

        [BindProperty]
        public int nbDemandesEquipes { get; set; }

        [BindProperty]
        public int nbMatchs { get; set; }

        [BindProperty]
        public int nbJeux { get; set; }

        [BindProperty]
        public UserProfile Profil { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var nbDemandesEquipe = 0;
            if (user != null)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Utilisateur utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.userID == userId);
                bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                showAdminThings = isAdmin;
                bool isLi = await _userManager.IsInRoleAsync(user, "Licencie");
               isLicencie = isLi;
                int nbMatchsToPlay = 0;
                if (utilisateur != null)
                {
                    nbDemandesEquipe = await _context.DemandesEquipes.Where(de => de.approuver == false && de.userID == utilisateur.ID.ToString()).CountAsync();

                    Equipe tempEquipe = await _context.Equipes.FirstOrDefaultAsync(e => e.ID == utilisateur.equipeID);
                    if(tempEquipe != null)
                    {
                        Profil.equipe = tempEquipe;
                        nbMatchsToPlay = await _context.Matchs.Where(m => m.gagnantID == null && (m.EquipeAID == tempEquipe.ID || m.EquipeBID == tempEquipe.ID)).CountAsync();

                    }
                    Profil = new UserProfile();
                    Profil.user = utilisateur;
                    Profil.nbMatchsAjouer = nbMatchsToPlay;
                }

            }
            var nbDemandesE = await _context.DemandesEquipeCreations.Where(de => de.approuver == false).CountAsync();
            var nbDemandesL = await _context.DemandeLicences.Where(de => de.approuver == false).CountAsync();
            var nbDemandesO = await _context.DemandeOrganisateurs.Where(de => de.approuver == false).CountAsync();
            var nbJ = await _context.Jeux.CountAsync();
            var nbM = await _context.Matchs.Where(de => de.gagnantID == null).CountAsync();

            nbDemandesEquipes = nbDemandesEquipe;
            nbDemandesOrganisateurs = nbDemandesO;
            nbDemandesLicences = nbDemandesL;
            nbDemandesCreationEquipes = nbDemandesE;
            nbJeux = nbJ;
            nbMatchs = nbM;
        }
    }
}
