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
        public int nbDemandesEquipes { get; set; }

        [BindProperty]
        public int nbDemandesLicences { get; set; }

        
        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                showAdminThings = isAdmin;
            }
            var nbDemandesE = await _context.DemandesEquipes.Where(de => de.approuver == false).CountAsync();
            var nbDemandesL = await _context.DemandeLicences.Where(de => de.approuver == false).CountAsync();

            nbDemandesLicences = nbDemandesL;
            nbDemandesEquipes = nbDemandesE;
        }
    }
}
