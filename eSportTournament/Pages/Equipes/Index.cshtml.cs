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
using Microsoft.AspNetCore.Authorization;

namespace eSportTournament.Pages.Equipes
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(UserManager<IdentityUser> userManager, eSportTournament.Data.ApplicationDbContext context)
        {
            _userManager = userManager;

            _context = context;
        }
        [BindProperty]
        public bool showCreate { get; set; }

        [BindProperty]
        public bool showDelete { get; set; }
        public IList<Equipe> Equipe { get;set; }

        public async Task OnGetAsync()
        {

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                bool isLi = await _userManager.IsInRoleAsync(user, "Licencie");
                showCreate = isLi;
                bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                showDelete = isAdmin;
            }
            Equipe = await _context.Equipes.Where(e => e.valider == true).ToListAsync();
        }
    }
}
