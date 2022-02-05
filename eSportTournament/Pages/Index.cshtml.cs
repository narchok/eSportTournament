using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var nbDemandesEquipe = 0;
            if (user != null)
            {
                bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                showAdminThings = isAdmin;
                bool isLi = await _userManager.IsInRoleAsync(user, "Licencie");
                isLicencie = isLi;
                nbDemandesEquipe = await _context.DemandesEquipes.Where(de => de.approuver == false && de.userID == user.Id).CountAsync();

            }
            var nbDemandesE = await _context.DemandesEquipeCreations.Where(de => de.approuver == false).CountAsync();
            var nbDemandesL = await _context.DemandeLicences.Where(de => de.approuver == false).CountAsync();
            var nbDemandesO = await _context.DemandeOrganisateurs.Where(de => de.approuver == false).CountAsync();

            nbDemandesEquipes = nbDemandesEquipe;
            nbDemandesOrganisateurs = nbDemandesO;
            nbDemandesLicences = nbDemandesL;
            nbDemandesCreationEquipes = nbDemandesE;
        }
    }
}
